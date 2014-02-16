using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Baasic.Client.Model
{
    /// <summary>
    /// Model base class.
    /// </summary>
    public abstract class ModelBase : IModel
    {
        /// <summary>
        /// Gets or sets the primary key.
        /// </summary>
        public object Id { get; set; }

        /// <summary>
        /// Gets or sets the Date created.
        /// </summary>
        [DataType(DataType.DateTime)]
        public System.DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the Date updated.
        /// </summary>
        [DataType(DataType.DateTime)]
        public System.DateTime DateUpdated { get; set; }        
    }
}
