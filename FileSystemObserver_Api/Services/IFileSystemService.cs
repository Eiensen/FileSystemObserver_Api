using FileSystemObserver_Api.ViewModels;

namespace FileSystemObserver_Api.Services
{
    public interface IFileSystemService
    {
        IEnumerable<FileView> GetAllFilesAndDirectoriesInDefaultPath();

        IEnumerable<FileView> GetAllFilesAndDirectoriesInCurrentPath(string dirPath);

        IEnumerable<FileView> GetFilteredListOfFiles(string dirPath, string filter);
    }
}
