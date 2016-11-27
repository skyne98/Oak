using Oak.Core.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Core.Interfaces
{
    public interface ITreeValue : ITreeNode
    {
        //Getters
        byte GetType();
        T GetValue<T>();

        //Setters
        void Delete();
        void SetValue<T>();
    }
}
