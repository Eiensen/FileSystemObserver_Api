namespace FileSystemObserver_Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FileSystemController : ControllerBase
    {
        private readonly IFileSystemService _service;

        private readonly ILogger<FileSystemController> _logger;

        public FileSystemController(IFileSystemService service, ILogger<FileSystemController> logger)
        {
            _service = service;

            _logger = logger;
        }

        /// <summary>
        /// Запрос на получение всех папок и файлов. Путь по умолчанию: С:\
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public IActionResult GetFilesInDefaultPath()
        {
            _logger.LogInformation("Run GetFilesInDefaultPath method!");

            var response = _service.GetAllFilesAndDirectoriesInPath();

            if (response == null)
            {
                _logger.LogInformation("Has null response from _service!");

                return NotFound("Incorrect default path. Please, check the path in config.json file.");
            }

            return Ok(response);
        }

        /// <summary>
        /// Запрос на получение всех папок и файлов по заданному пути. Можно вернуться к предыдущей папке
        /// </summary>
        /// <param name="fullPath">Полный путь до файла или папки</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        [Route("/api/[controller]/directory")]
        public IActionResult GetFilesInDirectory(string fullPath)
        {
            _logger.LogInformation($"Run GetFilesInDirectory method with param={fullPath}");

            var response = _service.GetAllFilesAndDirectoriesInCurrentPath(fullPath);

            if (response == null)
            {
                _logger.LogWarning("Has null response from _service!");

                return BadRequest("Path incorrect!");
            }

            _logger.LogInformation($"Succesfull has colection: {response.GetType()}");

            return Ok(response);
        }

        /// <summary>
        /// Запрос на получение отфильтрованных файлов по заданному пути
        /// </summary>
        /// <param name="fullPath">Полный путь до файла или папки</param>
        /// <param name="filter">Расширение, по которому филтруем (exe, ini, txt и т.п)</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        [Route("/api/[controller]/directory/filter")]
        public IActionResult GetFilteredListOfFiles(string fullPath, string filter)
        {
            _logger.LogInformation($"Run GetFilteredListOfFiles() method with params[ fullPath={fullPath}, filter={filter} ]");

            var response = _service.GetFilteredListOfFiles(fullPath, filter);

            if (response == null)
            {
                _logger.LogWarning("Has null response from _service!");

                return BadRequest("No existing files by this filter or incorrect path!");
            }

            _logger.LogInformation($"Succesfull has colection: {response.GetType()}");

            return Ok(response);
        }
    }
}
