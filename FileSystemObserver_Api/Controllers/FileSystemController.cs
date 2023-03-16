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
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public IActionResult GetFilesInDefaultPath()
        {
            var response = _service.GetFilesInDefaultPath();

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
        /// <param name="isToParent">true - возврат к предыдущей папке</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        [Route("/api/[controller]/directory")]
        public IActionResult GetFilesInDirectory(string fullPath, bool isToParent = false)
        {
            var response = _service.GetFilesInDirectory(fullPath, isToParent);

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
