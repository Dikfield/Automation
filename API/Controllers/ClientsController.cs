using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ClientsController(
        IClientRepository clientRepository,
        AppDbContext context,
        ITokenService tokenService
    //IAzureStorage azureStorage
    ) : BaseApiController
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

        // [HttpPost("addDocuments")]
        // public async Task<ActionResult<Document>> AddDocuments(DocumentDto documentDto)
        // {
        //     var client = await context.Clients.FindAsync(documentDto.ClientCpf);

        //     if (client == null)
        //         return Unauthorized("CLient not found");

        //     var document = new Document
        //     {
        //         FileName = documentDto.FileName,
        //         ContentType = documentDto.ContentType,
        //         Url = await azureStorage.UploadFileAsync(documentDto.File, documentDto.FileName),
        //     };

        //     client.Documents.Add(document);

        //     return document;
        // }
    }
}
