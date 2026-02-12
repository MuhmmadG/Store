using System;
using System.Collections.Generic;
using System.Text;

namespace myStructure.interfaces
{
    public interface IComparer<in T>
    {
        int Compare(T? x, T? y);
    }

}
