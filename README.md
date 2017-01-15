# OrgChartProvider
.Net WebAPI based provider of information for OrgChart

Designed for enterprise environments, you need only implement a reasonably
simple interface for you data set (IInfoProvider).

Uses <a href="https://github.com/dabeng/OrgChart">OrgChart</a> as the client-side web renderer.

To use in your own situations, you would need to implement IInfoProvider and then adjust WebApiConfig to use your class.

Note this is still early days and some significant refactoring and changing to the API is probably required.