using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Service.DTO;

namespace Services
{
    public class InstagramBasicDisplayService
    {
        /// <summary>
        /// Use code to exchange access token
        /// </summary>
        /// <param name="client_id">The client id</param>
        /// <param name="app_secret">The app secret</param>
        /// <param name="code">The facebook code</param>
        /// <param name="redirect_uri">The redirect uri</param>
        /// <returns></returns>
        public async Task<ExchangeTokenResponseDTO> ExchangeToken(string client_id, string app_secret, string code, string redirect_uri)
        {
            var requestUrl = "https://api.instagram.com/oauth/access_token";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    SetDefaultHeaders(client);
                    var formParameters = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("client_id", client_id),
                        new KeyValuePair<string, string>("app_secret", app_secret),
                        new KeyValuePair<string, string>("grant_type", "authorization_code"),
                        new KeyValuePair<string, string>("redirect_uri", redirect_uri),
                        new KeyValuePair<string, string>("code", code)
                    };
                    var formContent = new FormUrlEncodedContent(formParameters);
                    HttpResponseMessage response =
                        await client.PostAsync(requestUrl, formContent);
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<ExchangeTokenResponseDTO>(responseContent);
                    return data;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Get long lived token
        /// </summary>
        /// <param name="app_secret">The app secret</param>
        /// <param name="short_lived_token">The short lived token</param>
        /// <returns></returns>
        public async Task<ExchangeTokenResponseDTO> GetLongLivedToken(string app_secret, string short_lived_token)
        {
            var url = "https://graph.instagram.com/access_token?grant_type=ig_exchange_token&client_secret={0}&access_token={1}";
            using (var client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync(string.Format(url, app_secret, short_lived_token));
                var responseBody = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<ExchangeTokenResponseDTO>(responseBody);
                return data;
            }
        }


        /// <summary>
        /// Refresh long lived token
        /// </summary>
        /// <param name="long_lived_token">The long lived token</param>
        /// <returns></returns>
        public async Task<ExchangeTokenResponseDTO> RefreshLongLivedToken(string long_lived_token)
        {
            var url = "https://graph.instagram.com/refresh_access_token?grant_type=ig_refresh_token&access_token={0}";
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(string.Format(url, long_lived_token));
                var responseBody = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<ExchangeTokenResponseDTO>(responseBody);
                return data;
            }
        }

        /// <summary>
        /// Get user profile data
        /// </summary>
        /// <param name="accessToken">The access token</param>
        /// <returns></returns>
        public async Task<UserProfileResponseDTO> GetUserProfile(string accessToken)
        {
            var requestUrl = "https://graph.instagram.com/me?fields=id,username&access_token={0}";
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Format(requestUrl, accessToken));
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<UserProfileResponseDTO>(responseBody);
                    return data;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Get user media edge data
        /// </summary>
        /// <param name="accessToken">The access token</param>
        /// <returns></returns>
        public async Task<UserMediaEdgeResponseDTO> GetUserMediaEdge(string accessToken)
        {
            var requestUrl = "https://graph.instagram.com/me/media?fields=id,caption&access_token={0}";
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Format(requestUrl, accessToken));
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<UserMediaEdgeResponseDTO>(responseBody);
                    return data;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Get user media data
        /// fields refer https://developers.facebook.com/docs/instagram-basic-display-api/reference/media#fields
        /// </summary>
        /// <param name="accessToken">The access token</param>
        /// <returns></returns>
        public async Task<UserMediaResponseDTO> GetMedia(string accessToken, string mediaId, string fields = "")
        {
            if (string.IsNullOrEmpty(fields))
                fields = "id,media_type,media_url,username,timestamp,caption,thumbnail_url,permalink";
            var requestUrl = "https://graph.instagram.com/{0}?fields={1}&access_token={2}";
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Format(requestUrl, mediaId, fields, accessToken));
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<UserMediaResponseDTO>(responseBody);
                    return data;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Get user media data
        /// fields refer https://developers.facebook.com/docs/instagram-basic-display-api/reference/media#children
        /// </summary>
        /// <param name="accessToken">The access token</param>
        /// <returns></returns>
        public async Task<MediaChildrenResponseDTO> GetAlbumChildren(string accessToken, string mediaId)
        {

            var requestUrl = "https://graph.instagram.com/{0}/children?access_token={1}";
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Format(requestUrl, mediaId, accessToken));
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<MediaChildrenResponseDTO>(responseBody);
                    return data;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        private static void SetDefaultHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
