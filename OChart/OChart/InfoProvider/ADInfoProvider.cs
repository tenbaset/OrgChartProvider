using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OChart.InfoProvider {

    /// <summary>
    /// Example AD provider
    /// </summary>
    public class ADInfoProvider : IInfoProvider {
        //<https://social.msdn.microsoft.com/Forums/vstudio/en-US/729d1214-37f5-4330-9208-bc4d9d695ad0/querying-adctive-directory-with-ldap-in-c?forum=netfxbcl>

        public InfoProviderNode GetNode(string id) {
            using (var mySearcher = new System.DirectoryServices.DirectorySearcher()) {
                mySearcher.Filter = ("(objectClass=user)(");
                using (var results = mySearcher.FindAll()) {
                    // wibble wibble wibble

                }
            }
            throw new NotImplementedException();
        }

        public string GetRootId() {
            var mySearcher = new System.DirectoryServices.DirectorySearcher();
            mySearcher.Filter = ("(objectClass=user)");

            throw new NotImplementedException();
        }
    }
}