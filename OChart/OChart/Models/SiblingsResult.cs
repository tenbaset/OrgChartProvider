using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OChart.Models {

    /// <summary>
    /// JSON serialising result of calling for siblings from a node
    /// </summary>
    public class SiblingsResult {
        public SiblingsResult() {
            this.siblings = new List<ChartNodeChild>();
        }

        public IList<ChartNodeChild> siblings {
            get;
            set;
        }
    }
}