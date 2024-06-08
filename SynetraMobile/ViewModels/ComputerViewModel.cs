using SynetraMobile.Services;
using SynetraUtils.Models.DataManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SynetraMobile.ViewModels
{
    public class ComputerViewModel : INotifyPropertyChanged
    {
        private readonly ComputerService _computerService;
        private readonly ParcService _parcService;
        private List<Computer> _computers;
        private List<Parc> _parcs;
        private Parc _selectedFilter;


        public List<Computer> Computers
        {
            get => _computers;
            set
            {
                _computers = value;
                OnPropertyChanged();
            }
        }
        public List<Parc> Parcs
        {
            get => _parcs;
            set
            {
                _parcs = value;
                OnPropertyChanged();
            }
        }

        public Parc SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                _selectedFilter = value;
                FilterBy(value);
                OnPropertyChanged();
            }
        }

        public ComputerViewModel()
        {
            _computerService = new ComputerService();
            _parcService = new ParcService();
            LoadPCs();

        }

        private async void LoadPCs()
        {
            Computers = await _computerService.GetComputersAsync();
            Parcs = await _parcService.GetParcAsync();
        }
        public async void FilterBy(Parc filter)
        {
            if (filter is null)
            {
                Computers = new List<Computer>(_computers);
            }
            else
            {
                Computers = await _computerService.GetAllByParcAsync(filter.Id);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
