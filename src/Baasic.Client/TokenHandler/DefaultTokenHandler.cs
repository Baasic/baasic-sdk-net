using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Baasic.Client.TokenHandler
{
    public class DefaultTokenHandler : ITokenHandler
    {
        private static string token = null;
        private static ReaderWriterLockSlim rwl = new ReaderWriterLockSlim();

        public virtual string Get()
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
        public virtual bool Save(string token)
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
    }
}
