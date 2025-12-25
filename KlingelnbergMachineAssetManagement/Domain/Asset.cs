namespace KlingelnbergMachineAssetManagement.Domain
{
    public class Asset
    {
        public string AssetName { get; }
        public string Series { get; }

        public Asset(string assetName, string series)
        {
            AssetName = assetName;
            Series = series;
        }
    }
}
