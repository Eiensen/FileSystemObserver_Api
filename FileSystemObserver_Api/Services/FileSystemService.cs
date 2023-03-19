using FileSystemObserver_Api.Handlers;

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

        public IEnumerable<FileView> GetAllFilesAndDirectoriesInDefaultPath()
        {           
            if (!String.IsNullOrEmpty(_defaultPath))
            {
                var handler = new FilesAndDirectoriesInCurrentPath(_defaultPath);

                var files = handler.GetAllFilesAndDirectories();

                return files;
            }

            return null;
        }

        public IEnumerable<FileView> GetAllFilesAndDirectoriesInCurrentPath(string dirPath)
        {
            var handler = new FilesAndDirectoriesInCurrentPath(dirPath);

            if (handler != null)
            {
                var files =  handler.GetAllFilesAndDirectories();

                return files;
            }

            return null;
        }

        public IEnumerable<FileView> GetAllFilesAndDirectoriesInParent(string dirPath)
        {
            var handler = new FilesAndDirectoriesInParentDirectory(dirPath);

            if (handler != null)
            {
                var files = handler.GetAllFilesAndDirectories();

                return files;
            }

            return null;
        }

        public IEnumerable<FileView> GetFilteredListOfFiles(string dirPath, string filter)
        {
            var handler = new FilteredFilesInCurrentPath(dirPath, filter);

            if (handler != null)
            {
                var files = handler.GetAllFilesAndDirectories();

                return files;
            }

            return null;
        }
    }
}
