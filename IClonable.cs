﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlocks
{
    public interface IClonable<T>
    {
        T Clone();
    }
}
