using AssemblyBrowserLib;
using AssemblyBrowserLib.AssemblyStruct;
using AssemblyBrowserLib.AssemblyStructView;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;

namespace WPFApp
{
    public class ViewModel : INotifyPropertyChanged
    {
        private AssemblyStructView _assemblyStruct;      
        public AssemblyStructView AssemblyStruct { 
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
                        try
                        {
                            // reading selected assembly
                            OpenFileDialog openFileDialog = new OpenFileDialog();
                            if (openFileDialog.ShowDialog() == true)
                            {
                                Assembly currentAssembly = Assembly.LoadFrom(openFileDialog.FileName);
                                AssemblyStruct assemblyStruct = new AssemblyStruct(currentAssembly);
                                this.AssemblyStruct = new AssemblyStructView(assemblyStruct, HideGenerated);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }));
            }
        }

        public bool HideGenerated = true;

        public ViewModel()
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            AssemblyStruct assemblyStruct = new AssemblyStruct(currentAssembly);
            this.AssemblyStruct = new AssemblyStructView(assemblyStruct, HideGenerated);

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
