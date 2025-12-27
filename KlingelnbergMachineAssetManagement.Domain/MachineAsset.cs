namespace KlingelnbergMachineAssetManagement.Domian
{
    public class MachineAsset
    {
        public string MachineName { get; }
        public string AssetName { get; }
        public string Series { get; }

        public MachineAsset(string machineName, string assetName, string series)
        {
            MachineName = machineName;
            AssetName = assetName;
            Series = series;
        }
    }
}
