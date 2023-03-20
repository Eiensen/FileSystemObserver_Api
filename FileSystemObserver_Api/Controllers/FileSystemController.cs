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
        /// <param name="path">Путь к папке</param>
        /// <param name="filter">Филтр по расширению (exe, ini, sys и т.д.)</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public IActionResult GetAllFilesAndDirectories(string? path, string? filter)
        {
            _logger.LogInformation($"Запущен метод контроллера - GetAllFilesAndDirectories()! Параметры: [path={path}, filter={filter}]");

            var response = _service.GetAllFilesAndDirectoriesInPath(path, filter);

            if (response == null)
            {
                _logger.LogWarning($"Неправильный путь! Получен параметр: path={path}");

                return BadRequest("Incorrect path!");
            }

            _logger.LogInformation($"Успешно отправлен объект: {response}");

            return Ok(response);
        }       
    }
}
