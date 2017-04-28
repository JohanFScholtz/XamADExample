using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace UserDetailsClient
{
    public class App : Application
    {
        public static PublicClientApplication PCA = null;
       
        // Azure AD B2C Coordinates
        public static string ClientID = "90c0fe63-bcf2-44d5-8fb7-b8bbc0b29dc6";
        public static string PolicySignUpSignIn = "b2c_1_susi";
        public static string PolicyEditProfile = "b2c_1_edit_profile";

        public static string AuthorityBase = "https://login.microsoftonline.com/tfp/fabrikamb2c.onmicrosoft.com/";
        public static string Authority = $"{AuthorityBase}{PolicySignUpSignIn}";
        public static string AuthorityEditProfile = $"{AuthorityBase}{PolicyEditProfile}";

        public static string[] Scopes = { "https://fabrikamb2c.onmicrosoft.com/demoapi/demo.read" };
        public static string ApiEndpoint = "https://aadb2cplayground.azurewebsites.net/api/Tasks";
 
        // Azure AD v2 Coordinates
        /*
        public static string ClientID = "a7d8cef0-4145-49b2-a91d-95c54051fa3f";
        public static string Authority = string.Empty;
        public static string AuthorityEditProfile = string.Empty;
        public static string[] Scopes = { "User.Read" };
        public static string ApiEndpoint = "https://graph.microsoft.com/v1.0/me";
        */

        public static string Username = string.Empty;
        public App()
        {
            // string.IsNullOrEmpty(Authority) is a temp thing just to allow this app to work
            // with both Azure AD v2 and Azure AD B2C
            PCA = string.IsNullOrEmpty(Authority) ? new PublicClientApplication(ClientID) : new PublicClientApplication(ClientID, Authority);
            MainPage = new NavigationPage(new MainPage());        
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
