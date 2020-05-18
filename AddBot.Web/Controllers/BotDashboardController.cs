using AddBot.Web.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AddBot.Web.Controllers
{
    public class BotDashboardController : Controller
    {
        private readonly IBotStoreClient _botStoreClient;
        public BotDashboardController(IBotStoreClient botStoreClient)
        {
            _botStoreClient = botStoreClient;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var response = await _botStoreClient.GetBotDetails();
            return View(response);
        }
    }
}