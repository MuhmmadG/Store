using System;
using System.Collections.Generic;
using System.Text;

namespace myStructure.interfaces
{
    public interface IEqualityComparer<T>
    {
        bool Equals(T x, T y);
        int GetHashCode(T obj);
    }
}
