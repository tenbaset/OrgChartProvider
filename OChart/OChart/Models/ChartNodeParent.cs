using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OChart.Models {
    public class ChartNodeParent : ChartNodeChild {

        public ChartNodeParent() {
            this.children = new List<ChartNodeChild>();
        }

        public IList<ChartNodeChild> children {
            get;
            set;
        }



    }
}