using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saritasa.BAL.StorageFile;
using System.Security.Claims;
using ViewModels.StorageFile;

namespace Saritasa.BackendApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class StorageFileController : Controller
    {
        private readonly IStorageFileService _storageFileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StorageFileController(IStorageFileService storageFileService, IHttpContextAccessor httpContextAccessor)
        {
            _storageFileService = storageFileService;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateText(CreateTextRequest input)
        {
            var userID = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _storageFileService.CreateText(input, new Guid(userID));
            return Ok("http://localhost:5000/StorageFile/AccessText/" + result);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> AccessText(string id)
        {
            var userID = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            string currentUser = userID == null ? "Guest": userID.ToString();

            var result = await _storageFileService.AccessText(id, currentUser);
            return Ok(result);
        }
    }
}
