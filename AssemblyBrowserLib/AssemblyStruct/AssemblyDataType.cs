using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLib.AssemblyStruct
{
    public class AssemblyDataType
    {
        public string name;

        public ObservableCollection<AssemblyDataType> DataTypes;
        public ObservableCollection<AssemblyField> Fields;
        public ObservableCollection<AssemblyMethod> Methods;
    }
}
