using FileSystemObserver_Api.Handlers;

namespace FileSystemObserver_Api.Services
{
    public class FileSystemService : IFileSystemService
    {
        private readonly IConfiguration _configuration;

        public FileSystemService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<FileView> GetAllFilesAndDirectoriesInDefaultPath()
        {
            var defaultPath = _configuration.GetSection("defaultPath").Value;

            if (!String.IsNullOrEmpty(defaultPath))
            {
                var handler = new FilesAndDirectoriesInCurrentPath(defaultPath);

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
                return handler.GetAllFilesAndDirectories();
            }

            return null;
        }

        public IEnumerable<FileView> GetAllFilesAndDirectoriesInParent(string dirPath)
        {
            var handler = new FilesAndDirectoriesInParentDirectory(dirPath);

            if (handler != null)
            {
                return handler.GetAllFilesAndDirectories();
            }

            return null;
        }

        public IEnumerable<FileView> GetFilteredListOfFiles(string dirPath, string filter)
        {
            var handler = new FilteredFilesInCurrentPath(dirPath, filter);

            if (handler != null)
            {
                return handler.GetAllFilesAndDirectories();
            }

            return null;
        }
    }
}
