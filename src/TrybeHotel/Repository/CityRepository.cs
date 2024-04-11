using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class CityRepository : ICityRepository
    {
        protected readonly ITrybeHotelContext _context;
        public CityRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 4. Refatore o endpoint GET /city
        public IEnumerable<CityDto> GetCities()
        {
            try
            {
                var response = _context.Cities.Select(e => new CityDto() { cityId = e.CityId, name = e.Name, state = e.State }).ToList();
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // 2. Refatore o endpoint POST /city
        public CityDto AddCity(City city)
        {
            try
            {
                _context.Cities.Add(city);
                _context.SaveChanges();
                return new CityDto() { cityId = city.CityId, name = city.Name, state = city.State };
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        // 3. Desenvolva o endpoint PUT /city
        public CityDto UpdateCity(City city)
        {
            var cityOld = _context.Cities.FirstOrDefault(c => c.CityId == city.CityId);
            if (cityOld != null)
            {
                cityOld.Name = city.Name;
                cityOld.State = city.State;
                _context.SaveChanges();
                return new CityDto() { cityId = city.CityId, name = city.Name, state = city.State };
            }
            else
            {
                throw new Exception("City Not Found");
            }
        }

    }
}