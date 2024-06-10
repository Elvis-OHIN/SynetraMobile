using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        private readonly RoomService _roomService;
        private List<Computer> _computers;
        private List<Parc> _parcs;
        private Parc _selectedFilter;
        public ObservableCollection<GroupedComputers> GroupComputer { get; private set; } = new ObservableCollection<GroupedComputers>();


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
            _roomService = new RoomService();
            LoadPCs();

        }

        private async void LoadPCs()
        {
            Computers = await _computerService.GetComputersAsync();
            Parcs = await _parcService.GetParcAsync();
            var groupedPCs = Computers
            .GroupBy(pc => pc.RoomId)
            .ToList();


            GroupByRoom(Computers);
          

        }
        private async void GroupByRoom(List<Computer> computers)
        {
            var rooms = await _roomService.GetRoomAsync();
            foreach (var room in rooms)
            {
                var groupComputers = new List<Computer>();
                foreach (var pc in computers)
                {
                    if (room.Id == pc.RoomId)
                    {
                        groupComputers.Add(pc);
                    }
                }
                GroupComputer.Add(new GroupedComputers(room.Name, groupComputers));
            }
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
    public class GroupedComputers : List<Computer>
    {
        public string? Room { get; private set; }

        public GroupedComputers(string? room, List<Computer> computer) : base(computer)
        {
            Room = room;
        }
    }
}
