using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;

namespace OChart.InfoProvider {

    /// <summary>
    /// Example AD provider, designed to be fully auto-configuring by
    /// starting with the user identity.  NB: If you cache this, I recommend
    /// using the InfoProviderCacheLayerNoRoot instead of the normal cache layer.
    /// </summary>
    public class ADInfoProviderAuto : IInfoProvider {

        //<https://social.msdn.microsoft.com/Forums/vstudio/en-US/729d1214-37f5-4330-9208-bc4d9d695ad0/querying-adctive-directory-with-ldap-in-c?forum=netfxbcl>

        /// <summary>
        /// Length of time for a cache entry
        /// </summary>
        protected TimeSpan CacheLifetime = new TimeSpan(minutes: 30, hours: 0, seconds: 0);

        /// <summary>
        /// Gets the samAccountName for a DN, but with an internal 30 minute cache
        /// </summary>
        /// <param name="dn"></param>
        /// <returns></returns>
        protected String GetNativeGuidForDN(string dn) {
            var cache = System.Runtime.Caching.MemoryCache.Default;
            var key = "S4D" + dn;

            var cacheValue = (string)cache[key];
            if (cacheValue == null) {
                using (var de = new DirectoryEntry(dn)) {
                    cacheValue = de.NativeGuid;
                    cache.Add(key, cacheValue, DateTimeOffset.Now + CacheLifetime);
                }
            }
            return cacheValue;
        }

        public InfoProviderNode GetNode(string id) {
            var result = new InfoProviderNode();

            using (var mySearcher = new System.DirectoryServices.DirectorySearcher()) {
                mySearcher.Filter = $"(objectGUID={id})";
                var adSearchResult = mySearcher.FindOne()?.GetDirectoryEntry();
                if ((adSearchResult == null)) {
                    return null;

                }
                result.Id = adSearchResult.NativeGuid;
                result.Name = "name goes here";

                // The DN of the manager is stored in A.D., but we
                // really want the samAccountName for said manager.
                var managerDn = (string)adSearchResult?.Properties?["manager"]?[0];
                result.Parent = string.IsNullOrEmpty(managerDn) ? null : GetNativeGuidForDN(managerDn).ToString();

                result.Title = "title";
                result.Office = "office";
                result.PhotoURL = "discover exchange";
            }

            return result;

        }

        /// <summary>
        /// Gets the root id - the NativeGUID of the currently authenticated user
        /// </summary>
        /// <returns></returns>
        public string GetRootId() {
            if (HttpContext.Current.User.Identity.IsAuthenticated) {
                // Code take from http://stackoverflow.com/questions/315403/getting-authenticate-ad-users-objectguid-from-asp-net

                var userIdentity = HttpContext.Current.User.Identity;

                //Split the username into domain and userid parts
                var domainName = userIdentity.Name.Substring(0, userIdentity.Name.IndexOf("\\"));
                var userId = userIdentity.Name.Substring(userIdentity.Name.IndexOf("\\") + 1);

                // Start at the top level domain
                var entry = new DirectoryEntry(domainName);
                var mySearcher = new DirectorySearcher(entry);

                // Build a filter for just the user
                mySearcher.Filter = $"(&(anr={userId})(objectClass=user))";

                // Get the search result ...
                //... and then get the AD entry that goes with it
                // The Guid property is the objectGuid
                return mySearcher.FindOne().GetDirectoryEntry().NativeGuid;
            } else {
                return string.Empty;
            }
        }
    }
}
