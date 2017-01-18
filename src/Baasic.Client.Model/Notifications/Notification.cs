using Newtonsoft.Json.Linq;

using System;

namespace Baasic.Client.Model
{
    /// <summary>
    /// Notification used to transfer data from and to Baasic.
    /// </summary>
    public class Notification : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the channels.
        /// </summary>
        public string[] Channels { get; set; }

        /// <summary>
        /// Gets or sets the template context.
        /// </summary>
        public JObject TemplateContext { get; set; }

        /// <summary>
        /// Gets or sets the template unique identifier .
        /// </summary>
        public string TemplateId { get; set; }

        #endregion Properties
    }
}