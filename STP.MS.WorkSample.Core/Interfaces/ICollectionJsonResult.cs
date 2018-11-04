using System;
using System.Collections.Generic;
using System.Text;

namespace STP.MS.WorkSample.Core.Interfaces
{
    public interface ICollectionJsonResult<T>
    {
        IEnumerable<T> Items { get; set; }
        int TotalCount { get; set; }
    }
}
