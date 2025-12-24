using KlingelnbergMachineAssetManagement.Api.Application.Interfaces;
using static System.Net.Mime.MediaTypeNames;


namespace KlingelnbergMachineAssetManagement.Api.Infrastructure.ParserResolver
{
    public class MatrixParserResolver
    {
        private readonly IEnumerable<IUploadedMatrixParser> _parsers;

        public MatrixParserResolver(IEnumerable<IUploadedMatrixParser> parsers)
        {
            _parsers = parsers;
        }

        public IUploadedMatrixParser Get(string ext)
        {
            if (string.IsNullOrWhiteSpace(ext))
                throw new ArgumentException("File extension is missing");

            var parser = _parsers.FirstOrDefault(p => p.CanHandle(ext));

            if (parser == null)
                throw new NotSupportedException(
                    $"No parser registered for extension '{ext}'");

            return parser;
        }
    }
}
