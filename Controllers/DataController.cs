using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TVM.Models;
using TVM.Services;

namespace TVM.Controllers
{
    [Route("api/data")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataService _dataService;

        public DataController(IDataService dataService)
        {
            _dataService = dataService;

        }

       [Authorize]

        [HttpGet("pp")]
        public Task<IEnumerable<Data>> GetData()
        {
            return _dataService.GetData();
        }
        

        [HttpPost("mango")]
        [AllowAnonymous]

        public Response PostData(Data data)
        {
            return _dataService.PostData(data);
        }


        
        [HttpPost("log")]
        [AllowAnonymous]
        public Response PostLogin(Data data)
        {
            return _dataService.PostLogin(data);
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<Data>> GetData(int id)
        {
            return await _dataService.GetData(id);
        }



    }
}
