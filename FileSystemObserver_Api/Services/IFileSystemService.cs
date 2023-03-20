namespace FileSystemObserver_Api.Services
{
    public interface IFileSystemService
    {
        IEnumerable<FileView> GetAllFilesAndDirectoriesInPath(string? path, string? filter);
    }
}
