using System.Text.RegularExpressions;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ClientsController(
        AppDbContext context,
        ITokenService tokenService
        //IAzureStorage azureStorage
    ) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Client>>> GetCLients()
        {
            var clients = await context.Clients.ToListAsync();

            return clients;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Client>> GetClient(string name)
        {
            var client = await context.Clients.FindAsync(name);

            if (client == null)
                return NotFound();

            return client;
        }

        [HttpPost]
        public async Task<ActionResult<Client>> CreateClient(CreateClientDto createClientDto)
        {
            var client = new Client()
            {
                Name = createClientDto.Name,
                Cpf = createClientDto.Cpf,
                BirthDate = createClientDto.BirthDate,
                PassNumber = createClientDto.PassNumber,
                Telephone = createClientDto.Telephone,
                Email = createClientDto.Email,
                Code = createClientDto.Cpf,
            };

            context.Clients.Add(client);
            await context.SaveChangesAsync();

            return client;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ClientDto>> Login(ClientLoginDto loginDto)
        {
            var client = await context.Clients.SingleOrDefaultAsync(x => x.Code == loginDto.Code);

            if (client == null)
                return Unauthorized("Code");

            return new ClientDto
            {
                Name = client.Name,
                Email = client.Email,
                Token = tokenService.CreateToken(client.Email, client.Id),
            };
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
