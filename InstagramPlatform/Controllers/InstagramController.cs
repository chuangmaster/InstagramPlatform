using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using FacebookMessenger;
using FacebookWebhook;
using FacebookWebhook.Models;
using InstagramWebhook;
using InstagramWebhook.Models;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace InstagramPlatform.Controllers
{
    public class InstagramController : ApiController
    {
        [HttpGet]
        [Route("~/api/v1/IgWebhook")]
        public HttpResponseMessage Get()
        {
            try
            {
                var queryStrings = Request.GetQueryNameValuePairs().ToDictionary(x => x.Key, x => x.Value);
                IMessengerCore messengerCore = new MessengerCore(ApiBase.CreateCredentials("6609e0f84b8b719cdbfff11ff370c2f9", "EAAh8mr5jqU4BAKzoIGOODMHcg3B4G1IKqxxLSZBrZBEXFFLr82FMPHQQ6PM0faV8SrUaki0B8hKK1sNRfbhutXZBOEFMWvZB2qA5lR68tJTNifvzg8sGkLhuBlcDgZAdgZAcQibr1ZCxKZBZAvL1mzdkbsUerVQoeVIdHhnyFAxsqlZBKydXHM8W8Un2FNVww8HtsZD", "hello"));
                if (messengerCore.Authenticator.VerifyToken(queryStrings["hub.verify_token"]))
                {
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(queryStrings["hub.challenge"])
                    };
                }
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }

        [HttpPost]
        [Route("~/api/v1/IgWebhook")]
        public async Task<HttpResponseMessage> Test()
        {
            try
            {
                var conn = ConnectionMultiplexer.Connect("fastip-rds-test.redis.cache.windows.net:6380,password=6elcfxSyOIpbIpgyxBsKgAAq8RD1wd9J49UUonLPCIk=,ssl=True,abortConnect=False,SyncTimeout=100000");
                var db = conn.GetDatabase();
                var requestBody = await Request.Content.ReadAsStringAsync();
                var fieldData = JsonConvert.DeserializeObject<WebhookModel<InstagramWebhookEntry<ChangeModel<object>>>>(requestBody);

                foreach (var e in fieldData.Entries)
                {
                    foreach (var change in e.Changes)
                    {
                        switch (change.Field)
                        {
                            case "comments":
                                var commentsTemp = JsonConvert.DeserializeObject<WebhookModel<InstagramWebhookEntry<ChangeModel<CommentValueModel>>>>(requestBody);
                                db.StringSet("comments", JsonConvert.SerializeObject(commentsTemp.Entries));
                                break;
                            case "mentions":
                                var mentionsTemp = JsonConvert.DeserializeObject<WebhookModel<InstagramWebhookEntry<ChangeModel<MentionValueModel>>>>(requestBody);
                                db.StringSet("mentions", JsonConvert.SerializeObject(mentionsTemp.Entries));
                                break;
                            case "story_insights":
                                var storyInsightsTemp = JsonConvert.DeserializeObject<WebhookModel<InstagramWebhookEntry<ChangeModel<StoryInsightsValueModel>>>>(requestBody);
                                db.StringSet("story_insights", JsonConvert.SerializeObject(storyInsightsTemp.Entries));
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}