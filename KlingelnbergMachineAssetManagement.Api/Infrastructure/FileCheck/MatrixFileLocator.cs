
using KlingelnbergMachineAssetManagement.Api.Application.Interfaces;

namespace KlingelnbergMachineAssetManagement.Api.Infrastructure.FileCheck
{
   
    public class MatrixFileLocator : IMatrixFileLocator
    {
        private readonly IWebHostEnvironment _env;

        public MatrixFileLocator(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string? GetMatrixFilePath()
        {
            var dataFolder = Path.GetFullPath(Path.Combine(
                _env.ContentRootPath, 
                "KlingelnbergMachineAssetManagement.Api",
                "..",
                "Data"
            ));

            if (!Directory.Exists(dataFolder))
                return null;

            return Directory.GetFiles(dataFolder, "matrix.*").FirstOrDefault();
        }
    }
}
