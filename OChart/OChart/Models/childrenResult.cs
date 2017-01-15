using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OChart.Models {
    /// <summary>
    /// Result of calling the 'children' rest endpoint.  Mimics the schema
    /// from the examples of OrgChart.
    /// </summary>
    public class ChildrenResult {

        public ChildrenResult() {
            this.children = new List<ChartNodeChild>();
        }

        public IList<ChartNodeChild> children {
            get;
            set;
        }
    }
}