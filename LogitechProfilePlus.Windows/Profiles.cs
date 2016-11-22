using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace LogitechProfilePlus.Windows
{
    class Profiles
    {
        public BindingList<ProfileData> ProfileList { get; set; }
        public List<Color> ColorList { get; set; }

        public Profiles()
        {
            ProfileList = new BindingList<ProfileData>();
            ColorList = new List<Color>();
        }

    }
}
