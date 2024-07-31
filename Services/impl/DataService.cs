
using Microsoft.AspNetCore.Mvc;
using TVM.Models;
using TVM.Repository;

namespace TVM.Services.impl
{
    public class DataService : IDataService
    {
       
            public readonly IDataRepository _dataRepository;
            private readonly HttpClient _httpClient;

            public DataService(IDataRepository dataRepository, HttpClient httpClient)
            {
                _dataRepository = dataRepository;
                _httpClient = httpClient;
            }

            public Task<IEnumerable<Data>> GetData()
            {
                return _dataRepository.GetData();
            }

        public Response PostData(Data data)
        {
            return _dataRepository.PostData(data);

        }
        public Response PostLogin(Data data)
        {
            return _dataRepository.PostLogin(data);
        }

        public async Task<ActionResult<Data>> GetData(int id)
        {
            return await _dataRepository.GetData(id);
        }



    }
}
