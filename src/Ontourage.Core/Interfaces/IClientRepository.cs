using Ontourage.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ontourage.Core.Interfaces
{
    public interface IClientRepository
    {
        List<Client> GetAllClients();

        void AddNewClient(Client client);

        Client ViewDetails(Client client);

        Client GetClientById(int id);

        void DeleteClient(int id);

        Client EditClient(Client client);
    }
}
