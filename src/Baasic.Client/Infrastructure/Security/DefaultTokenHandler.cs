using System;
using System.Threading;
using Baasic.Client.Common.Infrastructure.Security;

namespace Baasic.Client.Infrastructure.Security
{
    /// <summary>
    /// Default token handler.
    /// </summary>
    public class DefaultTokenHandler : ITokenHandler
    {
        #region Fields

        private static ReaderWriterLockSlim rwl = new ReaderWriterLockSlim();

        private static IAuthenticationToken token = null;

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
                try
                {
                    DefaultTokenHandler.token = null;
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
                    return token;
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
                    DefaultTokenHandler.token = token;
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
    }
}