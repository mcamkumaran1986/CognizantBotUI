using AddBot.Web.Models;
using AddBot.Web.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace AddBot.Web.Controllers
{
    public class BotStoreController : Controller
    {
        private static List<BotAttributeMaster> botAttributeMasters = new List<BotAttributeMaster>();
        private static List<BotIntendMaster> botIntendMasters = new List<BotIntendMaster>();
        private static List<BotSkillMaster> botSkillMasters = new List<BotSkillMaster>();
        private static BotMaster botMasterEdit;
        private static int botSkillMasterId;
        private readonly IBotStoreClient _botStoreClient;
        public BotStoreController(IBotStoreClient botStoreClient)
        {
            _botStoreClient = botStoreClient;
        }
        public async Task<IActionResult> Index()
        {
            @ViewBag.BotImageDetails = await _botStoreClient.GetBotImageDetails();
            return View();
        }
        public async Task<IActionResult> Edit(int Id)
        {
            @ViewBag.BotImageDetails = await _botStoreClient.GetBotImageDetails();

            BotListDetails botListDetails = await _botStoreClient.GetBotDetailsById(Id);
            BotMasterModel botMasterModel = new BotMasterModel
            {
                BotName = botListDetails.BotMasters.BotName,
                BotImageID = botListDetails.BotMasters.BotImageID,
                BotStatus = botListDetails.BotMasters.Active ? "true" : "false",
                SkillName = botListDetails.BotSkillMasters[0].SkillName,
            };
            botSkillMasterId = botListDetails.BotSkillMasters[0].BotSkillID;
            botMasterEdit = botListDetails.BotMasters;
            botAttributeMasters = botListDetails.BotAttributeMasters;
            botIntendMasters = botListDetails.BotIntendMasters;
            return View(botMasterModel);
        }
        public IActionResult BotAttributes()
        {
            var model = new BotAttributeMaster { };
            return PartialView("_BotAttributes", model);
        }
        public IActionResult BotCustomIntend()
        {
            var model = new BotIntendMaster { };
            return PartialView("_BotCustomIntend", model);
        }
        public ActionResult GetView(string attributeName, string viewName)
        {
            if (viewName == "_DisplayAttribute")
            {
                if (!string.IsNullOrEmpty(attributeName))
                {
                    BotAttributeMaster botAttributeMaster = new BotAttributeMaster { AttributeName = attributeName };
                    botAttributeMasters.Add(botAttributeMaster);
                }
                return PartialView(viewName, botAttributeMasters);
            }
            else if (viewName == "_DisplayCutomIntend")
            {
                if (!string.IsNullOrEmpty(attributeName))
                {
                    BotIntendMaster botIntendMaster = new BotIntendMaster { IntendName = attributeName };
                    botIntendMasters.Add(botIntendMaster);
                }
                return PartialView(viewName, botIntendMasters);
            }
            return null;
        }
        [HttpPost]
        [ActionName("savebot")]
        //public ActionResult SaveBotDetails()
        public async Task<IActionResult> SaveBotDetails()
        {
            BotListDetails botListDetails = new BotListDetails();

            bool.TryParse(Convert.ToString(HttpContext.Request.Form["BotStatus"]), out bool isAvailable);

            BotMaster botMaster = new BotMaster
            {
                BotName = Convert.ToString(HttpContext.Request.Form["BotName"]),
                Active = isAvailable,
                BotImageID = Convert.ToInt32(HttpContext.Request.Form["BotImageID"]),
                CreatedBy = 1,
                CreatedTime = DateTime.Now
            };

            List<BotSkillMaster> botSkillMasters = new List<BotSkillMaster>
            {
                new BotSkillMaster{ SkillName = Convert.ToString(HttpContext.Request.Form["SkillName"]) }
            };


            botListDetails.BotMasters = botMaster;
            botListDetails.BotSkillMasters = botSkillMasters;
            botListDetails.BotAttributeMasters = botAttributeMasters;
            botListDetails.BotIntendMasters = botIntendMasters;
            await _botStoreClient.SaveBotDetails(botListDetails);
            ResetValues();
            return RedirectToAction("Index", "BotDashboard");
        }

        [HttpPost]
        [ActionName("saveeditbot")]
        //public ActionResult SaveEditBotDetails()
        public async Task<IActionResult> SaveEditBotDetails()
        {
            BotListDetails botListDetails = new BotListDetails();

            bool.TryParse(Convert.ToString(HttpContext.Request.Form["BotStatus"]), out bool isAvailable);

            BotMaster botMaster = new BotMaster
            {
                BotID = botMasterEdit.BotID,
                BotName = Convert.ToString(HttpContext.Request.Form["BotName"]),
                Active = isAvailable,
                BotImageID = Convert.ToInt32(HttpContext.Request.Form["BotImageID"]),
                CreatedBy = 1,
                CreatedTime = DateTime.Now
            };

            List<BotSkillMaster> botSkillMasters = new List<BotSkillMaster>
            {
                new BotSkillMaster{BotID =botMasterEdit.BotID,  BotSkillID= botSkillMasterId, SkillName = Convert.ToString(HttpContext.Request.Form["SkillName"]) }
            };

            botListDetails.BotMasters = botMaster;
            botListDetails.BotSkillMasters = botSkillMasters;
            botListDetails.BotAttributeMasters = botAttributeMasters;
            botListDetails.BotIntendMasters = botIntendMasters;
            await _botStoreClient.SaveBotDetails(botListDetails);
            ResetValues();
            return RedirectToAction("Index", "BotDashboard");
        }
        private void ResetValues()
        {
            botAttributeMasters = new List<BotAttributeMaster>();
            botIntendMasters = new List<BotIntendMaster>();
            botSkillMasters = new List<BotSkillMaster>();
            botMasterEdit = new BotMaster();
            botSkillMasterId = 0;
        }
    }
}