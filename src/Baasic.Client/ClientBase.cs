using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baasic.Client
{
    /// <summary>
    /// Base Module Client.
    /// </summary>
    public abstract class ClientBase
    {
        #region Fields
        /// <summary>
        /// Maximum number of results.
        /// <para>
        /// Default value is 100.
        /// </para>
        /// </summary>
        public const int MaxNumberOfResults = 10;

        /// <summary>
        /// Default page.
        /// <para>
        /// Default value is 1.
        /// </para>
        /// </summary>
        public const int DefaultPage = 1;

        /// <summary>
        /// Default sorting.
        /// <para>
        /// Default value is <see cref="String.Empty"/>.
        /// </para>
        /// </summary>
        public const string DefaultSorting = "";

        /// <summary>
        /// Default embed.
        /// <para>
        /// Default value is <see cref="String.Empty"/>.
        /// </para>
        /// </summary>
        public const string DefaultEmbed = "";

        /// <summary>
        /// Default search query.
        /// <para>
        /// Default value is <see cref="String.Empty"/>.
        /// </para>
        /// </summary>
        public const string DefaultSearchQuery = "";
        #endregion


        #region Properties
        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        public string ApplicationIdentifier { get; protected set; }

        protected abstract string ModuleRelativePath { get; }

        private string _baseAddress;

        public string BaseAddress
        {
            get
            {
                return _baseAddress;
            }
            set
            {
                _baseAddress = value;
            }
        }

        private string _defaultMediaType;

        public string DefaultMediaType
        {
            get
            {
                return _defaultMediaType;
            }
            set { _defaultMediaType = value; }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="applicationIdentifier">Application identifier.</param>
        public ClientBase(string applicationIdentifier)
        {
            ApplicationIdentifier = applicationIdentifier;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="baseAddress">Baasic API address.</param>
        /// <param name="applicationIdentifier">Application identifier.</param>
        public ClientBase(string baseAddress, string applicationIdentifier)
        {
            ApplicationIdentifier = applicationIdentifier;
            BaseAddress = baseAddress;
        }
        #endregion
    }
}
