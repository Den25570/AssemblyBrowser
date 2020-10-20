using AssemblyBrowserLib.AssemblyStruct;
using AssemblyBrowserLib.AssemblyStructView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLib.AssemblyStructView
{
    public class AssemblyDataTypeView
    {
        private string _fullName;
        private List<AssemblyTypeMemberView> _fields;

        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                OnPropertyChanged("FullName");
            }
        }

        public List<AssemblyTypeMemberView> Fields
        {
            get { return _fields; }
            set
            {
                _fields = value;
                OnPropertyChanged("Fields");
            }
        }

        public AssemblyDataTypeView(AssemblyDataType dataType, bool HideGenerated)
        {
            FullName = dataType.FullName;
            Fields = dataType.Fields
                .Where(member => !(member.IsGenerated && HideGenerated)).ToList()
                .ConvertAll<AssemblyTypeMemberView>(member => new AssemblyTypeMemberView(member));
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
