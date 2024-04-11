using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class RoomRepository : IRoomRepository
    {
        protected readonly ITrybeHotelContext _context;
        public RoomRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 7. Refatore o endpoint GET /room
        public IEnumerable<RoomDto> GetRooms(int HotelId)
        {
           try
            {
                var Rooms = from room in _context.Rooms
                            join hotel in _context.Hotels on room.HotelId equals hotel.HotelId
                            where room.HotelId == HotelId
                            select new RoomDto()
                            {
                                roomId = room.RoomId,
                                name = room.Name,
                                capacity = room.Capacity,
                                image = room.Image,
                                hotel = new HotelDto()
                                {
                                    name = hotel.Name,
                                    address = hotel.Address,
                                    cityId = hotel.CityId,
                                    cityName = hotel.City!.Name,
                                    state = hotel.City.State
                                }
                            };
            return Rooms;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        // 8. Refatore o endpoint POST /room
        public RoomDto AddRoom(Room room) {
            try
            {
                _context.Rooms.Add(room);
                _context.SaveChanges();
                var hotel = _context.Hotels.FirstOrDefault(h => h.HotelId == room.HotelId);
                if(hotel != null){
                var city = _context.Cities.FirstOrDefault(c => c.CityId == hotel.CityId);
                if(city != null) {
                    return new RoomDto()
                {
                    roomId = room.RoomId,
                    name = room.Name,
                    capacity = room.Capacity,
                    image = room.Image,
                    hotel = new HotelDto() {
                        hotelId = hotel.HotelId,
                        name = hotel.Name,
                        address = hotel.Address,
                        cityId = city.CityId,
                        cityName = city.Name,
                        state = city.State
                    }
                };
                } else {
                    throw new Exception("City Not Found");
                }
                } else {
                    throw new Exception("Hotel Not Found");
                }
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }

        public void DeleteRoom(int RoomId) {
           try
            {
                var room = _context.Rooms.FirstOrDefault(r => r.RoomId == RoomId);
                _context.Rooms.Remove(room!);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }
    }
}