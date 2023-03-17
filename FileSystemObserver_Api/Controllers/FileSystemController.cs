using Microsoft.AspNetCore.Diagnostics;

namespace FileSystemObserver_Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FileSystemController : ControllerBase
    {
        private readonly IFileSystemService _service;

        public FileSystemController(IFileSystemService service)
        {
            _service = service;
        }

        /// <summary>
        /// Запрос на получение всех папок и файлов. Путь по умолчанию: С:\
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public IActionResult GetFilesInDefaultPath()
        {
            var response = _service.GetAllFilesAndDirectoriesInDefaultPath();

            if (response == null)
            {
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
            var response = _service.GetAllFilesAndDirectoriesInCurrentPath(fullPath);

            if (response == null)
            {
                return BadRequest("Path incorrect!");
            }

            return Ok(response);
        }

        /// <summary>
        /// Запрос на получение всех папок и файлов в предыдущей папке от указанного пути.
        /// </summary>
        /// <param name="fullPath">Текущий полный путь</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        [Route("/api/[controller]/parent")]
        public IActionResult GetFilesInParentDirectory(string fullPath)
        {
            var response = _service.GetAllFilesAndDirectoriesInParent(fullPath);

            if (response == null)
            {
                return BadRequest("Path incorrect!");
            }

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
            var response = _service.GetFilteredListOfFiles(fullPath, filter);

            if (response == null)
            {
                return BadRequest("No existing files by this filter or incorrect path!");
            }

            return Ok(response);
        }
    }
}
