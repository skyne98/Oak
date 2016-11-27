using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Core.Interfaces
{
    public interface ITreePackage : ITreeNode
    {
        //Getters
        List<ITreeNode> GetChildren();
        List<ITreeValue> GetValues();
        List<ITreePackage> GetPackages();
        int GetChildrenCount();
        int GetValuesCount();
        int GetPackagesCount();

        //Setters
        void AddNode(ITreeNode treeNode);
        void AddValue(ITreeValue treeValue);
        void AddPackage(ITreePackage treePackage);
    }
}
