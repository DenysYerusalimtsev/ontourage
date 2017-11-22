using Ontourage.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Ontourage.Core.Entities;
using System.Linq;

namespace Ontourage.DataAccess.SqlServer
{
    public class DbClientRepository : IClientRepository
    {
        private static List<Client> _clients = new List<Client>
        {
            new Client(1, "Леонид", "Иванов", "Мужской", new DateTime(),
                "МН391240", "+380875367549", "ivanov@mail.ru", 0, 0),
            new Client(2, "Петр", "Ершов", "Мужской", new DateTime(),
                "МН391240", "+380457867549", "ershov@ukr.net", 1, 5),
            new Client(3, "Илья", "Коваленко", "Мужской", new DateTime(),
                "МН391240", "+380875367549", "kovalenko@gmail.com", 2, 7)
        };
        private int _id = 3;

        public void AddNewClient(Client client)
        {
            _id++;
            _clients.Add(client);
        }

        public void DeleteClient(int id)
        {
            var clientToDelete = GetClientById(id);
            _clients.Remove(clientToDelete);
        }

        public Client EditClient(Client client)
        {
            DeleteClient(client.Id);
            AddNewClient(client);
            return client;
        }

        public List<Client> GetAllClients()
        {
            return _clients;
        }

        public Client GetClientById(int id)
        {
            return _clients.Where(с => с.Id == id).FirstOrDefault();
        }

        public Client ViewDetails(Client client)
        {
            return client;
        }
    }
}
