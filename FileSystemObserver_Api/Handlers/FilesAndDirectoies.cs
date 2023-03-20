namespace FileSystemObserver_Api.Handlers
{
    public class FilesAndDirectoies
    {
        protected List<FileView> _files;

        protected DirectoryInfo _directory;

        public FilesAndDirectoies(string path)
        {
            _directory = new DirectoryInfo(path);

            _files = new List<FileView>();
        }

        public virtual IEnumerable<FileView> GetAllFilesAndDirectories(string? filter)
        {     

            try
            {
                if (!_directory.Exists)
                {
                    return null;
                }

                if(filter == null)
                {
                    foreach (var dir in _directory.GetDirectories())
                    {
                        _files.Add(new FileView() { FullName = dir.FullName, Name = dir.Name });
                    }

                    foreach (var file in _directory.GetFiles())
                    {
                        _files.Add(new FileView() { FullName = file.FullName, Name = file.Name });
                    }

                    return _files;
                }
                else
                {
                    foreach (var file in _directory.EnumerateFiles($"*.{filter}"))
                    {
                        _files.Add(new FileView() { FullName = file.FullName, Name = file.Name });
                    }

                    return _files;
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
