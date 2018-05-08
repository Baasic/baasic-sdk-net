using Baasic.Client.Common;

namespace Baasic.Client.Model.Dynamic
{
    /// <summary>
    /// Dynamic schema model.
    /// </summary>
    public partial class DynamicSchema : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enforce schema validation].
        /// </summary>
        /// <value><c>true</c> if [enforce schema validation]; otherwise, <c>false</c>.</value>
        public bool EnforceSchemaValidation { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>The owner.</value>
        public Owner Owner { get; set; }

        /// <summary>
        /// Gets or sets the owner identifier.
        /// </summary>
        /// <value>The owner identifier.</value>
        public SGuid OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the schema.
        /// </summary>
        /// <value>The schema.</value>
        public string Schema { get; set; }

        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        /// <value>The time stamp.</value>
        public System.DateTime TimeStamp { get; set; }

        #endregion Properties
    }
}