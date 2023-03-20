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
            var handler = A.Fake<FilesAndDirectories>(x => x.WithArgumentsForConstructor(() => new FilesAndDirectories(_path)));

            var response = A.Fake<IEnumerable<FileView>>();

            var manager = Fake.GetFakeManager(handler);

            A.CallTo(() => handler.GetAllFilesAndDirectories(_path)).Returns(response);

            //Act
            var result = _service.GetAllFilesAndDirectoriesInPath(_path, "");

            //Assert
            result.Should().BeNullOrEmpty();
            Assert.Equal(handler, manager.Object);
        }  
    }
}
