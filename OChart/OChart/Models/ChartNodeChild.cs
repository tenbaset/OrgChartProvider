using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OChart.Models {
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

    }
}