using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ClientRepository(AppDbContext context, ITokenService tokenService)
        : IClientRepository
    {
        public async Task<Client> CreateClientAsync(CreateClientDto createClientDto)
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
                Token = string.Empty,
            };

            context.Clients.Add(client);
            await SaveAllAsync();

            return client;
        }

        public async Task<DestinyDto> AddDestinyAsync(DestinyDto destiny)
        {
            var client =
                GetClientByIdAsync(destiny.ClientID)
                ?? throw new ArgumentException("Client not found");

            var newDestiny = new Destiny
            {
                Country = destiny.Country,
                City = destiny.City,
                TravelDate = destiny.TravelDate,
                Persons = destiny.Persons,
                Tours = destiny.Tours,
                ClientId = destiny.ClientID,
            };

            context.Destinies.Add(newDestiny);
            await SaveAllAsync();

            var destinyDto = new DestinyDto
            {
                Id = newDestiny.Id,
                Country = newDestiny.Country,
                City = newDestiny.City,
                TravelDate = newDestiny.TravelDate,
                Persons = newDestiny.Persons,
                Tours = newDestiny.Tours,
                ClientID = newDestiny.ClientId,
            };

            return destinyDto;
        }

        public async Task<Document?> GetDocumentByIdAsync(int id)
        {
            var document = await context.Documents.FindAsync(id);
            if (document == null)
                return null;

            return document;
        }

        public async Task<ClientDto?> GetClientByIdAsync(string id)
        {
            var client = await context
                .Clients.Include(c => c.Documents)
                .Include(c => c.Destinies)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
                return null;
            return CreateClientDto(client);
        }

        public async Task<ClientDto?> LoginClient(ClientLoginDto clientLoginDto)
        {
            var client = await context
                .Clients.Include(c => c.Documents)
                .Include(c => c.Destinies)
                .FirstOrDefaultAsync(c => c.Cpf == clientLoginDto.cpf);

            if (client == null)
                return null;

            client.Token = tokenService.CreateToken(client.Email, client.Id);

            return CreateClientDto(client);
        }

        public async Task<Client?> GetClientByIdInternalAsync(string id)
        {
            var client = await context
                .Clients.Include(c => c.Documents)
                .Include(c => c.Destinies)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
                return null;
            return client;
        }

        public async Task<IReadOnlyList<ClientDto>> GetClientsAsync()
        {
            var clients = await context
                .Clients.Include(c => c.Documents)
                .Include(c => c.Destinies)
                .ToListAsync();

            return clients.Select(CreateClientDto).ToList();
        }

        public async Task<IReadOnlyList<DocumentDto>> GetClientDocumentsByIdAsync(string id)
        {
            var client = await context
                .Clients.Include(c => c.Documents)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
                return new List<DocumentDto>();

            return client
                .Documents.Select(d => new DocumentDto
                {
                    Id = d.Id,
                    FileName = d.FileName,
                    ClientID = d.ClientId,
                    UploadedAt = d.UploadedAt,
                    PublicId = d.PublicId,
                    ContentType = d.ContentType,
                    Url = d.Url,
                })
                .ToList();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void Update(Client client)
        {
            context.Entry(client).State = EntityState.Modified;
        }

        private ClientDto CreateClientDto(Client client)
        {
            return new ClientDto
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email,
                Telephone = client.Telephone,
                Cpf = client.Cpf,
                PassNumber = client.PassNumber,
                BirthDate = client.BirthDate,
                Created = client.Created,
                Documents = client
                    .Documents.Select(d => new DocumentDto
                    {
                        Id = d.Id,
                        FileName = d.FileName,
                        ContentType = d.ContentType,
                        ClientID = d.ClientId,
                        PublicId = d.PublicId,
                        Url = d.Url,
                    })
                    .ToList(),
                Destinies = client
                    .Destinies.Select(dst => new DestinyDto
                    {
                        Id = dst.Id,
                        Country = dst.Country,
                        City = dst.City,
                        TravelDate = dst.TravelDate,
                        Persons = dst.Persons,
                        Tours = dst.Tours,
                        ClientID = dst.ClientId,
                    })
                    .ToList(),
                Code = client.Code,
                Token = client.Token,
            };
        }
    }
}
