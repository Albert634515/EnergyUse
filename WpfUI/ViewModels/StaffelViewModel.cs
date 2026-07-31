using EnergyUse.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace WpfUI.ViewModels;

public class StaffelViewModel : ViewModelBase
{
    private readonly EnergyUse.Core.UnitOfWork.Staffel _unitOfWork;

    public ObservableCollection<Staffel> Staffels { get; } = new();
    public Staffel? SelectedStaffel { get; set; }

    private long _rateId;

    public ICommand AddCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand RefreshCommand { get; }


    public StaffelViewModel()
    {
        _unitOfWork = new EnergyUse.Core.UnitOfWork.Staffel(Managers.Config.GetDbFileName());

        AddCommand = new RelayCommand(_ => addStaffel());
        SaveCommand = new RelayCommand(_ => setStaffel());
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
        _unitOfWork.Complete();
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
}
