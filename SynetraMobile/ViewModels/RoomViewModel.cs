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
        private readonly UserService _userService;
        private Parc _selectedFilter;
        private bool _isAuthorize;
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

        public bool IsAuthorize
        {
            get => _isAuthorize;
            set
            {
                _isAuthorize = value;
                OnPropertyChanged();
            }
        }

        public RoomViewModel()
        {
            _parcService = new ParcService();
            _roomService = new RoomService();
            _userService = new UserService();
            LoadRooms();
        }

        private async void LoadRooms()
        {
            var user = await _userService.GetMe();
            if (user is not null)
            {
                if (user.ParcId != null)
                {
                    Parcs = new List<Parc>();
                    Parcs.Add(new Parc { Id = (int)user.ParcId });
                    IsAuthorize = false;
                }
                else
                {
                    IsAuthorize = true;
                    Parcs = await _parcService.GetParcAsync();
                }
            }
            
            
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
