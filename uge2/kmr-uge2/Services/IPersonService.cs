using kmr_uge2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kmr_uge2.Services
{
    public interface IPersonService
    {
        Task AddItemAsync(PersonModel person);
        Task DeleteItemAsync(string id);
        Task<PersonModel> GetItemAsync(string id);
        Task<IEnumerable<PersonModel>> GetItemsAsync(string queryString);
        Task UpdateItemAsync(string id, PersonModel person);
    }
}