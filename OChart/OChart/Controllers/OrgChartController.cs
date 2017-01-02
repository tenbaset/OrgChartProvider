using OChart.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


// using attribute routing https://www.asp.net/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2

namespace OChart.Controllers {
    public class OrgChartController : ApiController {

        public IInfoProviderW infoProvider;

        public OrgChartController() {
            // aweful
            var p = new InfoProvider.DummyInfoProvider();
            this.SetInfoProvider(p);
        }

        /// <summary>
        /// Sets the info provider, automatically upgrading an IInfoProvider to a IInfoProviderW by
        /// using an adapter if required.
        /// </summary>
        /// <param name="provider"></param>
        public void SetInfoProvider(IInfoProvider provider) {
            if (infoProvider is IInfoProviderW) {
                this.infoProvider = provider as IInfoProviderW;
            } else {
                this.infoProvider = new InfoProvider.InfoProviderAdapter(provider);
            }

        }

        [Route("orgchart/children/{id}")]
        [HttpGet]
        public Models.ChildrenResult children(string id) {
            // Demos seem to be a small JSON object with a single property Children containing
            // an array; hence returning a ChildrenResult

            var srcData = MakeChartNode<ChartNodeParent>(id);
            return new Models.ChildrenResult() { children = srcData.children };
        }

        [Route("orgchart/parent/{id}")]
        [HttpGet]
        public ChartNodeChild parent(string id) {
            // Parent: Get the truncated information (sans children) of the parent node of selected node.
            var parentId = infoProvider.GetParentId(id);
            return MakeChartNode<ChartNodeChild>(parentId);
        }

        [Route("orgchart/siblings/{id}")]
        [HttpGet]
        public SiblingsResult siblings(string id) {
            Debug.WriteLine($"orgchart/siblings/{id}");
            var srcData = MakeChartNode<ChartNodeParent>(infoProvider.GetParentId(id));
            // Demo data excludes 'self' from the sibling data.
            var sibs = srcData.children.Where(p => p.id != id).ToList();
            return new Models.SiblingsResult() { siblings = sibs };

        }

        [Route("orgchart/families/{id}")]
        [HttpGet]
        public ChartNodeParent families(string id) {
            Debug.WriteLine($"orgchart/families/{id}");
            // Families: Get the parent node and details for all children of the parent node
            var parentId = infoProvider.GetParentId(id);

            var cn = MakeChartNode<ChartNodeParent>(parentId);
            cn.children = cn.children.Where(p => p.id != id).ToList();
            return cn;
        }

        [Route("orgchart/initdata")]
        [HttpGet]
        public Models.ChartNodeParent initdata() {
            var rootId = infoProvider.GetRootId();

            return MakeChartNode<ChartNodeParent>(rootId);
        }

        private T MakeChartNode<T>(string id) where T : ChartNodeChild, new() {
            var srcNode = infoProvider.GetNode(id);
            // this could be an automapper call?
            var result = new T();
            result.id = srcNode.Id;
            result.name = srcNode.Name;
            result.title = srcNode.Title;

            result.relationship = string.Concat(
                srcNode.HasParent ? "1" : "0",
                srcNode.HasSiblings.Value ? "1" : "0",
                srcNode.HasChildren ? "1" : "0");

            result.className = srcNode.Division;

            // If being asked for a parent node, fill in the child nodes
            var asParent = result as ChartNodeParent;
            if (asParent != null) {
                foreach (var child in srcNode.Children) {
                    asParent.children.Add(MakeChartNode<ChartNodeChild>(child));
                }
            }

            return result;
        }
    }
}
