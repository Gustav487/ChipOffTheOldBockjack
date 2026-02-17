using ChipMasters.IO;
using Godot;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ChipMasters.Resources.Materials
{
    /// <summary>
    /// <see cref="IAssetProvider{TAss}"/> that loads a texture and weakly caches it.
    /// </summary>
    public class RCachedMaterialProvider : IAssetProvider<IMaterialData>
    {
        private readonly IDictionary<string, IMaterialData> _cache = new Dictionary<string, IMaterialData>();
        private readonly string _assetPath;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="assetPath">Partialy path inside an asset directory to the texture. DO NOT INCLUDE FILE ENDINGS.</param>
        public RCachedMaterialProvider(string assetPath)
        {
            _assetPath = assetPath.AssertNotNull();
        } // end ctor



        /// <inheritdoc/>
        public IMaterialData Get(string assetSetID)
        {
            if (TryGet(assetSetID, out IMaterialData? asset))
                return asset;

            throw new RMissingAssetException($"Couldn't find texture: {_assetPath} in asset set: {assetSetID}");
        } // end Get()

        /// <inheritdoc/>
        public bool TryGet(string assetSetID, [NotNullWhen(true)] out IMaterialData? asset)
        {
            if (_cache.TryGetValue(assetSetID, out asset))
                return true;

            return TryImport(assetSetID, out asset);
        } // end TryGet()



        /// <summary>
        /// Import the texture from <paramref name="assetSetID"/>, both caching and returning it.
        /// </summary>
        /// <param name="assetSetID"></param>
        /// <param name="asset"></param>
        /// <returns></returns>
        private bool TryImport(string assetSetID, [NotNullWhen(true)] out IMaterialData? asset)
        {
            asset = null;
            string path = $"{SAssetUtil.ASSET_DIR}/{assetSetID}/{_assetPath}";
            if (ResourceLoader.Exists(path + ".png"))
                asset = new RStaticMaterialData(ResourceLoader.Load<Texture2D>(path + ".png"));
            else if (ResourceLoader.Exists(path + "/0.png"))
                asset = ImportAnimatedTexture(path);
            else
                return false;

            _cache[assetSetID] = asset;
            return true;
        } // end Import()

        private IMaterialData ImportAnimatedTexture(string path)
        {
            List<Texture2D> frames = new();
            int i = 0;
            while (ResourceLoader.Exists($"{path}/{i}.png"))
                frames.Add(ResourceLoader.Load<Texture2D>($"{path}/{i++}.png")); // i++ occurs after path creation, preparing it for next iteration. It's a very odd operation


            VAnimData animData;
            string animDataPath = path + "/anim.json";
            if (ResourceLoader.Exists(animDataPath))
                animData = VAnimData.ENDEC.Decode(
                    JsonStreamCoder.INSTANCE,
                    JsonElement.ParseJson(FileAccess.GetFileAsString(animDataPath)),
                    SIOUtil.CODING_SETTINGS);
            else
                animData = new VAnimData(.1f, frames.Select((x, i) => i));

            return new RAnimatedMaterialData(frames.Select(x => x.GetImage()), animData);
        } // end ImportAnimatedTexture()




    } // end class
} // end namespace