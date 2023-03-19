namespace FileSystemObserver_Api.Services
{
    public class FileSystemService : IFileSystemService
    {
        private readonly IConfiguration _configuration;

        private readonly ILogger<FileSystemService> _logger;

        private readonly string _defaultPath;

        public FileSystemService(IConfiguration configuration, ILogger<FileSystemService> logger)
        {
            _configuration = configuration;

            _logger = logger;

            _defaultPath = _configuration.GetSection("defaultPath").Value;
        }

        public IEnumerable<FileView> GetAllFilesAndDirectoriesInDefaultPath()
        {
            _logger.LogInformation($"Start GetAllFilesAndDirectoriesInDefaultPath() method with default path = {_defaultPath}");

            if (!String.IsNullOrEmpty(_defaultPath))
            {
                var handler = new FilesAndDirectoriesInCurrentPath(_defaultPath);

                var files = handler.GetAllFilesAndDirectories();

                return files;
            }

            _logger.LogWarning($"The {_defaultPath} is empty!");

            return null;
        }

        public IEnumerable<FileView> GetAllFilesAndDirectoriesInCurrentPath(string dirPath)
        {
            _logger.LogInformation($"Start GetAllFilesAndDirectoriesInCurrentPath() method with path = {dirPath}");

            var handler = new FilesAndDirectoriesInCurrentPath(dirPath);

            if (handler != null)
            {
                var files =  handler.GetAllFilesAndDirectories();

                return files;
            }

            _logger.LogError($"The object {handler.GetType()} is null!");

            return null;
        }

        public IEnumerable<FileView> GetAllFilesAndDirectoriesInParent(string dirPath)
        {
            _logger.LogInformation($"Start GetAllFilesAndDirectoriesInParent() method with path = {dirPath}");

            var handler = new FilesAndDirectoriesInParentDirectory(dirPath);

            if (handler != null)
            {
                var files = handler.GetAllFilesAndDirectories();

                return files;
            }

            _logger.LogError($"The object {handler.GetType()} is null!");

            return null;
        }

        public IEnumerable<FileView> GetFilteredListOfFiles(string dirPath, string filter)
        {
            _logger.LogInformation($"Start GetFilteredListOfFiles() method with path = {dirPath} and filter = {filter}");

            var handler = new FilteredFilesInCurrentPath(dirPath, filter);

            if (handler != null)
            {
                var files = handler.GetAllFilesAndDirectories();

                return files;
            }

            _logger.LogError($"The object {handler.GetType()} is null!");

            return null;
        }
    }
}
