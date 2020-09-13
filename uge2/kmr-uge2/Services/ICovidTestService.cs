using kmr_uge2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kmr_uge2.Services
{
    public interface ICovidTestService
    {
        Task AddItemAsync(CovidTestModel person);
        Task DeleteItemAsync(string id);
        Task<CovidTestModel> GetItemAsync(string id);
        Task<IEnumerable<CovidTestModel>> GetItemsAsync(string queryString);
        Task UpdateItemAsync(string id, CovidTestModel person);
    }
}