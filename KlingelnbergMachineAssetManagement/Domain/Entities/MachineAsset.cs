namespace KlingelnbergMachineAssetManagement.Domain.Entities
{
    public class MachineAsset
    {
        public string MachineName { get; }
        public string AssetName { get; }
        public string Series { get; }

        public MachineAsset(string machineName, string assetName, string series)
        {
            this.MachineName = machineName;
            this.AssetName = assetName;
            this.Series = series;
        }
    }
}
