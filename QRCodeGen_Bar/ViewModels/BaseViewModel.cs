using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace QRCodeGen_Bar.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, obj) => { };

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
