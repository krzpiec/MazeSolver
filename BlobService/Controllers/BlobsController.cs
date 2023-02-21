using System.Net;
using BlobService.Dtos;
using BlobService.Services;
using Microsoft.AspNetCore.Mvc;


namespace BlobService.AddControllers
{
    [Route("api/")]
    [ApiController]
    public class BlobsController : Controller
    {
        private readonly IBlobService _blobService;

        public BlobsController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpGet("{blobName}", Name = "GetBlob")]
        public async Task<string> getBlob(long blobName)
        {
            Console.WriteLine(blobName);
            return await _blobService.getBlob(blobName);

        }

        [HttpDelete("{blobName}", Name = "DeleteBlob")]
        public async Task<int> deleteBlob(long blobName)
        {
            return await _blobService.deleteBlob(blobName);
        }

        [HttpPost("upload", Name = "UploadBlob")]
        public async Task<ActionResult<HttpStatusCode>> uploadBlob(BlobDto blobDto)
        {
            Console.WriteLine("blob");
            Console.WriteLine(blobDto.Id);
            var responseCode =  await _blobService.uploadBlob(blobDto.Id, blobDto.Content);
            return (HttpStatusCode)responseCode;
        }

    }
}