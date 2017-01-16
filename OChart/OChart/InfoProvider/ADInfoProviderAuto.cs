using System;
using System.Collections.Generic;
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

        public InfoProviderNode GetNode(string id) {
            // TODO: Internal cache; getting manager and looking up data
            // is going to hit AD a few times.

            using (var mySearcher = new System.DirectoryServices.DirectorySearcher()) {
                // "id" needs splitting from domain

                mySearcher.Filter = $"(sAMAccountName={id})";
                var adSearchResult = mySearcher.FindOne();
                if ((adSearchResult == null)) {
                    return null;

                }
                var result = new InfoProviderNode();
                result.Id = (string)adSearchResult?.Properties?["sAMAccountName"]?[0];
                result.Name = "name goes here";
                result.Parent = "manager";
                result.Title = "title";
                result.Office = "office"
                    ;
                result.PhotoURL = "discover exchange";
                return result;
            }
        }

        public string GetRootId() {
            if (HttpContext.Current.User.Identity.IsAuthenticated) {
                return HttpContext.Current.User.Identity.Name;
            } else {
                return string.Empty;
            }
        }
    }
}
