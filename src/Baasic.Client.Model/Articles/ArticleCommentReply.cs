using System;

namespace Baasic.Client.Model.Articles
{
    /// <summary>
    /// Comment used to transfer data from and to controller.
    /// </summary>
    public class ArticleCommentReply : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the approve date.
        /// </summary>
        /// <value>The approve date.</value>
        public DateTime? ApproveDate { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>The author.</value>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the comment identifier.
        /// </summary>
        /// <value>The comment identifier.</value>
        public SGuid CommentId { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the flag date.
        /// </summary>
        /// <value>The flag date.</value>
        public DateTime? FlagDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is approved.
        /// </summary>
        /// <value><c>true</c> if this instance is approved; otherwise, <c>false</c>.</value>
        public bool IsApproved { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is flagged.
        /// </summary>
        /// <value><c>true</c> if this instance is flagged; otherwise, <c>false</c>.</value>
        public bool IsFlagged { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is reported.
        /// </summary>
        /// <value><c>true</c> if this instance is reported; otherwise, <c>false</c>.</value>
        public bool IsReported { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is spam.
        /// </summary>
        /// <value><c>true</c> if this instance is spam; otherwise, <c>false</c>.</value>
        public bool IsSpam { get; set; }

        /// <summary>
        /// Gets or sets the reply.
        /// </summary>
        /// <value>The reply.</value>
        public string Reply { get; set; }

        /// <summary>
        /// Gets or sets the report date.
        /// </summary>
        /// <value>The report date.</value>
        public DateTime? ReportDate { get; set; }

        /// <summary>
        /// Gets or sets the spam date.
        /// </summary>
        /// <value>The spam date.</value>
        public DateTime? SpamDate { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public Author User { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public SGuid? UserId { get; set; }

        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        /// <value>The website.</value>
        public string Website { get; set; }

        #endregion Properties
    }
}