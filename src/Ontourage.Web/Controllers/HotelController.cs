using Microsoft.AspNetCore.Mvc;
using Ontourage.Core.Entities;
using Ontourage.Core.Interfaces;
using Ontourage.Web.Models;
using System.Linq;

namespace Ontourage.Web.Controllers
{
    public class HotelController : Controller
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly ICountryRepository _countryRepository;

        public HotelController(IHotelRepository hotelRepository, ICountryRepository countryRepository)
        {
            _hotelRepository = hotelRepository;
            _countryRepository = countryRepository;
        }

        public IActionResult GetAllHotels()
        {
            var model = new HotelStoreViewModel
            {
                Hotels = _hotelRepository.GetAllHotels()
                .Select(h => new HotelAggregateViewModel(h)).ToList()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult AddHotel()
        {
            var model = new HotelViewModel
            {
                Header = new HeaderViewModel("Добавить отель", "AddHotel"),
                Countries = _countryRepository.GetAllCoutries(),
            };
            return View("AddEditHotel", model);
        }

        [HttpPost]
        public IActionResult AddHotel(HotelViewModel model)
        {
            if (ModelState.IsValid)
            {
                Hotel hotel = model.CreateFromModel();
                _hotelRepository.AddHotel(hotel);
                return RedirectToAction("GetAllHotels");
            }
            return RedirectToAction("AddHotel");
        }

        [HttpGet]
        public IActionResult DeleteHotel(int id)
        {
            _hotelRepository.DeleteHotel(id);
            return RedirectToAction("GetAllHotels");
        }

        [HttpGet]
        public IActionResult ViewDetails(int id)
        {
            var hotelToDetails = _hotelRepository.GetHotelById(id);
            var model = new HotelAggregateViewModel(hotelToDetails)
            {
                Header = new HeaderViewModel("Просмотр отеля", "ViewDetails")
            };
            return View("ViewDetails", model);
        }

        [HttpGet]
        public IActionResult EditHotel(int id)
        {
            var hotelToEdit = _hotelRepository.GetHotelById(id);
            var model = new HotelViewModel
            {
                Header = new HeaderViewModel("Редактирование отеля", "EditHotel"),
                Countries = _countryRepository.GetAllCoutries(),
            };
            model.BindFromModel(hotelToEdit);
            return View("AddEditHotel", model);
        }

        [HttpPost]
        public IActionResult EditHotel(HotelViewModel hotelToEdit)
        {
            if (ModelState.IsValid)
            {
                Hotel hotel = hotelToEdit.CreateFromModel();
                _hotelRepository.EditHotel(hotel);
                return RedirectToAction("GetAllHotels");
            }
            return RedirectToAction("EditHotel");
        }
    }
}