using Baasic.Client.Common;
using System;

namespace Baasic.Client.Model.CMS
{
    public class BlogPost : BuiltInModelBase
    {
        #region Properties

        public string Abrv { get; set; }
        public SGuid BlogId { get; set; }
        public bool CommentsAllowed { get; set; }
        public string Content { get; set; }
        public DateTime DatePublished { get; set; }
        public string Description { get; set; }
        public string Json { get; set; }
        public SGuid LanguageId { get; set; }
        public BlogOwner Owner { get; set; }
        public BlogPostStatus Status { get; set; }
        public string Title { get; set; }

        #endregion Properties
    }
}