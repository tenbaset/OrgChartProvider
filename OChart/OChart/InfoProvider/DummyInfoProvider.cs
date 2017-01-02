using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OChart.InfoProvider {
    public class DummyInfoProvider : IInfoProvider {
        public string GetRootId() {
            return "6";
        }


        public InfoProviderNode GetNode(string id) {
            switch (id) {
                case "1":
                    return new InfoProviderNode() {
                        Id = "1",
                        Name = "1 Su Miao",
                        Title = "General Manager",
                        Parent = "6",
                        Division = "Fishing",
                        Children = new HashSet<string> { "2", "3" }
                    };
                case "2":
                    return new InfoProviderNode() {
                        Id = "2",
                        Name = "2 Tie Hua",
                        Title = "senior engineer",
                        Division = "Fishing",
                        Parent = "1"
                    };
                case "3":
                    return new InfoProviderNode() {
                        Id = "3",
                        Name = "3 Hei Hei",
                        Title = "senior engineer",
                        Parent = "1",
                        Division = "Sales",
                        Children = new HashSet<string> { "4", "5" }
                    };
                case "4":
                    return new InfoProviderNode() {
                        Id = "4",
                        Name = "4 Pang Pang",
                        Title = "engineer",
                        Parent = "3",
                        Division = "Sales",
                        Children = new HashSet<string> { }
                    };
                case "5":
                    return new InfoProviderNode() {
                        Id = "5",
                        Name = "5 Xiang Xiang",
                        Title = "UE engineer",
                        Parent = "3",
                        Division = "Engineering",
                        Children = new HashSet<string> { }
                    };
                case "6":
                    return new InfoProviderNode() {
                        Id = "6",
                        Name = "6 Lao Lao",
                        Title = "general manager",
                        Parent = null,
                        Division = "Sales",
                        Children = new HashSet<string> { "1", "7" }
                    };
                case "7":
                    return new InfoProviderNode() {
                        Id = "7",
                        Name = "7 Bo Miao",
                        Title = "department engineer",
                        Parent = "6",
                        Division = "Fishing",
                        Children = new HashSet<string> { }
                    };
            }
            return null;
        }
    }
}