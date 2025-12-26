using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
