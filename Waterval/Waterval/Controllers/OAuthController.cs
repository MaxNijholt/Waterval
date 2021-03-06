﻿using DomainModel.Models;
using Newtonsoft.Json.Linq;
using RepositoryModel.IRepository;
using RepositoryModel.Repository;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Waterval.Controllers
{
    public class OAuthController : Controller
    {
        // Constants
        public static string           CONSUMER_KEY     = "ac5baf7568250a03d1e8256b14eee24086fefef0";
        public static string           CONSUMER_SECRET  = "992b60477da323868197dcbcb9f8526eb6c8405b";

        public static string            ACCESS_TOKEN;
        public static string            ACCESS_SECRET;

        // Fields
        public static RestClient        client;
        public static string           requestSecret;

        public ActionResult Index()
        {
            client = new RestClient
            {
                BaseUrl = new Uri("https://publicapi.avans.nl/oauth/"),
                Authenticator = OAuth1Authenticator.ForRequestToken(CONSUMER_KEY, CONSUMER_SECRET, "http://localhost:6969/OAuth/OAuth")
            };
            var requestTokenRequest             = new RestRequest("request_token");
            var requestTokenResponse            = client.Execute(requestTokenRequest);

            var requestTokenResponseParameters  = HttpUtility.ParseQueryString(requestTokenResponse.Content);
            var requestToken                    = requestTokenResponseParameters["oauth_token"];
            requestSecret                       = requestTokenResponseParameters["oauth_token_secret"];

            // Redirect the user
            requestTokenRequest                 = new RestRequest("saml.php?oauth_token=" + requestToken);

            var redirectUri = client.BuildUri(requestTokenRequest);
            return Redirect(redirectUri.ToString());
        }

        public ActionResult OAuth(string oauth_token, string oauth_verifier)
        {
            client.Authenticator = OAuth1Authenticator.ForAccessToken(CONSUMER_KEY, CONSUMER_SECRET, oauth_token, requestSecret, oauth_verifier);

            var requestAccessTokenRequest       = new RestRequest("access_token");
            var requestActionTokenResponse      = client.Execute(requestAccessTokenRequest);

            var response                        = HttpUtility.ParseQueryString(requestActionTokenResponse.Content);
            ACCESS_TOKEN                        = response["oauth_token"];
            ACCESS_SECRET                       = response["oauth_token_secret"];

            client.Authenticator                = OAuth1Authenticator.ForProtectedResource(CONSUMER_KEY, CONSUMER_SECRET, ACCESS_TOKEN, ACCESS_SECRET);
            
            var link                            = new RestRequest("/people/@me");
            var linkResponse                    = client.Execute(link);

            // Putting the response into a JSON Object
            JObject obj = JObject.Parse(linkResponse.Content);
            string username = (string) obj["id"];
            bool employee = (bool) obj["employee"];

            /* 
             * VERANDEREN NAAR EMPLOYEE INPLAATSVAN !EMPLOYEE
             */
            if (!string.IsNullOrEmpty(username) && !employee) {
                IAccountRepository accounts = new AccountRepository();
                Account acc = accounts.Get(username);
                if (acc != null && !acc.isActive)
                    return RedirectToAction("Index", "Home");
                if (acc == null)
                {
                    AccountRole defaultRole = accounts.GetAcountRoles("Default");
                    if (defaultRole == null) {
                        defaultRole = new AccountRole { 
                            RoleName = "Default",
                            Description = "Standaard rol met nul rechten"
                        };
                    }

                    accounts.Create(new Account
                    {
                        Username = username,
                        isActive = true,
                        AccountRole = defaultRole
                    });
                }
                FormsAuthentication.SetAuthCookie(username, false);
            }

            

            return RedirectToAction("Index", "Home");
            //USERNAME = obj["id"].ToString();
            //string employee = obj["employee"].ToString();
            //string student  = obj["student"].ToString();
            //string nickname = obj["nickname"].ToString();
            //string email    = obj["emails"].ToString();

            //return RedirectToAction("Index", "Home");
            //return Content(
            //    "Naam : " + nickname + " -- " + 
            //    "isStudent : " + student + " -- " + 
            //    "isEmployee : " + employee + " --" + 
            //    "Email : " + email
            //);
            //return Content(linkResponse.Content);
        }

        public static bool IsUserInRole(string role) {
            if (!System.Web.HttpContext.Current.Request.IsAuthenticated) return false;
            List<AccountLaw> acc = new AccountRepository().GetAllLawsThatBelongToThatAccount(System.Web.HttpContext.Current.User.Identity.Name);
            foreach (AccountLaw al in acc)
            {
                if (al.LawName.Equals(role))
                {
                    return true;
                }
            }
            return false;
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("https://logout.sso.avans.nl/logout.html?");
        }
    }
}