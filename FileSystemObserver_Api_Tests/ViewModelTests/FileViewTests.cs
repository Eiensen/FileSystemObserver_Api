namespace FileSystemObserver_Api_Tests.ViewModelTests
{
    public class FileViewTests
    {
        [Fact]
        public void FileView_FullName_ReturnEmptyString()
        {
            //Arrenge
            var fileView = new FileView();

            var expected = string.Empty;

            //Act
            var result = fileView.FullName;

            //Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void FileView_Name_ReturnEmptyString()
        {
            //Arrenge
            var fileView = new FileView();

            var expected = string.Empty;

            //Act
            var result = fileView.Name;

            //Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void FileView_FullName_ReturnString()
        {
            //Arrenge
            var fileView = new FileView() { FullName = "C:"};

            //Act
            var result = fileView.FullName;

            //Assert
            result.Should().NotBeEmpty();
            result.Should().BeOfType<string>();            
        }

        [Fact]
        public void FileView_Name_ReturnString()
        {
            //Arrenge
            var fileView = new FileView() { Name = "file" };

            //Act
            var result = fileView.Name;

            //Assert
            result.Should().NotBeEmpty();
            result.Should().BeOfType<string>();
        }
    }
}
