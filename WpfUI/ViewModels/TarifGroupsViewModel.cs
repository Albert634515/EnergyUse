using EnergyUse.Core.Controllers;
using EnergyUse.Core.Interfaces;
using EnergyUse.Models;
using EnergyUse.Models.Common;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WpfUI.ViewModels
{
    public class TarifGroupsViewModel : ViewModelBase
    {
        private readonly TariffGroupController _controller;
        private readonly IDialogService _dialogService;

        public ObservableCollection<TariffGroup> TariffGroups { get; } = new();
        public ObservableCollection<SelectionItem> TariffGroupTypes { get; } = new();

        private TariffGroup? _selectedTariffGroup;
        public TariffGroup? SelectedTariffGroup
        {
            get => _selectedTariffGroup;
            set => SetProperty(ref _selectedTariffGroup, value);
        }

        private string _statusText = string.Empty;
        public string StatusText
        {
            get => _statusText;
            set => SetProperty(ref _statusText, value);
        }

        public ICommand AddCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand CloseCommand { get; }

        public Action? CloseAction { get; set; }

        public TarifGroupsViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            _controller = new TariffGroupController(Managers.Config.GetDbFileName());
            _controller.Initialize();

            AddCommand = new RelayCommand(_ => addTariffGroup());
            SaveCommand = new RelayCommand(_ => saveTariffGroup());
            CancelCommand = new RelayCommand(_ => cancelTariffGroup());
            DeleteCommand = new RelayCommand(_ => deleteTariffGroup(), _ => SelectedTariffGroup != null);
            RefreshCommand = new RelayCommand(_ => setTariffGroups());
            CloseCommand = new RelayCommand(_ => closeWindow());

            setTariffGroupTypes();
            setTariffGroups();
        }

        private void setTariffGroupTypes()
        {
            TariffGroupTypes.Clear();
            var items = Managers.SelectionItemList.GetTariffGroupTypeList();

            foreach (var item in items)
                TariffGroupTypes.Add(item);
        }

        private void setTariffGroups()
        {
            TariffGroups.Clear();

            _controller.UnitOfWork.TariffGroups =
                _controller.UnitOfWork.TariffGroupRepo.GetAll().ToList();

            foreach (var tg in _controller.UnitOfWork.TariffGroups)
                TariffGroups.Add(tg);

            SelectedTariffGroup = TariffGroups.FirstOrDefault();
            StatusText = $"Loaded {TariffGroups.Count} tariff groups";
        }

        private void addTariffGroup()
        {
            var caption = Managers.Languages.GetResourceString(
                "TarifGroupNewGroup",
                "New group");

            var entity = _controller.UnitOfWork.SetDefaultEntity(caption);

            if (!TariffGroups.Contains(entity))
                TariffGroups.Add(entity);

            SelectedTariffGroup = entity;
            StatusText = "New tariff group added";
        }

        private void cancelTariffGroup()
        {
            _controller.UnitOfWork.CancelChanges();
            setTariffGroups();
            StatusText = "Changes cancelled";
        }

        private void saveTariffGroup()
        {
            _controller.UnitOfWork.Complete();
            StatusText = "Changes saved";
        }

        private void deleteTariffGroup()
        {
            if (SelectedTariffGroup == null)
                return;

            var message = Managers.Languages.GetResourceString(
                "TariffGroupsAskDelete",
                "Are you sure you want to delete this tariff group?");

            var title = Managers.Languages.GetResourceString(
                "DeleteTitle",
                "Delete?");

            if (_dialogService.ShowYesNo(message, title))
            {
                _controller.UnitOfWork.Delete(SelectedTariffGroup);
                TariffGroups.Remove(SelectedTariffGroup);
                SelectedTariffGroup = TariffGroups.FirstOrDefault();
                StatusText = "Tariff group deleted";
            }
        }

        private void closeWindow()
        {
            if (_controller.UnitOfWork.HasChanges())
            {
                if (_dialogService.WarningUnsavedChanges())
                    return;
            }

            CloseAction?.Invoke();
        }
    }
}