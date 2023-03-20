namespace FileSystemObserver_Api.Handlers
{
    public class FilteredFilesInPath: FilesAndDirectoies
    {
        private readonly string _filterBy;

        public FilteredFilesInPath(string path, string filterBy): base(path)
        {
            _filterBy = filterBy;
        }

        public override IEnumerable<FileView> GetAllFilesAndDirectories()
        {
            try
            {
                if (!_directory.Exists)
                {
                    return null;
                }

                foreach (var file in _directory.EnumerateFiles($"*.{_filterBy}"))
                {
                    _files.Add(new FileView() { FullName = file.FullName, Name = file.Name });
                }

                return _files;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
