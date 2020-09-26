﻿using AssemblyBrowserLib;
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
        private AssemblyStruct _assemblyStruct;      
        public AssemblyStruct AssemblyStruct { 
            get 
            {
                return _assemblyStruct;
            }
            set 
            {
                _assemblyStruct = value;
                OnPropertyChanged("AssemblyStruct");
            } 
        }

        private RelayCommand _loadAssemblyCommand;
        public RelayCommand LoadAssemblyCommand
        {
            get
            {
                return _loadAssemblyCommand ??
                    (_loadAssemblyCommand = new RelayCommand(obj =>
                    {
                        Phone phone = new Phone();
                        Phones.Insert(0, phone);
                        SelectedPhone = phone;
                    }));
            }
        }

        public ViewModel()
        {
            AssemblyInfo.LoadAssembly();
            this.AssemblyStruct = AssemblyInfo.GetAssemblyInfo();

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
