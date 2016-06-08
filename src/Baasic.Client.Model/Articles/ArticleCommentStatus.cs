using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baasic.Client.Model.Articles
{
    /// <summary>
    /// Article service status.
    /// </summary>
    [Flags]
    public enum ArticleCommentStatus
    {
        /// <summary>
        /// The approved.
        /// </summary>
        Approved = 1,

        /// <summary>
        /// The spam.
        /// </summary>
        Spam = 2,

        /// <summary>
        /// The reported.
        /// </summary>
        Reported = 4,

        /// <summary>
        /// The flagged.
        /// </summary>
        Flagged = 8,

        /// <summary>
        /// The un approved.
        /// </summary>
        UnApproved = 16
    }
}
