using System;

namespace Baasic.Client.Model
{
    /// <summary>
    /// Key value used to transfer data from and to Baasic.
    /// </summary>
    public class KeyValue : ModelBase
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public virtual string Key
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the KeyValue item store value.
        /// </summary>
        public virtual string Value
        {
            get;
            set;
        }
    }
}