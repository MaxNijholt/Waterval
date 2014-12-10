using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Waterval.Controllers
{
    public class OAuthController : Controller
    {
        // GET: OAuth
        private static RestClient client;
        public const string consumerKey = "ac5baf7568250a03d1e8256b14eee24086fefef0";
        public const string consumerSecret = "992b60477da323868197dcbcb9f8526eb6c8405b";
        public static string requestSecret;
        public ActionResult Index()
        {

            // request token
            client = new RestClient
            {
                BaseUrl = new Uri("https://publicapi.avans.nl/oauth/"),
                Authenticator = OAuth1Authenticator.ForRequestToken(consumerKey, consumerSecret, "http://localhost:1063/OAuth/OAuth")
            };
            var requestTokenRequest = new RestRequest("request_token");
            var requestTokenResponse = client.Execute(requestTokenRequest);

            var requestTokenResponseParameters = HttpUtility.ParseQueryString(requestTokenResponse.Content);
            var requestToken = requestTokenResponseParameters["oauth_token"];
            requestSecret = requestTokenResponseParameters["oauth_token_secret"];

            // redirect user
            requestTokenRequest = new RestRequest("saml.php?oauth_token=" + requestToken);

            var redirectUri = client.BuildUri(requestTokenRequest);
            return Redirect(redirectUri.ToString());
        }

        public ActionResult OAuth(string oauth_token, string oauth_verifier)
        {
            client.Authenticator = OAuth1Authenticator.ForAccessToken(
                consumerKey, consumerSecret, oauth_token, requestSecret, oauth_verifier);

            var requestAccessTokenRequest = new RestRequest("access_token");
            var requestActionTokenResponse = client.Execute(requestAccessTokenRequest);

            var requestActionTokenResponseParameters = HttpUtility.ParseQueryString(requestActionTokenResponse.Content);
            var accessToken = requestActionTokenResponseParameters["oauth_token"];
            var accessSecret = requestActionTokenResponseParameters["oauth_token_secret"];

            client.Authenticator = OAuth1Authenticator.ForProtectedResource(consumerKey, consumerSecret, accessToken, accessSecret);
            
            var link = new RestRequest("/people/@me");
            var linkResponse = client.Execute(link);

            JObject obj = JObject.Parse(linkResponse.Content);

            string employee = obj["employee"].ToString();
            string student = obj["student"].ToString();
            string nickname = obj["nickname"].ToString();
            string email = obj["emails"].ToString();

            return Content("Naam : " + nickname + " -- " + "isStudent : " + student + " -- " + "isEmployee : " + employee + " -- Email : " + email);
        }
    }
}