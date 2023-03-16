﻿namespace FileSystemObserver_Api_Tests.ControllerTests
{
    public class FileSystemControllerTests
    {
        private readonly IFileSystemService _service;

        private readonly FileSystemController _controller;

        public FileSystemControllerTests() 
        {
            _service = A.Fake<IFileSystemService>();

            _controller = new FileSystemController(_service);
        }

        [Fact]
        public void FileSystemController_GetFilesInDefaultPath_ReturnOk()
        {
            //Arrenge
            var response = A.Fake<IEnumerable<FileView>>();

            A.CallTo(() => _service.GetFilesInDefaultPath()).Returns(response);

            //Act
            var result = _controller.GetFilesInDefaultPath();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void FileSystemController_GetFilesInDirectory_ReturnOk()
        {
            //Arrenge
            var response = A.Fake<IEnumerable<FileView>>();

            var directory = string.Empty;

            A.CallTo(() => _service.GetFilesInDirectory(directory, false)).Returns(response);

            //Act
            var result = _controller.GetFilesInDirectory(directory);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void FileSystemController_GetFilteredListOfFiles_ReturnOk()
        {
            //Arrenge
            var response = A.Fake<IEnumerable<FileView>>();

            var directory = string.Empty;

            var filter = string.Empty;

            A.CallTo(() => _service.GetFilteredListOfFiles(directory, filter)).Returns(response);

            //Act
            var result = _controller.GetFilteredListOfFiles(directory, filter);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
