using OChart.InfoProvider;
/// <summary>
/// Restricted interface that should be implemented by an info provider plug in.  If straightforward,
/// should implement the IInfoProviderW interface instead of this one.  However, there is an adapter
/// so don't implement your own 'adapter' type system.
/// </summary>
public interface IInfoProvider {

    /// <summary>
    /// Should return the ID of the root node
    /// </summary>
    /// <returns></returns>
    string GetRootId();

    /// <summary>
    /// Should return information about a particular node based on the id, including children IDs
    /// </summary>
    /// <param name="id">id of the interested node</param>
    /// <returns>Information for node "id"</returns>
    InfoProviderNode GetNode(string id);
}
