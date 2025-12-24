using KlingelnbergMachineAssetManagement.Api.Application.Interfaces;
using KlingelnbergMachineAssetManagement.Api.Infrastructure.Fileupload;


namespace KlingelnbergMachineAssetManagement.Api.Infrastructure.FileUpload
{
    public class MatrixUploadService
    {
        //private readonly MatrixParserResolver _Resolver;
        private readonly MatrixWriter _writer;
        private readonly IEnumerable<IUploadedMatrixParser> _parser;
        public MatrixUploadService(IEnumerable<IUploadedMatrixParser> parser, MatrixWriter writer)
        {
            _parser = parser;  
            _writer = writer;
        }

        public async Task UploadAsync(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            var parser = _parser.FirstOrDefault(p => p.CanHandle(ext)) ?? throw new NotSupportedException($"No parser registered"); ;

            var records = await parser.ParseAsync(file);
            _writer.Save(records);
        }
    }
}
