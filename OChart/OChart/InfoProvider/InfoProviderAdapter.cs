using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OChart.InfoProvider {
    /// <summary>
    /// Adapts a 'narrow' info provider into a 'wide' one (emulating the sibling data requests)
    /// </summary>
    public class InfoProviderAdapter : IInfoProviderW {

        public IInfoProvider NarrowProvider {
            get; set;
        }

        public InfoProviderAdapter(IInfoProvider narrow) {
            this.NarrowProvider = narrow;
        }

        // Methods that we can forward, we do.  Otherwise we emulate

        public string GetRootId() {
            return NarrowProvider.GetRootId();
        }

        public InfoProviderNode GetNode(string id) {
            var innerNode = NarrowProvider.GetNode(id);
            // As implementations of IInfoProviderW may not return a null 'have siblings', we work
            // out the value

            if (!innerNode.HasSiblings.HasValue) {
                // No parent?  ->  No siblings
                if (innerNode.Parent == null) {
                    innerNode.HasSiblings = false;
                } else {
                    var parent = NarrowProvider.GetNode(innerNode.Parent);
                    // Sanity check
                    if (!parent.Children.Contains(id)) {
                        throw new Exception($"parent of '{id}' is '{innerNode.Parent}' but that parent did not list '{id}' as a child");
                    }
                    // All children that aren't me
                    var tempSet = parent.Children.Where(p => p != id);
                    innerNode.HasSiblings = tempSet.Count() > 0;
                }
            }
            return innerNode;
        }

        public string GetParentId(string childId) {
            var thisData = this.GetNode(childId);
            return thisData.Parent;
        }
    }
}