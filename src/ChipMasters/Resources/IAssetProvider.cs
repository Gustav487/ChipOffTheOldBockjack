using System.Diagnostics.CodeAnalysis;

namespace ChipMasters.Resources
{
    /// <summary>
    /// Contract for an object which provides assets.
    /// </summary>
    public interface IAssetProvider<TAss>
    {
        /// <summary>
        /// Get the asset from the asset set named <paramref name="assetSetID"/>. 
        /// </summary>
        /// <param name="assetSetID"></param>
        /// <returns></returns>
        /// <exception cref="RMissingAssetException"></exception>
        public TAss Get(string assetSetID);

        /// <summary>
        /// Try to get the asset from the asset set named <paramref name="assetSetID"/>. 
        /// </summary>
        /// <param name="assetSetID"></param>
        /// <param name="asset"></param>
        /// <returns>True if successful, false is unsuccessful.</returns>
        public bool TryGet(string assetSetID, [NotNullWhen(true)] out TAss? asset);
    } // end interface
} // end namespace