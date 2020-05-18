using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AddBot.Web.Models;
using Newtonsoft.Json;

namespace AddBot.Web.Utilities
{
    public class BotStoreClient : IBotStoreClient
    {
        public HttpClient Client { get; private set; }
        public BotStoreClient(HttpClient httpClient)
        {
            Client = httpClient;
        }

        public async Task<BotListing> GetBotDetails()
        {
            var result = await Client.GetAsync("botlist");
            if (!result.IsSuccessStatusCode)
            {
                return default;
            }
            using (var stream = await result.Content.ReadAsStreamAsync())
            {
                return stream.Deserialize<BotListing>();
            }
        }

        public async Task SaveBotDetails(BotListDetails botListDetails)
        {
            var content = JsonConvert.SerializeObject(botListDetails);
            var httpResponse = await Client.PostAsync("botlist", new StringContent(content, Encoding.Default, "application/json"));

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot add a todo task");
            }
            //var createdTask = JsonConvert.DeserializeObject<Todo>(await httpResponse.Content.ReadAsStringAsync());
            //return createdTask;
        }

        public async Task<List<BotImageDetails>> GetBotImageDetails()
        {
            var result = await Client.GetAsync("botimage/display");
            if (!result.IsSuccessStatusCode)
            {
                return default;
            }
            using (var stream = await result.Content.ReadAsStreamAsync())
            {
                return stream.Deserialize<List<BotImageDetails>>();
            }
        }

        //public async Task<BotListDetails> GetBotDetailsById(int botId)
        //{
        //    var result = await Client.GetAsync("botlist/" + botId);
        //    if (!result.IsSuccessStatusCode)
        //    {
        //        return default;
        //    }
        //    using (var stream = await result.Content.ReadAsStreamAsync())
        //    {
        //        return stream.Deserialize<BotListDetails>();
        //    }
        //}
        public async Task<BotListDetails> GetBotDetailsById(int id)
        {
            var result = await Client.GetAsync("botlist/" + id);
            if (!result.IsSuccessStatusCode)
            {
                return default;
            }
            using (var stream = await result.Content.ReadAsStreamAsync())
            {
                return stream.Deserialize<BotListDetails>();
            }
        }
    }
}
