namespace STP.MS.WorkSample.Core.Interfaces
{
    public interface IMapper<TVM>
    {
        ICollectionJsonResult<TVM> GetByParentId(int parentid, ICollectionRequestParams reqParams);

        TVM Get(int id);

        void Insert(TVM tvm);

        void Update(TVM tvm);

        void Delete(int id);
    }
}
