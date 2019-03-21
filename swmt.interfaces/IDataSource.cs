using System;
using System.Collections.Generic;

namespace Swmt.Interfaces 
{
    public interface IDataSource<T>
    {
        IList<T> All();
        IDictionary<string, IList<T>> All(string key);
        IList<T> Find(string key, dynamic val);
        T FirstOrDefault(string key, dynamic val);
    }
}