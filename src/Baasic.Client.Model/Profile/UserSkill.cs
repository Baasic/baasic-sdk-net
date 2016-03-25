using System;

namespace Baasic.Client.Model.Profile
{
    public partial class UserSkill : BuiltInModelBase
    {
        #region Class Property Declarations

        /// <summary>
        /// Gets or sets the core skill.
        /// </summary>
        /// <value>The core skill.</value>
        public Skill Skill { get; set; }

        /// <summary>
        /// Gets or sets the skill identifier.
        /// </summary>
        /// <value>The skill identifier.</value>
        public Nullable<System.Guid> SkillId { get; set; }

        /// <summary>
        /// Gets or sets the name of the skill.
        /// </summary>
        /// <value>The name of the skill.</value>
        public System.String SkillName { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public System.Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the user profile.
        /// </summary>
        /// <value>The user profile.</value>
        public UserProfile UserProfile
        {
            get;
            set;
        }

        #endregion Class Property Declarations
    }
}