﻿namespace FileSystemObserver_Api.Handlers
{
    public class BaseFilesAndDirectoiesHandler
    {
        protected List<FileView> _files;

        protected DirectoryInfo _directory;

        public BaseFilesAndDirectoiesHandler(string path)
        {
            _directory = new DirectoryInfo(path);

            _files = new List<FileView>();
        }

        public virtual IEnumerable<FileView> GetAllFilesAndDirectories()
        {     

            try
            {
                if (!_directory.Exists)
                {
                    return null;
                }

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
