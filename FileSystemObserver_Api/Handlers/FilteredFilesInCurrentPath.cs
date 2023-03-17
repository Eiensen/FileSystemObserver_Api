namespace FileSystemObserver_Api.Handlers
{
    public class FilteredFilesInCurrentPath: BaseFilesAndDirectoiesHandler
    {
        private readonly string _filterBy;

        public FilteredFilesInCurrentPath(string path, string filterBy): base(path)
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

                if (_files.Count > 0)
                {
                    return _files;
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
