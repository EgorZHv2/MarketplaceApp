using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers;

public class FilesController : Controller
{
    /// <summary>
    /// Получить информацию о файле
    /// </summary>
    /// <param name="shopId"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetFileInfo(Guid id)
    {
        // code
        // more code
        return NotFound();
    }
}