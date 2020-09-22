using AssemblyBrowserLib;
using AssemblyBrowserLib.AssemblyStruct;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WPFApp
{
    public class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<AssemblyNamespace> _assemblyname;
        public ObservableCollection<AssemblyNamespace> assemblyname
        {
            get
            {
                return _assemblyname;
            }
            set
            {
                _assemblyname = value;
                OnPropertyChanged("assemblyname");
            }
        }


        private AssemblyStruct _assemblyStruct;      
        public AssemblyStruct assemblyStruct { 
            get 
            {
                return _assemblyStruct;
            }
            set 
            {
                _assemblyStruct = value;
                OnPropertyChanged("assemblyStruct");
            } 
        }

        public ViewModel()
        {
            AssemblyInfo.LoadAssembly();
            this.assemblyStruct = AssemblyInfo.GetAssemblyInfo();
            this.assemblyname = assemblyStruct.Namespaces;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
