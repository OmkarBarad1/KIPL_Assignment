namespace KlingelnbergMachineAssetManagement.Api.Domain.Entities
{
    public class Machine
    {
        public string MachineName { get; }

        public Machine(string name)
        {
            this.MachineName = name;
        }
    }
}
