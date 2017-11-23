using Ontourage.Core.Entities;
using System.Collections.Generic;

namespace Ontourage.Core.Interfaces
{
    public interface IHotelRepository
    {
        List<HotelAggregate> GetAllHotels();

        HotelAggregate GetHotelById(int id);

        void AddHotel(Hotel hotel);

        void EditHotel(Hotel hotel);

        void DeleteHotel(int id);
    }
}