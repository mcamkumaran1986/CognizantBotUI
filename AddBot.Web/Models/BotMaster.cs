using System;

namespace AddBot.Web.Models
{
    public class BotMaster
    {
        public int BotID { get; set; }
        public string BotName { get; set; }
        public bool Active { get; set; }
        public int? BotImageID { get; set; }
        public string ImagePath { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
