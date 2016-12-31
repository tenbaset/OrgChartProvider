using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OChart.InfoProvider {

    /// <summary>
    /// Represents a node on the graph in a standardised fashion from the information provider
    /// layer.  This isn't exactly what is sent 'on the wire', but rather appropriate portions are
    /// as required.
    /// </summary>
    public class InfoProviderNode {

        /// <summary>
        /// Unique ID for this node - should be unique (e.g., AD GUID, row number, employee ID, SID).  Should never be null.
        /// </summary>
        public string Id {
            get;
            set;
        }

        /// <summary>
        /// Name for this node (e.g., "John Smith").
        /// </summary>
        public string Name {
            get;
            set;
        }

        /// <summary>
        /// THe job title (node content) that this node has
        /// </summary>
        public string Title {
            get;
            set;
        }

        private HashSet<string> children_ = new HashSet<string>();
        /// <summary>
        /// IDs of the children nodes of this node.  If required, the provider will be automatically
        /// asked for the details of each of these children.
        /// </summary>
        /// <remarks>
        /// Should always be a unique set and not contain duplicates.  Order is not preserved.  If there
        /// are no children, then this should be an empty set
        /// </remarks>
        public ISet<string> Children {
            get {
                return this.children_;
            }
            set {
                this.children_ = new HashSet<string>();
                this.children_.UnionWith(value);
            }
        }


        /// <summary>
        /// ID of the parent node to this one.  Should be null if there are no parents.
        /// </summary>
        public string Parent {
            get;
            set;
        }

        /// <summary>
        /// Whether this node has sibling nodes (other nodes with the same parent).  True for yes,
        /// False for no, and leave as null for unknown.
        /// </summary>
        /// <remarks>
        /// Implementations of IInfoProvider are not required to fill this field in (i.e., it can be
        /// left null), but implementations of IInfoProviderW must fill this in (i.e., cannot leave
        /// it null).
        ///
        /// If it is null, IInfoProvider will be queried for 'children of the parent' to determine
        /// whether there are siblings or not.
        /// </remarks>
        public bool? HasSiblings {
            get;
            set;
        }

        public bool HasParent {
            get {
                return this.Parent != null;
            }
        }

        public bool HasChildren {
            get {
                return this.children_.Count > 0;
            }
        }



    }
}