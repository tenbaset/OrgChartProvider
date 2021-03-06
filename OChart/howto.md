# General
To change to your own provider of information, you must update "WebApiConfig.cs" to whatever class name
that you want.  Out of the box, it uses DummyInfoProvider which is almost certainly not what you want!

To keep the API simple, it's possible there will be many calls to the GetNode(..) method 
(eg., to get sibling information for node "a", your provider will be asked about node "a", then the parent (node "b" based on the parent property of node "a".  Then asked about children of node "b" as required to build the sibling information.) 
 This will mean a cache is useful - hence the "InfoProviderCacheLayer" which is a caching implementation of the IInfoProvider. 

 You can either use one of my InfoProviders, write your own, or base yours on mine.  But if you have a really cool one, maybe submit it back to the project as a whole?

# Active Directory
**ADInfoProvider** reads data from Active Directory.  Aiming to be self-configuring as possible. (i.e., connect to AD based on the identity that the process itself is running as.) 

Still early days for this one.  Help appreciated.

## ADInfoProviderAuto
 **ADInfoProviderAuto** should become a subclass of **ADInfoProvier**.  The only difference is how it works out the 'root' node -- it should start the tree with the currently authenticated user!

Probably buggy.  Help appreciated.

To use, simply update WebApiConfig.cs to use this class.



# Database

**DBInfoProvider** to read from a database.  Not yet written.

# Dummy 
**DummyInfoProvider** is a simple, zero-configuration, not useful provider of dummy information.  But, hey, fast and reliable!

# Info Provider Cache Layer

**InfoProvierCacheLayer** forwards all requests on to another provider, but caches all results in the standard ASP.Net caching framework for 30 minutes.

**InfoProviderCacheLayerNoRoot** is exactly the same for getting node information - but does not attempt to cache root information (in case the root might change - e.g., with AdInfoProvierAuto where it is the currently logged in user.)