using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VNWalks.Infrastructure.Data;
using VNWalks.Shared.EntityModels.SqlServer.DTO;
using VNWalks.Shared.EntityModels.SqlServer.EntityModels;

namespace VNWalks.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly VNWalksDbContext _context;

        public ImageController(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor,
            VNWalksDbContext _context)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this._context = _context;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] UploadImageRequestDTO uploadImageRequestDTO)
        {
            ValidateFileUpload(uploadImageRequestDTO);

            if (ModelState.IsValid)
            {
                var image = new Image
                {
                    File = uploadImageRequestDTO.File,
                    FileExtension = Path.GetExtension(uploadImageRequestDTO.File.FileName),
                    FileSizeInBytes = uploadImageRequestDTO.File.Length,
                    FileName = uploadImageRequestDTO.FileName,
                    FileDescription = uploadImageRequestDTO.FileDescription,
                };

                await Upload(image);

                return Ok(image);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(UploadImageRequestDTO uploadImageRequestDTO)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(uploadImageRequestDTO.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (uploadImageRequestDTO.File.Length > 3048576)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file.");
            }
        }


        private async Task Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            // Upload Image to local Path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);
            
            // https://localhost:1234/images/image.jpg
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();
        }
    }
}
