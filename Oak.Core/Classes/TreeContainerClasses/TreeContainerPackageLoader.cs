using Oak.Core.Interfaces.TreeContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oak.Core.Interfaces;

namespace Oak.Core.Classes.TreeContainerClasses
{
    public class TreeContainerPackageLoader : ITreeContainerPackageLoader
    {
        public ITreePackage Load(ITreePackage treePackage)
        {
            return treePackage;
        }
    }
}
