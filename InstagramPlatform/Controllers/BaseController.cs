using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InstagramPlatform.Controllers
{
    public class BaseController : Controller
    {
        private string _ClientId { get; set; }

        protected string ClientId
        {
            get
            {
                if (string.IsNullOrEmpty(this._ClientId)
                    && ((IEnumerable<string>)ConfigurationManager.AppSettings.AllKeys).Contains<string>(nameof(ClientId)))
                {
                    this._ClientId = ConfigurationManager.AppSettings[nameof(ClientId)].ToString();
                }
                return this._ClientId;
            }

            set => this._ClientId = value;
        }

        private string _AppSecret { get; set; }

        protected string AppSecret
        {
            get
            {
                if (string.IsNullOrEmpty(this._AppSecret)
                    && ((IEnumerable<string>)ConfigurationManager.AppSettings.AllKeys).Contains<string>(nameof(AppSecret)))
                {
                    this._AppSecret = ConfigurationManager.AppSettings[nameof(AppSecret)].ToString();
                }
                return this._AppSecret;
            }

            set => this._AppSecret = value;
        }
    }
}