using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InstagramWebhook;
using InstagramWebhook.Models;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace InstagramPlatform.Controllers
{
    public class HomeController : Controller
    {
        private IDatabase _RedisDb;
        public HomeController()
        {
            var conn = ConnectionMultiplexer.Connect("fastip-rds-test.redis.cache.windows.net:6380,password=6elcfxSyOIpbIpgyxBsKgAAq8RD1wd9J49UUonLPCIk=,ssl=True,abortConnect=False,SyncTimeout=100000");
            _RedisDb = conn.GetDatabase();
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Comment()
        {
            var model = new List<InstagramWebhookEntry<ChangeModel<CommentValueModel>>>
            {
                new InstagramWebhookEntry<ChangeModel<CommentValueModel>>()
                {
                    Changes = new List<ChangeModel<CommentValueModel>>()
                }
            };
            var comments = _RedisDb.StringGet("comments");
            if (comments.HasValue)
                model = JsonConvert.DeserializeObject<List<InstagramWebhookEntry<ChangeModel<CommentValueModel>>>>(
                    comments.ToString());
            return View(model);
        }

        public ActionResult Mention()
        {
            var model = new List<InstagramWebhookEntry<ChangeModel<MentionValueModel>>>
            {
                new InstagramWebhookEntry<ChangeModel<MentionValueModel>>()
                {
                    Changes = new List<ChangeModel<MentionValueModel>>()
                }
            };
            var mentions = _RedisDb.StringGet("mentions");
            if (mentions.HasValue)
                model = JsonConvert.DeserializeObject<List<InstagramWebhookEntry<ChangeModel<MentionValueModel>>>>(
                    mentions.ToString());
            return View(model);
        }

        public ActionResult StoryInsights()
        {
            var model = new List<InstagramWebhookEntry<ChangeModel<StoryInsightsValueModel>>>
            {
                new InstagramWebhookEntry<ChangeModel<StoryInsightsValueModel>>()
                {
                    Changes = new List<ChangeModel<StoryInsightsValueModel>>()
                }
            };
            var story_insights = _RedisDb.StringGet("story_insights");
            if (story_insights.HasValue)
                model = JsonConvert.DeserializeObject<List<InstagramWebhookEntry<ChangeModel<StoryInsightsValueModel>>>>(
               story_insights.ToString());
            return View(model);
        }
    }
}
