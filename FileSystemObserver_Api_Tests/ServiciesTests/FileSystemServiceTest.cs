using FileSystemObserver_Api.Handlers;

namespace FileSystemObserver_Api_Tests.ServiciesTests
{
    public class FileSystemServiceTest
    {
        private readonly FileSystemService _service;

        private readonly IConfiguration _configuration;

        private readonly string _path;

        public FileSystemServiceTest()
        {
            _configuration = A.Fake<IConfiguration>();

            _service = new FileSystemService(_configuration);

            _path = "C:";
        }

        [Fact]
        public void FileSystemService_GetAllFilesAndDirectoriesInDefaultPath_ReturnNull()
        {
            //Arrenge
            var handler = A.Fake<FilesAndDirectoriesInCurrentPath>(x => x.WithArgumentsForConstructor(() => new FilesAndDirectoriesInCurrentPath(_path)));

            var response = A.Fake<IEnumerable<FileView>>();

            var manager = Fake.GetFakeManager(handler);

            A.CallTo(() => handler.GetAllFilesAndDirectories()).Returns(response);

            //Act
            var result = _service.GetAllFilesAndDirectoriesInDefaultPath();

            //Assert
            result.Should().BeNull();
            Assert.Equal(handler, manager.Object);
        }

        [Fact]
        public void FileSystemService_GetAllFilesAndDirectoriesInCurrentPath_ReturnList()
        {
            //Arrenge
            var handler = A.Fake<FilesAndDirectoriesInCurrentPath>(x => x.WithArgumentsForConstructor(() => new FilesAndDirectoriesInCurrentPath(_path)));

            var response = A.Fake<IEnumerable<FileView>>();

            var manager = Fake.GetFakeManager(handler);

            A.CallTo(() => handler.GetAllFilesAndDirectories()).Returns(response);

            //Act
            var result = _service.GetAllFilesAndDirectoriesInCurrentPath(_path);

            //Assert
            result.Should().Contain(x => x.FullName.Contains("C:"));
            Assert.Equal(handler, manager.Object);
        }

        [Fact]
        public void FileSystemService_GetAllFilesAndDirectoriesInParent_ReturnList()
        {
            //Arrenge
            var pathForParent = @"C:\windows";

            var handler = A.Fake<FilesAndDirectoriesInParentDirectory>(x => x.WithArgumentsForConstructor(() => new FilesAndDirectoriesInParentDirectory(pathForParent)));

            var response = A.Fake<IEnumerable<FileView>>();

            var manager = Fake.GetFakeManager(handler);

            A.CallTo(() => handler.GetAllFilesAndDirectories()).Returns(response);

            //Act
            var result = _service.GetAllFilesAndDirectoriesInParent(pathForParent);

            //Assert
            result.Should().Contain(x => x.FullName.Contains("C:"));
            Assert.Equal(handler, manager.Object);
        }

        [Fact]
        public void FileSystemService_GetFilteredListOfFiles_ReturnList()
        {
            //Arrenge
            var filterBy = "sys";

            var handler = A.Fake<FilteredFilesInCurrentPath>(x => x.WithArgumentsForConstructor(() => new FilteredFilesInCurrentPath(_path, filterBy)));

            var response = A.Fake<IEnumerable<FileView>>();

            var manager = Fake.GetFakeManager(handler);

            A.CallTo(() => handler.GetAllFilesAndDirectories()).Returns(response);

            //Act
            var result = _service.GetFilteredListOfFiles(_path, filterBy);

            //Assert
            result.Should().Contain(x => x.FullName.Contains("C:"));
            result.Should().Contain(x => x.Name.Contains(filterBy));
            Assert.Equal(handler, manager.Object);
        }
    }
}
