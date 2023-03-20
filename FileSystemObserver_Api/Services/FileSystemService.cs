namespace FileSystemObserver_Api.Services
{
    public class FileSystemService : IFileSystemService
    {
        private readonly IConfiguration _configuration;

        private readonly string _defaultPath;

        public FileSystemService(IConfiguration configuration)
        {
            _configuration = configuration;

            _defaultPath = _configuration.GetSection("defaultPath").Value;
        }

        public IEnumerable<FileView> GetAllFilesAndDirectoriesInPath(string? path, string? filter) =>
            new FilesAndDirectories(path ?? _defaultPath).GetAllFilesAndDirectories(filter);        
    }
}
