using STP.MS.WorkSample.Core.Interfaces;


namespace STP.MS.WorkSample.Services.ViewModels
{
    public class CollectionRequestParams : ICollectionRequestParams
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
