using Microsoft.AspNetCore.Mvc;
using TVM.Models;

namespace TVM.Repository
{
    public interface IDataRepository
    {
        Task<IEnumerable<Data>> GetData();

        Response PostData(Data data);
        Response PostLogin(Data data);

        Task<ActionResult<Data>> GetData(int id);



    }
}
