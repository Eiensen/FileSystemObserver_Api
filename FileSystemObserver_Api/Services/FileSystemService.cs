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

        public IEnumerable<FileView> GetAllFilesAndDirectoriesInPath(string? path, string? filter)
        {
            if (String.IsNullOrEmpty(path))
            {              
                return new BaseFilesAndDirectoiesHandler(_defaultPath).GetAllFilesAndDirectories();
            }
            else if (String.IsNullOrEmpty(filter))
            {
                return new FilteredFilesInCurrentPath(path, filter).GetAllFilesAndDirectories();
            }
            else
            {
                return new BaseFilesAndDirectoiesHandler(path).GetAllFilesAndDirectories();
            }
        }        
    }
}
