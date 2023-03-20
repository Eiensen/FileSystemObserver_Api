namespace FileSystemObserver_Api_Tests.ControllerTests
{
    public class FileSystemControllerTests
    {
        private readonly IFileSystemService _service;

        private readonly FileSystemController _controller;

        private readonly ILogger<FileSystemController> _logger;

        public FileSystemControllerTests()
        {
            _service = A.Fake<IFileSystemService>();

            _logger = A.Fake<ILogger<FileSystemController>>();

            _controller = new FileSystemController(_service, _logger);
        }

        [Fact]
        public void FileSystemController_GetAllFilesAndDirectories_ReturnOk()
        {
            //Arrenge
            var path = string.Empty;
            var filter = string.Empty;

            //Act
            var result = _controller.GetAllFilesAndDirectories(path, filter);

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
