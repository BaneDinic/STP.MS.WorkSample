using STP.MS.WorkSample.Core.Interfaces;
using System.Collections.Generic;

namespace STP.MS.WorkSample.Services.ViewModels
{
    public class CollectionJsonResult<T> : ICollectionJsonResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
