using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Baasic.Client
{
    /// <summary>
    /// URL builder utility class used for URL manipulations.
    /// </summary>
    public class UrlBuilder : UriBuilder
    {
        #region Fields

        private Dictionary<string, string> _queryString = null;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the name of the page.
        /// </summary>
        /// <value>The name of the page.</value>
        public string PageName
        {
            get
            {
                string path = base.Path;
                return path.Substring(path.LastIndexOf("/") + 1);
            }
            set
            {
                string path = base.Path;
                path = path.Substring(0, path.LastIndexOf("/"));
                base.Path = string.Concat(path, "/", value);
            }
        }

        /// <summary>
        /// Gets or sets any query information included in the URI.
        /// </summary>
        /// <returns>The query information included in the URI.</returns>
        public new string Query
        {
            get
            {
                GetQueryString();
                return base.Query;
            }
            set
            {
                base.Query = value;
            }
        }

        /// <summary>
        /// Gets the query string.
        /// </summary>
        /// <value>The query string.</value>
        public Dictionary<string, string> QueryString
        {
            get
            {
                if (_queryString == null)
                {
                    _queryString = new Dictionary<string, string>();
                }

                return _queryString;
            }
        }

        #endregion Properties

        #region Constructor overloads

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlBuilder" /> class.
        /// </summary>
        public UrlBuilder()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlBuilder" /> class.
        /// </summary>
        /// <param name="uri">A URI string.</param>
        public UrlBuilder(string uri)
            : base(uri)
        {
            PopulateQueryString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlBuilder" /> class.
        /// </summary>
        /// <param name="uri">An instance of the <see cref="T:System.Uri" /> class.</param>
        public UrlBuilder(Uri uri)
            : base(uri)
        {
            PopulateQueryString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlBuilder" /> class.
        /// </summary>
        /// <param name="schemeName">An Internet access protocol.</param>
        /// <param name="hostName">A DNS-style domain name or IP address.</param>
        public UrlBuilder(string schemeName, string hostName)
            : base(schemeName, hostName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlBuilder" /> class.
        /// </summary>
        /// <param name="scheme">An Internet access protocol.</param>
        /// <param name="host">A DNS-style domain name or IP address.</param>
        /// <param name="portNumber">An IP port number for the service.</param>
        public UrlBuilder(string scheme, string host, int portNumber)
            : base(scheme, host, portNumber)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlBuilder" /> class.
        /// </summary>
        /// <param name="scheme">An Internet access protocol.</param>
        /// <param name="host">A DNS-style domain name or IP address.</param>
        /// <param name="port">An IP port number for the service.</param>
        /// <param name="pathValue">The path to the Internet resource.</param>
        public UrlBuilder(string scheme, string host, int port, string pathValue)
            : base(scheme, host, port, pathValue)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlBuilder" /> class.
        /// </summary>
        /// <param name="scheme">An Internet access protocol.</param>
        /// <param name="host">A DNS-style domain name or IP address.</param>
        /// <param name="port">An IP port number for the service.</param>
        /// <param name="path">The path to the Internet resource.</param>
        /// <param name="extraValue">A query string or fragment identifier.</param>
        public UrlBuilder(string scheme, string host, int port, string path, string extraValue)
            : base(scheme, host, port, path, extraValue)
        {
        }

        #endregion Constructor overloads

        #region Public methods

        /// <summary>
        /// Gets string representation of Uri.
        /// </summary>
        /// <returns>String representation of Uri</returns>
        public new string ToString()
        {
            GetQueryString();

            return base.Uri.AbsoluteUri;
        }

        #endregion Public methods

        #region Private methods

        private void GetQueryString()
        {
            //int count = _queryString.Count;
            int count = _queryString != null ? _queryString.Count : 0;

            if (count == 0)
            {
                base.Query = string.Empty;
                return;
            }

            string[] keys = new string[count];
            string[] values = new string[count];
            string[] pairs = new string[count];

            _queryString.Keys.CopyTo(keys, 0);
            _queryString.Values.CopyTo(values, 0);

            for (int i = 0; i < count; i++)
            {
                pairs[i] = string.Concat(keys[i], "=", values[i]);
            }

            base.Query = string.Join("&", pairs);
        }

        private void PopulateQueryString()
        {
            string query = base.Query;

            if (query == string.Empty || query == null)
            {
                return;
            }

            if (_queryString == null)
            {
                _queryString = new Dictionary<string, string>();
            }

            _queryString.Clear();

            query = query.Substring(1); //remove the ?

            string[] pairs = query.Split(new char[] { '&' });
            foreach (string s in pairs)
            {
                string[] pair = s.Split(new char[] { '=' });

                _queryString[pair[0]] = (pair.Length > 1) ? pair[1] : string.Empty;
            }
        }

        #endregion Private methods
    }
}