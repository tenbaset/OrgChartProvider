using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OChart.Models {

    /// <summary>
    /// Main JSON serialised 'parent' based view of a node (i.e., includes children data)
    /// </summary>
    /// <remarks>As you can see, a subclass of ChartNodeChild so it inherits all attributes that a
    /// 'child' has, and simply adds the 'children' property</remarks>
    public class ChartNodeParent : ChartNodeChild {

        /// <summary>
        /// Construct new ChartNodeParent
        /// </summary>
        public ChartNodeParent() {
            this.children = new List<ChartNodeChild>();
        }

        /// <summary>
        /// Children of this node - doesn't have grandchildren data.
        /// </summary>
        public IList<ChartNodeChild> children {
            get;
            set;
        }



    }
}