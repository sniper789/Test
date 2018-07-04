using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Owin;
using Vidly.Models;

namespace Vidly
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager)),

                    OnApplyRedirect = ctx =>
                    {
                        if (!IsAjaxRequest(ctx.Request))
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                    }
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "2181192531896011",
            //   appSecret: "cbc80926312e75363d50ae4b3179ba74"
            //   );

            //var x = new FacebookAuthenticationOptions();
            //x.AppId = "2181192531896011";
            //x.AppSecret = "cbc80926312e75363d50ae4b3179ba74";
            ////x.Scope.Add("email");
            //x.Scope.Add("public_profile");
            //x.UserInformationEndpoint = "https://graph.facebook.com/v2.4/me?fields=id,name,email,first_name,last_name";

            //x.Provider = new FacebookAuthenticationProvider()
            //{
            //    OnAuthenticated =  async context =>
            //    {
            //        //Get the access token from FB and store it in the database and
            //        //use FacebookC# SDK to get more information about the user
            //        context.Identity.AddClaim(new System.Security.Claims.Claim("FacebookAccessToken", context.AccessToken));
            //        context.Identity.AddClaim(new System.Security.Claims.Claim("urn:facebook:name", context.Name));
            //        context.Identity.AddClaim(new System.Security.Claims.Claim("urn:facebook:email", context.Email));
            //    }
            //};
            //x.SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie;
            //app.UseFacebookAuthentication(x);


            var x = new FacebookAuthenticationOptions
            {
                AppId = "2181192531896011",
                AppSecret = "cbc80926312e75363d50ae4b3179ba74",

                BackchannelHttpHandler = new FacebookBackChannelHandler()
            };
            x.Scope.Add("email");
            app.UseFacebookAuthentication(x);

            //app.UseFacebookAuthentication(new Microsoft.Owin.Security.Facebook.FacebookAuthenticationOptions()
            //    {
            //        AppId = "2181192531896011",
            //        AppSecret = "cbc80926312e75363d50ae4b3179ba74"
            //    }
            //);

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "61065139928-bjfkuhv8metftg89pu3egmglv1i8kutb.apps.googleusercontent.com",
                ClientSecret = "I5xZRKONNQ18ubmd9XFEyXfc"
            });
        }

        /// <summary>
        /// Checks if request to web api (this function should not be here!!! Must be refactored in a helper class!!!
        /// https://stackoverflow.com/questions/20149750/unauthorised-webapi-call-returning-login-page-rather-than-401/20151805
        /// 
        /// Some interesting links:
        /// https://www.codeproject.com/Articles/655086/Handling-authentication-specific-issues-for-AJAX-c
        /// https://brockallen.com/2013/10/27/using-cookie-authentication-middleware-with-web-api-and-401-response-codes/
        /// https://thuru.net/2015/07/30/custom-authorization-filter-returns-200-ok-during-authorization-failure-in-web-api-mvc/
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static bool IsAjaxRequest(IOwinRequest request)
        {
            string apiPath = System.Web.VirtualPathUtility.ToAbsolute("~/api/");
            return request.Uri.LocalPath.StartsWith(apiPath);
        }
    }
}

public class FacebookOauthResponse
{
    public string access_token { get; set; }
    public string token_type { get; set; }
    public int expires_in { get; set; }
}

public class FacebookBackChannelHandler : System.Net.Http.HttpClientHandler
{
    protected override async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> SendAsync(System.Net.Http.HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
    {
        var result = await base.SendAsync(request, cancellationToken);
        if (!request.RequestUri.AbsolutePath.Contains("access_token"))
            return result;

        // For the access token we need to now deal with the fact that the response is now in JSON format, not form values. Owin looks for form values.
        var content = await result.Content.ReadAsStringAsync();
        var facebookOauthResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<FacebookOauthResponse>(content);

        var outgoingQueryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
        outgoingQueryString.Add(nameof(facebookOauthResponse.access_token), facebookOauthResponse.access_token);
        outgoingQueryString.Add(nameof(facebookOauthResponse.expires_in), facebookOauthResponse.expires_in + string.Empty);
        outgoingQueryString.Add(nameof(facebookOauthResponse.token_type), facebookOauthResponse.token_type);
        var postdata = outgoingQueryString.ToString();

        var modifiedResult = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK)
        {
            Content = new System.Net.Http.StringContent(postdata)
        };

        return modifiedResult;
    }
}