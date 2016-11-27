﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Interfaces.AllocationTable
{
    public interface IFileAllocator
    {
        ISegment AllocateSegment(IBlockSpace blockSpace, FileStream fileStream);
    }
}