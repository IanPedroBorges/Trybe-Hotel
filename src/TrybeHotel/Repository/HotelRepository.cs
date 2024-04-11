using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class HotelRepository : IHotelRepository
    {
        protected readonly ITrybeHotelContext _context;
        public HotelRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        //  5. Refatore o endpoint GET /hotel
        public IEnumerable<HotelDto> GetHotels()
        {
            try
            {
                var AllHotels = _context.Hotels.Select(e => new HotelDto()
                {
                    hotelId = e.HotelId,
                    name = e.Name,
                    address = e.Address,
                    cityId = e.City!.CityId,
                    cityName = e.City.Name,
                    state = e.City.State
                }).ToList();
                return AllHotels;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        // 6. Refatore o endpoint POST /hotel
        public HotelDto AddHotel(Hotel hotel)
        {
            try
            {
                _context.Hotels.Add(hotel);
                _context.SaveChanges();
                var city = _context.Cities.FirstOrDefault(c => c.CityId == hotel.CityId);

                if (city != null) {
                    return new HotelDto() {
                    hotelId = hotel.HotelId,
                    name = hotel.Name,
                    address = hotel.Address,
                    cityId = city.CityId,
                    cityName = city.Name,
                    state = city.State
                };
                } else {
                    throw new Exception("City Not Found");
                }
                
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}