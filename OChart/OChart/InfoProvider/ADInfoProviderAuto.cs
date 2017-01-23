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
    public class ADInfoProviderAuto : ADInfoProvider {

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
        protected String GetGUIDForDN(string dn) {
            var cache = System.Runtime.Caching.MemoryCache.Default;
            var key = "S4D" + dn;

            var cacheValue = (string)cache[key];
            if (cacheValue == null) {
                using (var de = new DirectoryEntry($"LDAP://{dn}")) {
                    cacheValue = de.Guid.ToString();
                    cache.Add(key, cacheValue, DateTimeOffset.Now + CacheLifetime);
                }
            }
            return cacheValue;
        }


        // TODO: Move to parent class.
        public override InfoProviderNode GetNode(string id) {
            var result = new InfoProviderNode();
            // Bind by GUID
            var directoryEntry = new DirectoryEntry($"LDAP://<GUID={id}>");
            result.Id = directoryEntry.Guid.ToString();
            result.Name = (string)directoryEntry.Properties["name"].Value;

            // The DN of the manager is stored in A.D., but we
            // really want the samAccountName for said manager.
            var managerDn = (string)directoryEntry.Properties["manager"].Value;
            result.Parent = string.IsNullOrEmpty(managerDn) ? null : GetGUIDForDN(managerDn).ToString();

            result.Title = (string)directoryEntry.Properties["title"].Value;
            result.Office = (string)directoryEntry.Properties["physicalDeliveryOfficeName"].Value;

            var directReports = directoryEntry.Properties["directReports"];
            if (directReports != null) {
                foreach (string child in directReports) {
                    var childGUID = GetGUIDForDN(child);
                    result.Children.Add(childGUID);
                }
            }

            //    result.PhotoURL = "discover exchange";

            return result;
        }

        // NB read <<https://msdn.microsoft.com/en-us/library/ms677645(v=vs.85).aspx>> and
        // <<https://msdn.microsoft.com/en-us/library/ms677985(v=vs.85).aspx>> for how to handle the blasted
        // GUIDs.

        /// <summary>
        /// Gets the root id - the GUID of the currently authenticated user
        /// </summary>
        /// <returns></returns>
        public override string GetRootId() {
            if (HttpContext.Current.User.Identity.IsAuthenticated) {
                // Code take from http://stackoverflow.com/questions/315403/getting-authenticate-ad-users-objectguid-from-asp-net

                var userIdentity = HttpContext.Current.User.Identity;
                var userId = userIdentity.Name.Substring(userIdentity.Name.IndexOf("\\") + 1);

                // Start at the top level domain
                var mySearcher = new DirectorySearcher();

                // Build a filter for just the user
                mySearcher.Filter = $"(&(anr={userId})(objectClass=user))";

                // Get the search result ...
                //... and then get the AD entry that goes with it
                // The Guid property is the objectGuid
                return mySearcher.FindOne().GetDirectoryEntry().Guid.ToString();
            } else {
                return string.Empty;
            }
        }
    }
}
