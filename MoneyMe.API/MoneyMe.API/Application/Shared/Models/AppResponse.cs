namespace MoneyMe.API.Application.Shared.Models
{
    public class AppResponse<TData>
    {
        public TData? data { get; private set; }
        public dynamic? meta { get; private set; }

        public AppResponse(TData? objData, dynamic? pagination = null)
        {
            data = objData;
            meta = pagination;
        }
    }

}
