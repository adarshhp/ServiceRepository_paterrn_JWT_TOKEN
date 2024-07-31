using Microsoft.AspNetCore.Mvc;
using TVM.Models;

namespace TVM.Services
{
    public interface IDataService
    {
        Task<IEnumerable<Data>> GetData();

        Response PostData(Data data);
        Response PostLogin(Data data);

        Task<ActionResult<Data>> GetData(int id);


    }
}
