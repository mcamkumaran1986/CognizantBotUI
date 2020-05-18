using AddBot.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddBot.Web.Utilities
{
    public interface IBotStoreClient
    {
        Task<BotListing> GetBotDetails();
        Task SaveBotDetails(BotListDetails botListDetails);
        Task<BotListDetails> GetBotDetailsById(int id);
        Task<List<BotImageDetails>> GetBotImageDetails();
    }
}
