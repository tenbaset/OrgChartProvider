using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OChart.InfoProvider {

    /// <summary>
    /// Wraps an IInfoProvider implementation with storing data in the ASP.Net caching
    /// engine, with a time-out (30 minute by default).
    /// </summary>

    public class InfoProviderCacheLayer : IInfoProvider {

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
        public InfoProviderCacheLayer(IInfoProvider innerProvider) {
            this.InnerProvider = innerProvider;
        }


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

        string IInfoProvider.GetRootId() {
            var cache = System.Runtime.Caching.MemoryCache.Default;
            var key = "INodeRootId" ;

            var cacheValue = (string)cache[key];
            if (cacheValue == null) {
                cacheValue = InnerProvider.GetRootId();
                cache.Add(key, cacheValue, DateTimeOffset.Now + new TimeSpan(hours: 0, minutes: 30, seconds: 0));
            }
            return cacheValue;
        }
    }
}