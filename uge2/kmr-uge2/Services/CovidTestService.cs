using kmr_uge2.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kmr_uge2.Services
{
    public class CovidTestService : ICovidTestService
    {
        private Container _container;
        public CovidTestService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }
        public async Task AddItemAsync(CovidTestModel person)
        {
            await this._container.CreateItemAsync<CovidTestModel>(person, new PartitionKey(person.Id));
        }
        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<CovidTestModel>(id, new PartitionKey(id));
        }
        public async Task<CovidTestModel> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<CovidTestModel> response = await this._container.ReadItemAsync<CovidTestModel>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
        public async Task<IEnumerable<CovidTestModel>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<CovidTestModel>(new QueryDefinition(queryString));
            List<CovidTestModel> results = new List<CovidTestModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task UpdateItemAsync(string id, CovidTestModel person)
        {
            await this._container.UpsertItemAsync<CovidTestModel>(person, new PartitionKey(id));
        }
    }
}
