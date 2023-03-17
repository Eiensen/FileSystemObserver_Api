namespace FileSystemObserver_Api.Handlers
{
    public class FilesAndDirectoriesInParentDirectory: BaseFilesAndDirectoiesHandler
    {
        public FilesAndDirectoriesInParentDirectory(string path): base(path)
        {
            _directory = new DirectoryInfo(path).Parent;
        }
    }
}
