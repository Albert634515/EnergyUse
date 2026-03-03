using EnergyUse.Core.Controllers;
using EnergyUse.Core.Interfaces;
using EnergyUse.Models;
using EnergyUse.Models.Common;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WpfUI.Services;

namespace WpfUI.ViewModels
{
    public class TarifGroupsViewModel : ViewModelBase
    {
        private readonly TariffGroupController _controller;
        private readonly IDialogService _dialogService;

        public ObservableCollection<TariffGroup> TariffGroups { get; } =
            new ObservableCollection<TariffGroup>();

        public ObservableCollection<SelectionItem> TariffGroupTypes { get; } =
            new ObservableCollection<SelectionItem>();

        private TariffGroup _selectedTariffGroup;
        public TariffGroup SelectedTariffGroup
        {
            get => _selectedTariffGroup;
            set => SetProperty(ref _selectedTariffGroup, value);
        }

        private string _statusText;
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

        public Action CloseAction { get; set; }

        public TarifGroupsViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            _controller = new TariffGroupController(Managers.Config.GetDbFileName());
            _controller.Initialize();

            AddCommand = new RelayCommand(_ => AddTariffGroup());
            SaveCommand = new RelayCommand(_ => SaveTariffGroup());
            CancelCommand = new RelayCommand(_ => CancelTariffGroup());
            DeleteCommand = new RelayCommand(_ => DeleteTariffGroup(), _ => SelectedTariffGroup != null);
            RefreshCommand = new RelayCommand(_ => LoadTariffGroups());
            CloseCommand = new RelayCommand(_ => CloseWindow());

            LoadTariffGroupTypes();
            LoadTariffGroups();
        }

        private void LoadTariffGroupTypes()
        {
            TariffGroupTypes.Clear();
            var items = Managers.SelectionItemList.GetTariffGroupTypeList();

            foreach (var item in items)
                TariffGroupTypes.Add(item);
        }

        private void LoadTariffGroups()
        {
            TariffGroups.Clear();

            _controller.UnitOfWork.TariffGroups =
                _controller.UnitOfWork.TariffGroupRepo.GetAll().ToList();

            foreach (var tg in _controller.UnitOfWork.TariffGroups)
                TariffGroups.Add(tg);

            SelectedTariffGroup = TariffGroups.FirstOrDefault();
            StatusText = $"Loaded {TariffGroups.Count} tariff groups";
        }

        private void AddTariffGroup()
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

        private void CancelTariffGroup()
        {
            _controller.UnitOfWork.CancelChanges();
            LoadTariffGroups();
            StatusText = "Changes cancelled";
        }

        private void SaveTariffGroup()
        {
            _controller.UnitOfWork.Complete();
            StatusText = "Changes saved";
        }

        private void DeleteTariffGroup()
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

        private void CloseWindow()
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