using Baasic.Client.Common;
using Baasic.Client.Model.Profile;

namespace Baasic.Client.Model.CMS
{
    /// <summary>
    /// Blog model.
    /// </summary>
    /// <seealso cref="Baasic.Client.Model.BuiltInModelBase" />
    public class Blog : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the abrv.
        /// </summary>
        /// <value>The abrv.</value>
        public string Abrv { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        public Language Language { get; set; }

        /// <summary>
        /// Gets or sets the language identifier.
        /// </summary>
        /// <value>The language identifier.</value>
        public SGuid LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the blog owner.
        /// </summary>
        /// <value>The owner.</value>
        public BlogOwner Owner { get; set; }

        /// <summary>
        /// Gets or sets the owner identifier.
        /// </summary>
        /// <value>The owner identifier.</value>
        public SGuid OwnerId { get; set; }

        #endregion Properties
    }
}