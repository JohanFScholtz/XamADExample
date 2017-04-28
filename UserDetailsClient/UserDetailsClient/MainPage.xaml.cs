using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace UserDetailsClient
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            // let's see if we have a user in our belly already
            try
            {
                if (App.PCA.Users.Count() > 0)
                {
                    AuthenticationResult ar = await App.PCA.AcquireTokenSilentAsync(App.Scopes, App.PCA.Users.First());
                    UpdateUserInfo(ar.IdToken);
                    btnSignInSignOut.Text = "Sign out";
                }
            }
            catch
            {
                // doesn't matter, we go in interactive more
                btnSignInSignOut.Text = "Sign in";
            }
        }
        async void OnSignInSignOut(object sender, EventArgs e)
        {
            try
            {
                if (btnSignInSignOut.Text == "Sign in")
                {
                    //TODO: Should be able to use this overload
                    //AuthenticationResult ar = await App.PCA.AcquireTokenAsync(App.Scopes, App.UiParent);
                    AuthenticationResult ar = await App.PCA.AcquireTokenAsync(App.Scopes, App.PCA.Users.FirstOrDefault(), UIBehavior.Consent, string.Empty, null, App.Authority, App.UiParent);
                    UpdateUserInfo(ar.IdToken);
                    btnSignInSignOut.Text = "Sign out";
                }
                else
                {
                    foreach (var user in App.PCA.Users)
                    {
                        App.PCA.Remove(user);
                    }
                    slUser.IsVisible = false;
                    btnSignInSignOut.Text = "Sign in";
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert($"Exception:", ex.ToString(), "Dismiss");
            }
        }
        public void UpdateUserInfo(string idToken)
        {
            /*
            //TODO: Validate the token
            // Extract user info from id_token
            var jwt = JwtSecurityToken(idToken);   
            slUser.IsVisible = true;
            lblDisplayName.Text = jwt.Claims.FirstOrDefault("displayName")?.ToString();
            lblGivenName.Text = jwt.Claims.FirstOrDefault("givenName")?.ToString();
            lblId.Text = jwt.Claims.FirstOrDefault("id")?.ToString();               
            lblSurname.Text = jwt.Claims.FirstOrDefault("surname")?.ToString();
            lblUserPrincipalName.Text = jwt.Claims.FirstOrDefault("userPrincipalName")?.ToString();
            */

        }
        public async void CallApi(string authToken)
        {
            //get data from API
            HttpClient client = new HttpClient();
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, App.ApiEndpoint);
            message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
            HttpResponseMessage response = await client.SendAsync(message);
            string responseString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert($"Response from API {App.ApiEndpoint}", responseString, "Dismiss");

            }
            else
            {
                await DisplayAlert("Something went wrong with the API call", responseString, "Dismiss");
            }
        }

        async void EditProfile(object sender, EventArgs e)
        {
            // Call EditProfile PublicClientApp to invoke EditProfile UI
            AuthenticationResult ar = await App.PCA.AcquireTokenAsync(App.Scopes, App.PCA.Users.First(), UIBehavior.Consent, string.Empty, null, App.AuthorityEditProfile, App.UiParent);
            UpdateUserInfo(ar.IdToken);
        }
    }

    
}
