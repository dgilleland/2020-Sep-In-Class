using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookTunes.ViewModels
{
    /// <summary>
    /// A general-purpose class for binding with a DropDownList or other such control.
    /// </summary>
    public class SelectionItem
    {
        /// <summary>
        /// Gets or sets the identifier value.
        /// </summary>
        /// <value>
        /// The identifier value.
        /// </value>
        public string IDValue { get; set; }
        /// <summary>
        /// Gets or sets the display text.
        /// </summary>
        /// <value>
        /// The display text.
        /// </value>
        public string DisplayText { get; set; }
    }
}
