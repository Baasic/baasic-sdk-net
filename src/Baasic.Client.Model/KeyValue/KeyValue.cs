using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [Required(ErrorMessage = "Key is required")]
        [StringLength(250)]
        public virtual string Key
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the KeyValue item store value.
        /// </summary>
        [Required(ErrorMessage = "Value is required")]
        public virtual string Value
        {
            get;
            set;
        }
    }
}
