using FileSystemObserver_Api.ViewModels;

namespace FileSystemObserver_Api.Services
{
    public interface IFileSystemService
    {
        IEnumerable<FileView> GetFilesInDefaultPath();

        IEnumerable<FileView> GetFilesInDirectory(string dirPath, bool isToParent);

        IEnumerable<FileView> GetFilteredListOfFiles(string dirPath, string filter);
    }
}
