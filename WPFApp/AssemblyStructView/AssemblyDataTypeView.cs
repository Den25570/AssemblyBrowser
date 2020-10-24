using AssemblyBrowserLib.AssemblyStruct;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

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
            FullName = dataType.GetFullName();
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
