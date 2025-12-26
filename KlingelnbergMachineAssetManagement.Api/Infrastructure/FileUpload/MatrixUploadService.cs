using KlingelnbergMachineAssetManagement.Api.Application.Interfaces;
using KlingelnbergMachineAssetManagement.Api.Infrastructure.Fileupload;


namespace KlingelnbergMachineAssetManagement.Api.Infrastructure.FileUpload
{
    public class MatrixUploadService
    {
        private readonly MatrixWriter _writer;
        private readonly IRepository _repository;
        public MatrixUploadService(IRepository repository, MatrixWriter writer)
        {
            _repository = repository; 
            _writer = writer;
        }

        public async Task UploadAsync(Stream stream, string extension)
        {
            var records = await _repository.GetAllData(stream, extension);
            _writer.Save(records);
        }
    }
}
