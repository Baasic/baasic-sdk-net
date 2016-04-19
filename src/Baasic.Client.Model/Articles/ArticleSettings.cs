using System;

namespace Baasic.Client.Model.Articles
{
    /// <summary>
    /// Article Settings used to transfer data from and to controller.
    /// </summary>
    public class ArticleSettings : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the AllowArchive.
        /// </summary>
        public bool AllowArchive { get; set; }

        /// <summary>
        /// Gets or sets the AllowComments.
        /// </summary>
        public bool AllowComments { get; set; }

        /// <summary>
        /// Gets or sets the AllowRating.
        /// </summary>
        public bool AllowRating { get; set; }

        /// <summary>
        /// Gets or sets the AllowSubscription.
        /// </summary>
        public bool AllowSubscription { get; set; }

        /// <summary>
        /// Gets or sets the AllowTags.
        /// </summary>
        public bool AllowTags { get; set; }

        /// <summary>
        /// Gets or sets the AllowUploads.
        /// </summary>
        public bool AllowUploads { get; set; }

        /// <summary>
        /// Gets or sets the AutoSaveDraft.
        /// </summary>
        public bool AutoSaveDraft { get; set; }

        /// <summary>
        /// Gets or sets the AutoSaveDraftPeriod.
        /// </summary>
        public int AutoSaveDraftPeriod { get; set; }

        /// <summary>
        /// Gets or sets the CommentAllowSubscription.
        /// </summary>
        public bool CommentAllowSubscription { get; set; }

        /// <summary>
        /// Gets or sets the CommentNotifyPostAuthor.
        /// </summary>
        public bool CommentNotifyPostAuthor { get; set; }

        /// <summary>
        /// Gets or sets the default content format.
        /// </summary>
        public string DefaultContentFormat { get; set; }

        /// <summary>
        /// Gets or sets the UploadAllowedExtensions.
        /// </summary>
        public string UploadAllowedExtensions { get; set; }

        #endregion Properties
    }
}