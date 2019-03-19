﻿using Baasic.Client.Common.Infrastructure.Security;
using System;
using System.Threading;

#if NET461 || NET462

using System.Linq;
using System.Web;

#else
using Microsoft.AspNetCore.Http;
#endif

namespace Baasic.Client.Infrastructure.Security
{
    /// <summary>
    /// Web Cookie token handler.
    /// </summary>
    public class PlatformCookieTokenHandler : IPlatformTokenHandler
    {
        #region Fields

        /// <summary>
        /// The Baasic authorization header key.
        /// </summary>
        public const string HeaderKey = "Baasic_Platform_Authorization";

        private static ReaderWriterLockSlim rwl = new ReaderWriterLockSlim();

        #endregion Fields

#if !(NET461 || NET462)
        private readonly IHttpContextAccessor contextAccessor;

        public CookieTokenHandler(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }
#endif

        #region Methods

        /// <summary>
        /// Clear token storage.
        /// </summary>
        /// <returns>True if token has been cleared, false otherwise.</returns>
        public virtual bool Clear()
        {
            if (rwl.TryEnterWriteLock(Timeout.Infinite))
            {
                bool result = false;
                try
                {
#if NET461 || NET462
                    if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Response != null)
                    {
                        var cookie = new HttpCookie(HeaderKey, "");
                        cookie.HttpOnly = true;
                        cookie.Expires = DateTime.Now.AddDays(-1d);
                        cookie.Path = "/";
                        HttpContext.Current.Response.Cookies.Add(cookie);
                        result = true;
                    }
#else
                    if (contextAccessor.HttpContext?.Response != null)
                    {
                        contextAccessor.HttpContext.Response.Cookies.Append(HeaderKey, "", new CookieOptions
                        {
                            HttpOnly = true,
                            Expires = DateTime.Now.AddDays(-1d),
                            Path = "/"
                        });
                        result = true;
                    }
#endif
                }
                finally
                {
                    rwl.ExitWriteLock();
                }
                return result;
            }
            return false;
        }

        /// <summary>
        /// Gets the token from a storage.
        /// </summary>
        /// <returns>Token.</returns>
        public virtual IAuthenticationToken Get()
        {
            if (rwl.TryEnterReadLock(Timeout.Infinite))
            {
                try
                {
#if NET461 || NET462
                    if (System.Web.HttpContext.Current != null)
                    {
                        HttpCookie cookie = null;
                        if (System.Web.HttpContext.Current.Request != null && HttpContext.Current.Request.Cookies.AllKeys.Contains(HeaderKey))
                        {
                            cookie = HttpContext.Current.Request.Cookies.Get(HeaderKey);
                            if (cookie != null && String.IsNullOrEmpty(cookie.Value))
                            {
                                cookie = null;
                            }
                        }

                        if (cookie == null && System.Web.HttpContext.Current.Response != null && HttpContext.Current.Response.Cookies.AllKeys.Contains(HeaderKey))
                        {
                            cookie = HttpContext.Current.Response.Cookies.Get(HeaderKey);
                        }

                        if (cookie != null)
                        {
                            return DeserializeToken(cookie.Value);
                        }
                    }
#else
                    if (contextAccessor.HttpContext != null)
                    {
                        string cookie = null;
                        if (contextAccessor.HttpContext.Request != null && contextAccessor.HttpContext.Request.Cookies.Keys.Contains(HeaderKey))
                        {
                            cookie = contextAccessor.HttpContext.Request.Cookies[HeaderKey];
                        }

                        if (cookie == null && contextAccessor.HttpContext != null && GetCookieValueFromResponse(contextAccessor.HttpContext.Response, HeaderKey) != null)
                        {
                            cookie = GetCookieValueFromResponse(contextAccessor.HttpContext.Response, HeaderKey);
                        }

                        if (cookie != null)
                        {
                            return DeserializeToken(cookie);
                        }
                    }
#endif
                }
                finally
                {
                    rwl.ExitReadLock();
                }
            }
            return default(IAuthenticationToken);
        }

        /// <summary>
        /// Saves token to storage.
        /// </summary>
        /// <param name="token">Token to save.</param>
        /// <returns>True if token has been saved, false otherwise.</returns>
        public virtual bool Save(IAuthenticationToken token)
        {
            if (rwl.TryEnterWriteLock(Timeout.Infinite))
            {
                var result = false;
                try
                {
#if NET461 || NET462
                    if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Response != null)
                    {
                        HttpContext.Current.Response.Cookies.Remove(HeaderKey);

                        var cookie = new HttpCookie(HeaderKey, SerializeToken(token));
                        cookie.HttpOnly = true;
                        cookie.Expires = DateTime.Now.AddSeconds((token.ExpiresIn.HasValue && token.ExpiresIn.Value > 0) ? token.ExpiresIn.Value : ((token.SlidingWindow.HasValue && token.SlidingWindow.Value > 0) ? token.SlidingWindow.Value : 7200));
                        cookie.Path = "/";
                        HttpContext.Current.Response.Cookies.Add(cookie);
                        result = true;
                    }
#else
                    if (contextAccessor.HttpContext?.Response != null)
                    {
                        contextAccessor.HttpContext.Response.Cookies.Delete(HeaderKey);

                        contextAccessor.HttpContext.Response.Cookies.Append(HeaderKey, SerializeToken(token), new CookieOptions
                        {
                            HttpOnly = true,
                            Expires = DateTime.Now.AddSeconds((token.ExpiresIn.HasValue && token.ExpiresIn.Value > 0) ? token.ExpiresIn.Value : ((token.SlidingWindow.HasValue && token.SlidingWindow.Value > 0) ? token.SlidingWindow.Value : 7200)),
                            Path = "/"
                        });
                        result = true;
                    }
#endif
                }
                finally
                {
                    rwl.ExitWriteLock();
                }
                return result;
            }
            return false;
        }

        /// <summary>
        /// Deserializes the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        protected virtual IAuthenticationToken DeserializeToken(string token)
        {
            return (IAuthenticationToken)Newtonsoft.Json.JsonConvert.DeserializeObject(System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(token)), typeof(AuthenticationToken));
        }

        /// <summary>
        /// Serializes the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        protected virtual string SerializeToken(IAuthenticationToken token)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(token)));
        }

        #endregion Methods

#if !(NET461 || NET462)
        private string GetCookieValueFromResponse(HttpResponse response, string cookieName)
        {
            foreach (var headers in response.Headers.Values)
                foreach (var header in headers)
                    if (header.StartsWith(cookieName))
                    {
                        var p1 = header.IndexOf('=');
                        var p2 = header.IndexOf(';');
                        return header.Substring(p1 + 1, p2 - p1 - 1);
                    }
            return null;
        }
#endif
    }
}