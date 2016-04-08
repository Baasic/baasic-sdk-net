using System;
using System.Threading;

namespace Baasic.Client.Infrastructure.Security
{
    /// <summary>
    /// Web session token handler.
    /// </summary>
    public class WebSessionTokenHandler : ITokenHandler
    {
        #region Methods

        /// <summary>
        /// Clear token storage.
        /// </summary>
        /// <returns>True if token has been cleared, false otherwise.</returns>
        public virtual bool Clear()
        {
            if (rwl.TryEnterWriteLock(Timeout.Infinite))
            {
                try
                {
                    System.Web.HttpContext.Current.Session["token"] = null;
                }
                finally
                {
                    rwl.ExitWriteLock();
                }
                return true;
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
                    return System.Web.HttpContext.Current.Session["token"] as IAuthenticationToken;
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
                try
                {
                    System.Web.HttpContext.Current.Session["token"] = token;
                }
                finally
                {
                    rwl.ExitWriteLock();
                }
                return true;
            }
            return false;
        }

        #endregion Methods

        #region Fields

        private static ReaderWriterLockSlim rwl = new ReaderWriterLockSlim();

        #endregion Fields
    }
}