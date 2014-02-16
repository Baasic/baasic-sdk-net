using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baasic.Client.Model
{
    /// <summary>
    /// Collection model interface.
    /// </summary>
    public interface ICollectionModel<T> where T : IModel
    {
        /// <summary>
        /// Gets or sets the total number of records.
        /// </summary>
        int TotalRecords { get; set; }

        /// <summary>
        /// Gets or sets the number of records per page.
        /// </summary>
        int RecordsPerPage { get; set; }

        /// <summary>
        /// Gets or sets the number of current page.
        /// </summary>
        int Page { get; set; }

        /// <summary>
        /// Gets or sets the search query.
        /// </summary>
        string SearchQuery { get; set; }

        /// <summary>
        /// Gets or sets the sort expression.
        /// </summary>
        string Sort { get; set; }

        /// <summary>
        /// Gets or sets the embed list.
        /// </summary>
        string Embed { get; set; }

        /// <summary>
        /// Gets or sets the REST model items.
        /// </summary>
        List<T> Item { get; set; }
    }
}
