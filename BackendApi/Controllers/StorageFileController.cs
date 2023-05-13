using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Saritasa.BAL.StorageFile;
using System.Security.Claims;
using ViewModels.StorageFile;
using static System.Net.Mime.MediaTypeNames;

namespace Saritasa.BackendApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageFileController : Controller
    {
        private readonly IStorageFileService _storageFileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StorageFileController(IStorageFileService storageFileService, IHttpContextAccessor httpContextAccessor)
        {
            _storageFileService = storageFileService;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpPost("Text/Create")]
        [Authorize]
        public async Task<IActionResult> CreateText(CreateTextRequest input)
        {
            var userID = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _storageFileService.CreateTextAsync(input, new Guid(userID));
            return Ok("http://localhost:5000/StorageFile/Text/Access?id=" + result);
        }
        [HttpGet("Text/Access")]
        [AllowAnonymous]
        public async Task<IActionResult> AccessText(string id)
        {
            var userID = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);

            string currentUser = userID == null ? "Guest": userID.ToString();

            var result = await _storageFileService.AccessTextAsync(id, currentUser);
            return Ok(result);
        }
        [HttpGet("Text/List")]
        [Authorize]
        public async Task<IActionResult> GetTexts()
        {
            var result = await _storageFileService.GetTextsAsync();
            return Ok(result);
        }
        [HttpDelete("Text/Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteText(string id)
        {
            var result = await _storageFileService.DeleteTextAsync(id);
            if(result) return Ok(result);
            return BadRequest("Text did not exist in system");
        }

        [HttpPost("File/Create")]
        [Authorize]
        public async Task<IActionResult> CreateFile([FromForm] CreateFileRequest input)
        {
            var id = Guid.NewGuid();
            if (input.File.Length > 0)
            {
                var filePath = Directory.GetCurrentDirectory() + "\\Files";
                // Add temp file to get File Type
                var stream = System.IO.File.Create(Path.Combine(filePath, input.File.FileName));
                stream.Close();
                string ext = Path.GetExtension(Path.Combine(filePath, input.File.FileName));
                // Clone it to a new file with naming convention
                using (var stream2 = System.IO.File.Create(Path.Combine(filePath, id + ext)))
                {
                    await input.File.CopyToAsync(stream2);
                }
                // Remove the temp file
                System.IO.File.Delete(Path.Combine(filePath, input.File.FileName));

                var userID = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _storageFileService.CreateFileAsync(id, new Guid(userID), Path.Combine(filePath, id + ext) , input.DownloadOnce);
                if(result) return Ok("http://localhost:5000/StorageFile/File/Access?id=" + id);
                return BadRequest("Error");
            }
            return BadRequest("File not found");
        }
        [HttpGet("File/Access")]
        [AllowAnonymous]
        public async Task<IActionResult> AccessFile(string id)
        {
            var userID = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);

            string currentUser = userID == null ? "Guest" : userID.ToString();

            var result = await _storageFileService.GetServerFileURLAsync(id, currentUser);
            return Ok(result);
        }
        [HttpGet("File/List")]
        [Authorize]
        public async Task<IActionResult> GetFiles()
        {
            var result = await _storageFileService.GetFilesAsync();
            return Ok(result);
        }
        [HttpDelete("File/Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteFile(string id)
        {
            var result = await _storageFileService.DeleteFileAsync(id);
            if (result) return Ok(result);
            return BadRequest("File did not exist in system");
        }
    }
}
