using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OChart.InfoProvider {

    /// <summary>
    /// Wraps an IInfoProvider implementation with storing data in the ASP.Net caching engine, with
    /// a time-out (30 minute by default), but does not wrap the GetRoot method.  This is useful for
    /// when the root ID is dynamic but the rest of the data is not.
    /// </summary>
    /// <remarks>
    /// Note that the InfoProvider may trigger a few 'redundant' queries (e.g., to get siblings of a
    /// node, it will automatically get the parent of a node and then look at the children.)  To
    /// combat any inefficiencies, this will cache results.
    /// </remarks>

    public class InfoProviderCacheLayerNoRoot : IInfoProvider {

        /// <summary>
        /// The inner provider that is called when the desired value isn't cached
        /// </summary>
        public IInfoProvider InnerProvider {
            get; set;
        }

        private TimeSpan CacheLifetime_ = new TimeSpan(hours: 0, minutes: 30, seconds: 0);

        /// <summary>
        /// How long newly added items should last for; defaults to 30 minutes.
        /// </summary>
        public TimeSpan CacheLifetime {
            get {
                return CacheLifetime_;
            }
            set {
                CacheLifetime_ = value;
            }
        }

        /// <summary>
        /// Create a Cache Layer
        /// </summary>
        /// <param name="innerProvider">Provider to wrap in a cache</param>
        public InfoProviderCacheLayerNoRoot(IInfoProvider innerProvider) {
            this.InnerProvider = innerProvider;
        }

        /// <summary>
        /// Gets a node, either from the cache or from the inner provider
        /// </summary>
        /// <param name="id">Node ID</param>
        /// <returns>Node</returns>
        InfoProviderNode IInfoProvider.GetNode(string id) {
            var cache = System.Runtime.Caching.MemoryCache.Default;
            var key = "INode" + id;

            var cacheValue = (InfoProviderNode)cache[key];
            if (cacheValue == null) {
                cacheValue = InnerProvider.GetNode(id);
                cache.Add(key, cacheValue, DateTimeOffset.Now + CacheLifetime);
            }
            return cacheValue;
        }

                /// <summary>
        /// Gets the root node id exclusively from the inner provider
        /// </summary>
        /// <returns></returns>
        string IInfoProvider.GetRootId() {
            return InnerProvider.GetRootId();
        }
    }
}
