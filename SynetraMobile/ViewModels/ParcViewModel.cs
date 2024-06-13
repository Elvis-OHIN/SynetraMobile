using Microsoft.Maui.Controls.Compatibility;
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
using System.Windows.Input;

namespace SynetraMobile.ViewModels
{
    public class ParcViewModel : INotifyPropertyChanged
    {
        private readonly ParcService _parcService;
        private List<Parc> _parcs;
        public ICommand SearchCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public List<Parc> Parcs
        {
            get => _parcs;
            set
            {
                _parcs = value;
                OnPropertyChanged();
            }
        }

        public ParcViewModel()
        {
            _parcService = new ParcService();
            LoadParcs();
            SearchCommand = new Command<string>(OnSearch);
        }
        private async void OnSearch(string query)
        {
            var parc = await _parcService.GetParcAsync();
            if (string.IsNullOrWhiteSpace(query))
            {
                Parcs = new List<Parc>(parc);
            }
            else
            {
                var filteredParcs = parc.Where(p => p.Name.ToLower().Contains(query.ToLower())).ToList();
                Parcs = new List<Parc>(filteredParcs);
            }
        }
        public void OnSearchTextChanged(string query)
        {
            OnSearch(query);
        }
        private async void LoadParcs()
        {
            Parcs = await _parcService.GetParcAsync();
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
