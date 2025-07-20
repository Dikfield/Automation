using System.Text.Json;
using API.DTOs;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedClients(AppDbContext context)
        {
            if (await context.Clients.AnyAsync())
                return;

            var clientData = await File.ReadAllTextAsync("Data/ClientSeedData.json");
            var clients = JsonSerializer.Deserialize<List<ClientSeedDto>>(clientData);

            if (clients == null)
            {
                Console.WriteLine("No CLients in seed date");
                return;
            }

            foreach (var clientDto in clients)
            {
                var client = new Client
                {
                    Id = clientDto.Id,
                    Name = clientDto.Name,
                    Email = clientDto.Email,
                    Telephone = clientDto.Telephone,
                    Cpf = clientDto.Cpf,
                    Code = clientDto.Code,
                    BirthDate = clientDto.BirthDate,
                    PassNumber = clientDto.PassNumber,
                    Token = string.Empty,
                };

                foreach (var dest in clientDto.Destinies)
                {
                    var destiny = new Destiny
                    {
                        Country = dest.Country,
                        City = dest.City,
                        TravelDate = DateTime.SpecifyKind(dest.TravelDate, DateTimeKind.Utc),
                        Persons = dest.Persons,
                        Tours = dest.Tours,
                        ClientId = client.Id,
                    };
                    client.Destinies.Add(destiny);
                }

                context.Clients.Add(client);
            }
            ;
            await context.SaveChangesAsync();
        }
    }
}
