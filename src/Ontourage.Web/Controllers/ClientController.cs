using System;
using Microsoft.AspNetCore.Mvc;
using Ontourage.Core.Entities;
using Ontourage.Core.Interfaces;
using Ontourage.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ontourage.Web.Controllers
{
    public class ClientController : Controller
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IClientRepository _clientRepository;
        private readonly List<string> _sex = new List<string>
        {
            "Мужской",
            "Женский"
        };

        public ClientController(IDiscountRepository discountRepository,
            IPaymentChecksRepository paymentChecksRepository,
            IClientRepository clientRepository)
        {
            _discountRepository = discountRepository;
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public IActionResult GetAllClients()
        {
            var model = new ClientBaseViewModel
            {
                Clients = _clientRepository.GetAllClients()
                    .Select(c => new ClientAggregateViewModel(c)).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult GetRegularClients()
        {
            var model = new ClientBaseViewModel
            {
                Clients = _clientRepository.GetReguralClients().
                    Select(c => new ClientAggregateViewModel(c)).ToList()
            };
            return View("GetAllClients", model);
        }

        [HttpGet]
        public IActionResult SortClients()
        {
            var model = new ClientBaseViewModel
            {
                Clients = _clientRepository.GetAllClients().OrderBy(c => c.FirstName)
                    .Select(c => new ClientAggregateViewModel(c)).ToList()
            };
            return View("GetAllClients", model);
        }

        [HttpPost]
        public IActionResult SearchClient(string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                return RedirectToAction("GetAllClients");
            }
            var model = new ClientBaseViewModel
            {
                Clients = _clientRepository.GetAllClients().
                    Where(c => c.FirstName.ToLower() == searchString.ToLower() ||
                               c.LastName.ToLower() == searchString.ToLower())
                    .Select(c => new ClientAggregateViewModel(c)).ToList()
            };
            return View("GetAllClients", model);
        }

        [HttpGet]
        public IActionResult AddClient()
        {
            var model = new ClientViewModel
            {
                Header = new HeaderViewModel("Добавление клиента", "AddClient"),
                ChooseSex = _sex,
                Discounts = _discountRepository.GetAllDiscounts()
            };
            return View("AddEditClient", model);
        }

        
        [HttpPost]
        public IActionResult AddClient(ClientViewModel addModel)
        {
            if (ModelState.IsValid)
            {
                Client client = addModel.CreateFromViewModel();
                _clientRepository.AddNewClient(client);
                return RedirectToAction("GetAllClients");
            }
            return RedirectToAction("AddClient");
        }

        [HttpGet]
        public IActionResult DeleteClient(int id)
        {
            _clientRepository.DeleteClient(id);
            return RedirectToAction("GetAllClients");
        }

        [HttpGet]
        public IActionResult ViewDetails(int id)
        {
            var clientToDetails = _clientRepository.GetClientById(id);
            var model = new ClientAggregateViewModel(clientToDetails);
            model.BindFromModel(clientToDetails);
            return View("ViewDetails", model);
        }

        [HttpGet]
        public IActionResult EditClient(int id)
        {
            var clientToEdit = _clientRepository.GetClientById(id);
            var model = new ClientViewModel
            {
                Header = new HeaderViewModel("Редактирование клиента", "EditClient"),
                ChooseSex = _sex,
                Discounts = _discountRepository.GetAllDiscounts()
            };
            model.BindFromModel(clientToEdit);
            return View("AddEditClient", model);
        }


        [HttpPost]
        public IActionResult EditClient(ClientViewModel editModel)
        {
            if (ModelState.IsValid)
            {
                Client client = editModel.CreateFromViewModel();
                _clientRepository.EditClient(client);
            }
            return RedirectToAction("GetAllClients");
        }
    }
}