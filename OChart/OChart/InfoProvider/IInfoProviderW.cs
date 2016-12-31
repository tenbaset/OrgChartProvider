/// <summary>
/// Wide interface that should be implemented by an info provider plug in.   This should be
/// implemented if straightforward, otherwise implement the easier IInfoProvider interface.
/// (Essentially, if the underlying data source has sibling information readily available or not.)
/// Do not write your own adapter, as there already is one.
/// </summary>
public interface IInfoProviderW : IInfoProvider {

    /// <summary>
    /// Get the ID of the parent of passed ID
    /// </summary>
    /// <param name="childId">ID of the child node</param>
    /// <returns>Parent ID</returns>
     string GetParentId(string childId);



}
