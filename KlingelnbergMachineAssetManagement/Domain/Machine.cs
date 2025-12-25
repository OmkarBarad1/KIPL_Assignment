namespace KlingelnbergMachineAssetManagement.Domain
{
    public class Machine
    {
        public string MachineName { get; }
        public Machine(string machineName)
        {
            MachineName = machineName;
        }
    }
}
