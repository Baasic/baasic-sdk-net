using System;

namespace Baasic.Client.Model
{
    /// <summary>
    /// Value set used to transfer data from and to Baasic.
    /// </summary>
    public class ValueSet : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the value set description.
        /// </summary>
        public virtual string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the set name.
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value set items associated with the set.
        /// </summary>
        public ValueSetItem[] Values { get; set; }

        #endregion Properties
    }
}