using EnergyUse.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace WpfUI.ViewModels;

public class StaffelViewModel : ViewModelBase
{
    private readonly EnergyUse.Core.UnitOfWork.Staffel _unitOfWork;

    public ObservableCollection<Staffel> Staffels { get; } = new();
    private Staffel? _selectedStaffel;
    public Staffel? SelectedStaffel
    {
        get => _selectedStaffel;
        set => SetProperty(ref _selectedStaffel, value);
    }

    private long _rateId;

    public ICommand AddCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand RefreshCommand { get; }


    public StaffelViewModel()
    {
        _unitOfWork = new EnergyUse.Core.UnitOfWork.Staffel(Managers.Config.GetDbFileName());

        AddCommand = new RelayCommand(_ => addStaffel(), _ => _rateId > 0);
        SaveCommand = new RelayCommand(_ => setStaffel(), _ => _rateId > 0);
        CancelCommand = new RelayCommand(_ => cancelStaffel());
        DeleteCommand = new RelayCommand(_ => deleteStaffel(), _ => SelectedStaffel != null);
        RefreshCommand = new RelayCommand(_ => refreshStaffels());
    }

    public async void GetStaffels(long rateId)
    {
        _rateId = rateId;
        Staffels.Clear();

        _unitOfWork.Staffels = new List<Staffel>();

        if (_rateId > 0)
            _unitOfWork.Staffels = (await _unitOfWork.StaffelRepo.SelectByRateId(_rateId)).ToList();

        _unitOfWork.SetListSorted();

        foreach (var s in _unitOfWork.Staffels)
            Staffels.Add(s);

        SelectedStaffel = Staffels.FirstOrDefault();
        CommandManager.InvalidateRequerySuggested();
    }

    private void addStaffel()
    {
        var entity = _unitOfWork.AddDefaultEntity(_rateId);

        Staffels.Clear();
        foreach (var s in _unitOfWork.Staffels)
            Staffels.Add(s);

        SelectedStaffel = entity;
    }

    private void setStaffel()
    {
        var validationMessage = validateStaffels();
        if (validationMessage is not null)
        {
            MessageBox.Show(validationMessage, "Invalid staffel", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        _unitOfWork.Complete();
        _unitOfWork.SetListSorted();
        synchronizeStaffels();
    }

    private void cancelStaffel()
    {
        _unitOfWork.CancelChanges();
        GetStaffels(_rateId);
    }

    private void deleteStaffel()
    {
        if (SelectedStaffel == null)
            return;

        var message = Managers.Languages.GetResourceString("StaffelAskDelete", "Are you sure you want to delete this staffel?");
        var title = Managers.Languages.GetResourceString("DeleteTitle", "Delete?");

        if (MessageBox.Show(message, title, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
            _unitOfWork.Delete(SelectedStaffel);

            Staffels.Clear();
            foreach (var s in _unitOfWork.Staffels)
                Staffels.Add(s);

            SelectedStaffel = Staffels.FirstOrDefault();
        }
    }

    private void refreshStaffels()
    {
        GetStaffels(_rateId);
    }

    private string? validateStaffels()
    {
        var orderedStaffels = Staffels.OrderBy(x => x.ValueFrom).ToList();
        if (orderedStaffels.Count == 0)
            return "Add at least one staffel row.";

        if (orderedStaffels[0].ValueFrom != 0)
            return "The first staffel must start at 0.";

        for (var index = 0; index < orderedStaffels.Count; index++)
        {
            var staffel = orderedStaffels[index];
            if (staffel.ValueFrom < 0 || staffel.ValueTill <= staffel.ValueFrom)
                return "Each staffel must have a non-negative start and an end greater than its start.";

            if (index > 0 && staffel.ValueFrom != orderedStaffels[index - 1].ValueTill)
                return "Staffel ranges must connect without gaps or overlap.";
        }

        return null;
    }

    private void synchronizeStaffels()
    {
        var selectedStaffel = SelectedStaffel;
        Staffels.Clear();
        foreach (var staffel in _unitOfWork.Staffels)
            Staffels.Add(staffel);
        SelectedStaffel = selectedStaffel;
    }
}
