using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OChart.InfoProvider {
    public class DBInfoProvider : IInfoProvider {
        InfoProviderNode IInfoProvider.GetNode(string id) {
            // connect to database
            // select * from person where Id = id
            // return node
            throw new NotImplementedException();
        }

        string IInfoProvider.GetRootId() {

            // connect to database
            // find node with no parent
            // return that node id
            throw new NotImplementedException();
        }
    }
}