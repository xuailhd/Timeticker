using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeticker
{
    public class FocusHelper
    {
        private static TroopsModel troopsModel = null;
        public static TroopsModel GetTroops()
        {
            if (troopsModel == null)
            {
                troopsModel = new TroopsModel();
                ObservableCollection<Troop> troops = new ObservableCollection<Troop>();
                troops.Add(new Troop() { Name = "部队1" });
                troops.Add(new Troop() { Name = "部队2" });
                troops.Add(new Troop());
                troops.Add(new Troop());
                troops.Add(new Troop());
                troopsModel.Troops = troops;
            }
            return troopsModel;
        }

    }

    public class TroopsModel
    {
        public ObservableCollection<Troop> Troops { get; set; }
    }

    public class Troop : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private string distance;
        public string Distance
        {
            get { return distance; }
            set
            {
                distance = value;
                NotifyPropertyChanged("Distance");
            }
        }

        private string used;
        public string Used
        {
            get { return used; }
            set
            {
                used = value;
                NotifyPropertyChanged("Used");
            }
        }

        private string countdown;
        public string Countdown
        {
            get { return countdown; }
            set
            {
                countdown = value;
                NotifyPropertyChanged("Countdown");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
