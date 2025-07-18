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
        public async Task<ActionResult<IReadOnlyList<DocumentDto>>> GetClientDocuments(string id)
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
        public async Task<ActionResult<DocumentDto>> AddDocument(
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

            var documentDto = new DocumentDto
            {
                FileName = document.FileName,
                ContentType = document.ContentType,
                Url = document.Url,
                PublicId = document.PublicId,
                ClientID = document.ClientId,
            };

            if (await clientRepository.SaveAllAsync())
                return Ok(documentDto);

            return BadRequest("Problem saving document");
        }

        [HttpDelete("deletefile")]
        public async Task<ActionResult> DeleteFile([FromBody] DeleteFileDto deleteFileDto)
        {
            var client = await clientRepository.GetClientByIdInternalAsync(deleteFileDto.ClientId);

            if (client == null)
                return BadRequest("Cannot get member from Id");

            var file = client.Documents.SingleOrDefault(x => x.Id == deleteFileDto.Id);

            if (file == null)
                return BadRequest("Cannot get file from Id");

            if (file.PublicId != null)
            {
                var result = await fileService.DeleteFileAsync(file.PublicId);
                if (result.Error != null)
                    return BadRequest(result.Error.Message);
            }

            client.Documents.Remove(file);

            if (await clientRepository.SaveAllAsync())
                return Ok();

            return BadRequest("Problem deleting the Document");
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadDocument(int id)
        {
            try
            {
                var document = await clientRepository.GetDocumentByIdAsync(id);
                if (document == null)
                    return NotFound("Document not found");

                var resourceType = DetectResourceType(document.ContentType);

                var fileBytes = await fileService.DownloadFileAsync(
                    document.PublicId,
                    resourceType
                );

                return File(fileBytes, document.ContentType, document.FileName);
            }
            catch (Exception ex)
            {
                // Logue aqui o erro para depuração
                Console.WriteLine("Erro no download: " + ex.Message);
                return StatusCode(500, "Erro interno ao baixar o documento: " + ex.Message);
            }
        }

        private string DetectResourceType(string contentType)
        {
            if (contentType.StartsWith("image/"))
                return "image";
            if (string.IsNullOrWhiteSpace(contentType))
                return "raw";
            if (contentType.StartsWith("video/"))
                return "video";
            return "raw";
        }
    }
}
