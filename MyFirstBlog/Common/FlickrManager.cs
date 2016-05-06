using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlickrNet;
namespace MyFirstBlog.Common
{
    public class FlickrManager
    {
        public const string ApiKey = "62a8f32b7433fa31d63413835d38750f";
        public const string SharedSecret = "300579dd7f10c323";
        public string FilckerUpload(string url, string title, string description, string tags)
        {
            OAuthAccessToken accessToken = new OAuthAccessToken();
            accessToken.FullName = "your app name";
            accessToken.Token = "get it from Flickr Website for your login";
            accessToken.TokenSecret = "get it from Flickr Website for your login";
            accessToken.UserId = "get it from Flickr Website for your login";
            accessToken.Username = "get it from Flickr Website for your login";
            FlickrManager.OAuthToken = accessToken;
            Flickr flickr = FlickrManager.GetAuthInstance();
            string FileuploadedID = flickr.UploadPicture(@url, title, description, tags, true, false, false);
            PhotoInfo oPhotoInfo = flickr.PhotosGetInfo(FileuploadedID);
            return oPhotoInfo.Small320Url;
        }


        public static Flickr GetInstance()
        {
            return new Flickr(ApiKey, SharedSecret);

        }

        public static Flickr GetAuthInstance()
        {
            var f = new Flickr(ApiKey, SharedSecret);
            if (OAuthToken != null)
            {
                f.OAuthAccessToken = OAuthToken.Token;
                f.OAuthAccessTokenSecret = OAuthToken.TokenSecret;
            }
            return f;
        }

        public static OAuthAccessToken OAuthToken
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["OAuthToken"] == null)
                {
                    return null;
                }
                var values = HttpContext.Current.Request.Cookies["OAuthToken"].Values;
                return new OAuthAccessToken
                {
                    FullName = values["FullName"],
                    Token = values["Token"],
                    TokenSecret = values["TokenSecret"],
                    UserId = values["UserId"],
                    Username = values["Username"]
                };
            }
            set
            {
                // Stores the authentication token in a cookie which will expire in 1 hour
                var cookie = new HttpCookie("OAuthToken")
                {
                    Expires = DateTime.UtcNow.AddHours(1),
                };
                cookie.Values["FullName"] = value.FullName;
                cookie.Values["Token"] = value.Token;
                cookie.Values["TokenSecret"] = value.TokenSecret;
                cookie.Values["UserId"] = value.UserId;
                cookie.Values["Username"] = value.Username;
                HttpContext.Current.Response.AppendCookie(cookie);
            }
        }
    }
}