using SynetraMobile.Services;
using SynetraUtils.Models.DataManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SynetraMobile.ViewModels
{
    public class RoomViewModel : INotifyPropertyChanged
    {
        private readonly ParcService _parcService;
        private readonly RoomService _roomService;
        private Parc _selectedFilter;
        private List<Room> _rooms;
        private List<Parc> _parcs;

        public List<Room> Rooms
        {
            get => _rooms;
            set
            {
                _rooms = value;
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

        public RoomViewModel()
        {
            _parcService = new ParcService();
            _roomService = new RoomService();
            LoadRooms();
        }

        private async void LoadRooms()
        {
            Parcs = await _parcService.GetParcAsync();
            if (Parcs.Count > 0)
            {
                SelectedFilter = Parcs.FirstOrDefault();
                if (SelectedFilter is null)
                {
                    Rooms = await _roomService.GetRoomAsync();
                }
            }
        }
        public async void FilterBy(Parc filter)
        {
            if (filter is not null)
            {
                Rooms = await _roomService.GetAllByParcAsync(filter.Id);
            }
            else
            {
                Rooms = new List<Room>(_rooms);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
