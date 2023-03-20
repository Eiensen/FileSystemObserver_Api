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
            if (!String.IsNullOrEmpty(path) && String.IsNullOrEmpty(filter))
            {              
                return new FilesAndDirectoies(path).GetAllFilesAndDirectories();
            }
            else if (!String.IsNullOrEmpty(filter) && !String.IsNullOrEmpty(path))
            {
                return new FilteredFilesInPath(path, filter).GetAllFilesAndDirectories();
            }
            else if (!String.IsNullOrEmpty(filter) && String.IsNullOrEmpty(path))
            {
                return new FilteredFilesInPath(_defaultPath, filter).GetAllFilesAndDirectories();
            }
            else
            {
                return new FilesAndDirectoies(_defaultPath).GetAllFilesAndDirectories();
            }
        }        
    }
}
