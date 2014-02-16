using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baasic.Client.Model
{
    /// <summary>
    /// Collection model.
    /// </summary>
    public class CollectionModelBase<T> : ICollectionModel<T>
        where T : IModel
    {
        #region Properties
        /// <summary>
        /// Gets or sets the total number of records.
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Gets or sets the number of records per page.
        /// </summary>
        public int RecordsPerPage { get; set; }

        /// <summary>
        /// Gets or sets the number of current page.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the search query.
        /// </summary>
        public string SearchQuery { get; set; }

        /// <summary>
        /// Gets or sets the sort expression.
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// Gets or sets the embed list.
        /// </summary>
        public string Embed { get; set; }

        /// <summary>
        /// Gets or sets the REST model items.
        /// </summary>
        public List<T> Item { get; set; } 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        public CollectionModelBase()
        {
            Item = new List<T>();
        } 
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="items">Collection of REST items.</param>
        public CollectionModelBase(IEnumerable<T> items)
        {
            Item = new List<T>(items);
        }        
        #endregion
    }
}
