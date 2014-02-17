using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baasic.Client.Configuration
{
    public interface IClientConfiguration
    {
        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        string ApplicationIdentifier 
        { 
            get; 
            set; 
        }

        TimeSpan DefaultTimeout
        {
            get;
            set;
        }

        string BaseAddress
        {
            get;
            set;
        }

        string SecureBaseAddress
        {
            get;
            set;
        }

        string DefaultMediaType
        {
            get;
            set;
        }

    }
}
