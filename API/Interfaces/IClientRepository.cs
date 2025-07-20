using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IClientRepository
    {
        void Update(Client client);
        Task<bool> SaveAllAsync();
        Task<Client> CreateClientAsync(CreateClientDto client);
        Task<DestinyDto> AddDestinyAsync(DestinyDto destiny);
        Task<IReadOnlyList<ClientDto>> GetClientsAsync();
        Task<ClientDto?> GetClientByIdAsync(string id);
        Task<ClientDto?> LoginClient(ClientLoginDto loginDto);
        Task<Document?> GetDocumentByIdAsync(int id);
        Task<IReadOnlyList<DocumentDto>> GetClientDocumentsByIdAsync(string id);
        Task<Client?> GetClientByIdInternalAsync(string id);
    }
}
