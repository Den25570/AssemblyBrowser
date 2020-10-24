using AssemblyBrowserLib.AssemblyStruct;
using AssemblyBrowserLib.AssemblyStruct.TypeMembers;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AssemblyBrowserLib.AssemblyStructView
{
    public class AssemblyTypeMemberView : INotifyPropertyChanged
    {
        private string _fullName;

        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                OnPropertyChanged("FullName");
            }
        }

        public AssemblyTypeMemberView(AssemblyTypeMember member)
        {
            bool isExtension = (member.typeMember is TypeMethod) ? (member.typeMember as TypeMethod).IsExtensionMethod : false;
            FullName = member.GetFullName() + (isExtension ? "(Extension method)" : "");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
