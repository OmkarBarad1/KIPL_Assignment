using KlingelnbergMachineAssetManagement.Api.Infrastructure.FileWriter;
using KlingelnbergMachineAssetManagement.Api.Infrastructure.ParserResolver;
using Microsoft.AspNetCore.Http;


namespace KlingelnbergMachineAssetManagement.Api.Infrastructure.FileUpload
{
    public class MatrixUploadService
    {
        private readonly MatrixParserResolver _Resolver;
        private readonly MatrixWriter _writer;

        public MatrixUploadService(MatrixParserResolver Resolver, MatrixWriter writer)
        {
            _Resolver = Resolver;
            _writer = writer;
        }

        public async Task UploadAsync(IFormFile file, bool append)
        {
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            var parser = _Resolver.Get(ext);

            var records = await parser.ParseAsync(file);
            _writer.Save(records, append);
        }
    }
}
