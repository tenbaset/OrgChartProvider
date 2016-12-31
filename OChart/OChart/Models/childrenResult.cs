using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OChart.Models {
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