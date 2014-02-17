using System;
using System.Web;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace Baasic.Client
{
    /// <summary>
    /// Url builder utility class used for URL manipulations.
    /// </summary>
    public class UrlBuilder : UriBuilder
    {
        #region Fields
        StringDictionary _queryString = null; 
        #endregion

        #region Properties
        /// <summary>
        /// Gets query string.
        /// </summary>
        public StringDictionary QueryString
        {
            get
            {
                if (_queryString == null)
                {
                    _queryString = new StringDictionary();                    
                }

                return _queryString;
            }
        }

        /// <summary>
        /// Gets or sets a page name.
        /// </summary>
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
        /// Gets or sets query string.
        /// </summary>
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
        #endregion

        #region Constructor overloads
        /// <summary>
        /// Url builder constructor.
        /// </summary>
        public UrlBuilder()
            : base()
        {
        }

        /// <summary>
        /// Url builder constructor.
        /// </summary>
        /// <param name="uri">Absolute Uri</param>
        public UrlBuilder(string uri)
            : base(uri)
        {
            PopulateQueryString();
        }

        /// <summary>
        /// Url builder constructor.
        /// </summary>
        /// <param name="uri">Uri object</param>
        public UrlBuilder(Uri uri)
            : base(uri)
        {
            PopulateQueryString();
        }

        /// <summary>
        /// Url builder constructor.
        /// </summary>
        /// <param name="schemeName">Scheme name</param>
        /// <param name="hostName">Host name</param>
        public UrlBuilder(string schemeName, string hostName)
            : base(schemeName, hostName)
        {
        }

        /// <summary>
        /// Url builder constructor.
        /// </summary>
        /// <param name="scheme">Scheme</param>
        /// <param name="host">Host</param>
        /// <param name="portNumber">Post number</param>
        public UrlBuilder(string scheme, string host, int portNumber)
            : base(scheme, host, portNumber)
        {
        }

        /// <summary>
        /// Url builder constructor.
        /// </summary>
        /// <param name="scheme">Scheme</param>
        /// <param name="host">Host</param>
        /// <param name="port">Port</param>
        /// <param name="pathValue">Path value</param>
        public UrlBuilder(string scheme, string host, int port, string pathValue)
            : base(scheme, host, port, pathValue)
        {
        }

        /// <summary>
        /// Url builder constructor.
        /// </summary>
        /// <param name="scheme">Scheme</param>
        /// <param name="host">Host</param>
        /// <param name="port">Port</param>
        /// <param name="path">Path</param>
        /// <param name="extraValue">Extra value</param>
        public UrlBuilder(string scheme, string host, int port, string path, string extraValue)
            : base(scheme, host, port, path, extraValue)
        {
        }

        #endregion

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

        #endregion

        #region Private methods
        private void PopulateQueryString()
        {
            string query = base.Query;

            if (query == string.Empty || query == null)
            {
                return;
            }

            if (_queryString == null)
            {
                _queryString = new StringDictionary();
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
        #endregion
    }
}
