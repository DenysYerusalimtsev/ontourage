using Ontourage.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ontourage.Core.Interfaces
{
    public interface IClientRepository
    {
        List<ClientAggregate> GetAllClients();

        void AddNewClient(Client client);

        Client ViewDetails(Client client);

        ClientAggregate GetClientById(int id);

        void DeleteClient(int id);

        void EditClient(Client client);

        List<ClientAggregate> GetReguralClients();

    }
}
