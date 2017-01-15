using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OChart.Models {

    /// <summary>
    /// JSON serialised class that represents a 'child' node from a current node.
    /// </summary>
    /// <remarks>
    /// This is the same structure as a 'parent' view of the same node, but doesn't include children
    /// of this node. Should the orgchart.js need that information, it'll call one of the other
    /// methods to get that.
    /// </remarks>
    public class ChartNodeChild {
        /// <summary>
        /// Node ID for further reference
        /// </summary>
        public string id {
            get; set;
        }

        public string name {
            get;
            set;
        }

        public string title {
            get;
            set;
        }
        public string relationship {
            get;
            set;

        }

        /// <summary>
        /// CSS Class name that is fed from the department
        /// </summary>
        public string className {
            get;
            set;
        }

        public string photourl {
            get;
            set;
        }

        public string division {
            get;
            set;
        }

        public string office {
            get; set;
        }

    }
}