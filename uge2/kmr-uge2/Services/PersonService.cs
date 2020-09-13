using kmr_uge2.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kmr_uge2.Services
{

    public class PersonService : IPersonService
    {
        private Container _container;
        public PersonService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }
        public async Task AddItemAsync(PersonModel person)
        {
            await this._container.CreateItemAsync<PersonModel>(person, new PartitionKey(person.Id));
        }
        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<PersonModel>(id, new PartitionKey(id));
        }
        public async Task<PersonModel> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<PersonModel> response = await this._container.ReadItemAsync<PersonModel>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
        public async Task<IEnumerable<PersonModel>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<PersonModel>(new QueryDefinition(queryString));
            List<PersonModel> results = new List<PersonModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task UpdateItemAsync(string id, PersonModel person)
        {
            await this._container.UpsertItemAsync<PersonModel>(person, new PartitionKey(id));
        }
    }
}
