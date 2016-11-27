using Oak.Virtualizer.Interfaces.BlockSpaceContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oak.Virtualizer.Interfaces;
using System.IO;

namespace Oak.Virtualizer.Classes.BlockSpaceContainerClasses
{
    public class BlockSpaceGetter : IBlockSpaceGetter
    {
        public IBlockSpace Get(long id, FileStream fileStream)
        {
            //TODO: Implement a way to get a block space
            throw new NotImplementedException();
        }
    }
}
