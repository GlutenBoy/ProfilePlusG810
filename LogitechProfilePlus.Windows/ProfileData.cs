using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogitechProfilePlus.Windows
{
    class ProfileData : INotifyPropertyChanged
    {
        private string _name;
        public List<KeyData> KeyList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public String Name {
            get { return _name; }
            set
            {
                 _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }

        public ProfileData(String name)
        {
            Name = name;
            KeyList = new List<KeyData>();
        }


        public override string ToString()
        {
            return Name;
        }
    }
}
