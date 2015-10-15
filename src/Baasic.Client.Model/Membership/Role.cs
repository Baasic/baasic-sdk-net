using System;

namespace Baasic.Client.Model.Membership
{
    /// <summary>
    /// Role used to transfer data from and to controller.
    /// </summary>
    public class Role : ModelBase
    {
        #region Properties

        /// <summary>
        /// Role description.
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Role name.
        /// </summary>
        public virtual string Name { get; set; }

        #endregion Properties
    }
}