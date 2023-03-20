using FileSystemObserver_Api.Handlers;

namespace FileSystemObserver_Api_Tests.ServiciesTests
{
    public class FileSystemServiceTest
    {
        private readonly FileSystemService _service;

        private readonly IConfiguration _configuration;

        private readonly ILogger<FileSystemService> _logger;

        private readonly string _path;

        public FileSystemServiceTest()
        {
            _configuration = A.Fake<IConfiguration>();

            _logger = A.Fake<ILogger<FileSystemService>>();

            _service = new FileSystemService(_configuration, _logger);

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
        public void FileSystemService_GetFilteredListOfFiles_ReturnList()
        {
            //Arrenge
            var filterBy = "sys";

            var handler = A.Fake<FilteredFilesInPath>(x => x.WithArgumentsForConstructor(() => new FilteredFilesInPath(_path, filterBy)));

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
