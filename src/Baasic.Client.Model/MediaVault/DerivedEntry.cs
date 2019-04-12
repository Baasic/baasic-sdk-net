using Baasic.Client.Common;

namespace Baasic.Client.Model.MediaVault
{
    /// <summary>
    /// Derived entry model.
    /// </summary>
    /// <seealso cref="Baasic.Client.Model.BuiltInModelBase" />
    public class DerivedEntry : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the media vault entry identifier.
        /// </summary>
        public SGuid FileEntryId { get; set; }

        /// <summary>
        /// Gets or sets the size of the file.
        /// </summary>
        public long? FileSize { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets the meta data.
        /// </summary>
        public object MetaData { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public int? Width { get; set; }

        #endregion Properties
    }
}