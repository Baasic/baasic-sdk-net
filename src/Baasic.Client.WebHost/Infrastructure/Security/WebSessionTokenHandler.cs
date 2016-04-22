using System;
using System.Threading;

namespace Baasic.Client.Infrastructure.Security
{
    /// <summary>
    /// Web session token handler.
    /// </summary>
    public class WebSessionTokenHandler : ITokenHandler
    {
        #region Fields

        private static ReaderWriterLockSlim rwl = new ReaderWriterLockSlim();

        #endregion Fields

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
                    if (System.Web.HttpContext.Current.Session != null)
                    {
                        System.Web.HttpContext.Current.Session["token"] = null;
                        result = true;
                    }
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
                    if (System.Web.HttpContext.Current.Session != null)
                    {
                        return System.Web.HttpContext.Current.Session["token"] as IAuthenticationToken;
                    }
                }
                finally
                {
                    rwl.ExitReadLock();
                }
            }
            return null;
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
                    if (System.Web.HttpContext.Current.Session != null)
                    {
                        System.Web.HttpContext.Current.Session["token"] = token;
                        result = true;
                    }
                }
                finally
                {
                    rwl.ExitWriteLock();
                }
                return result;
            }
            return false;
        }

        #endregion Methods
    }
}