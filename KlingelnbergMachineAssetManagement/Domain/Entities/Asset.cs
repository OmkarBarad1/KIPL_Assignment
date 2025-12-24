namespace KlingelnbergMachineAssetManagement.Domain.Entities
{
    public class Asset
    {
        public string AssetName { get; }
        public string Series { get; }

        public Asset(string name, string series)
        {
            this.AssetName = name;
            this.Series = series;
        }
    }
}
