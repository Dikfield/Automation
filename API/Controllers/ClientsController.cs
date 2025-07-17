using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ClientsController(IClientRepository clientRepository, IFileService fileService)
        : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetCLients()
        {
            return Ok(await clientRepository.GetClientsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDto?>> GetClient(string id)
        {
            return await clientRepository.GetClientByIdAsync(id);
        }

        [HttpGet("{id}/documents")]
        public async Task<ActionResult<IReadOnlyList<Document>>> GetClientDocuments(string id)
        {
            var documents = await clientRepository.GetClientDocumentsByIdAsync(id);
            if (documents == null)
                return NotFound("Client not found");

            return documents.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<Client>> CreateClient(CreateClientDto createClientDto)
        {
            return await clientRepository.CreateClientAsync(createClientDto);
        }

        [HttpPost("AddDestiny")]
        public async Task<ActionResult<DestinyDto>> AddDestiny(DestinyDto destinyDto)
        {
            return await clientRepository.AddDestinyAsync(destinyDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ClientDto>> Login(ClientLoginDto loginDto)
        {
            var clientDto = await clientRepository.LoginAsync(loginDto);

            if (clientDto == null)
                return Unauthorized("Invalid code");

            return Ok(clientDto);
        }

        [HttpPost("uploadFile")]
        public async Task<ActionResult<string>> AddDocument(
            [FromForm] DocumentCreateDto documentCreateDto
        )
        {
            var client = await clientRepository.GetClientByIdInternalAsync(
                documentCreateDto.ClientId
            );

            if (client == null)
                return BadRequest("Client not found");

            var result = await fileService.UploadAsync(documentCreateDto.File);

            if (result.Error != null)
                return BadRequest(result.Error.Message);

            var document = new Document
            {
                FileName = documentCreateDto.File.FileName,
                ContentType = documentCreateDto.File.ContentType,
                Url = result.SecureUrl.ToString(),
                PublicId = result.PublicId,
                ClientId = documentCreateDto.ClientId,
            };
            client.Documents.Add(document);

            if (await clientRepository.SaveAllAsync())
                return Ok(document.FileName);

            return BadRequest("Problem saving document");
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadDocument(int id)
        {
            var document = await clientRepository.GetDocumentByIdAsync(id);

            if (document == null)
                return NotFound("Document not found");

            var resourceType = DetectResourceType(document.PublicId);

            var fileBytes = await fileService.DownloadFileAsync(document.PublicId, resourceType);

            return File(fileBytes, document.ContentType, document.FileName);
        }

        private string DetectResourceType(string publicId)
        {
            var ext = Path.GetExtension(publicId).ToLowerInvariant();

            if (string.IsNullOrWhiteSpace(ext))
                return "raw";

            return ext switch
            {
                ".jpg" or ".jpeg" or ".png" or ".gif" or ".webp" => "image",
                _ => "raw",
            };
        }
    }
}
