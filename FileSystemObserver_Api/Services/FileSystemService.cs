namespace FileSystemObserver_Api.Services
{
    public class FileSystemService : IFileSystemService
    {
        private readonly IConfiguration _configuration;

        public FileSystemService(IConfiguration configuration)
        {
            _configuration = configuration;         
        }

        public IEnumerable<FileView> GetFilesInDefaultPath()
        {
            var defaultPath = _configuration.GetSection("defaultPath").Value;

            if(!String.IsNullOrEmpty(defaultPath))
            {
                var directory = new DirectoryInfo(defaultPath);

                var files = GetAllFilesInPath(directory);

                return files;
            }
            
            return null;
        }

        public IEnumerable<FileView> GetFilesInDirectory(string dirPath, bool isToParent)
        {
            if (isToParent)
            {
                var directory = new DirectoryInfo(dirPath).Parent;

                if(directory != null)
                {
                    return GetAllFilesInPath(directory);
                }

                return null;
            }
            else
            {
                var directory = new DirectoryInfo(dirPath);

                if(directory != null)
                {
                    return GetAllFilesInPath(directory);
                }

                return null;
            }
        }

        public IEnumerable<FileView> GetFilteredListOfFiles(string dirPath, string filter)
        {
            var directory = new DirectoryInfo(dirPath);

            if (directory != null)
            {
                return FilteredFiles(directory, filter);
            }

            return null;
        }

        private IEnumerable<FileView> GetAllFilesInPath(DirectoryInfo dirInfo)
        {
            var files = new List<FileView>();

            try
            {
                if (!dirInfo.Exists)
                {
                    return null;
                }

                foreach (var dir in dirInfo.GetDirectories())
                {
                    files.Add(new FileView() { FullName = dir.FullName, Name = dir.Name });
                }

                foreach (var file in dirInfo.GetFiles())
                {
                    files.Add(new FileView() { FullName = file.FullName, Name = file.Name });
                }

                return files;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private IEnumerable<FileView> FilteredFiles(DirectoryInfo dirInfo, string filterBy)
        {
            var files = new List<FileView>();

            try
            {
                if (!dirInfo.Exists)
                {
                    return null;
                }
                
                foreach (var file in dirInfo.EnumerateFiles($"*.{filterBy}"))
                {
                    files.Add(new FileView() { FullName = file.FullName, Name = file.Name });
                }

                if(files.Count > 0)
                {
                    return files;
                }
                else
                {
                    return null;
                }                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
