using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class BookingRepository : IBookingRepository
    {
        protected readonly ITrybeHotelContext _context;
        public BookingRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 9. Refatore o endpoint POST /booking
        public BookingResponse Add(BookingDtoInsert booking, string email)
        {
            try
            {
                var room = GetRoomById(booking.RoomId);
                if (room.Capacity < booking.GuestQuant)
                {
                    throw new Exception("Guest quantity over room capacity");
                }
                var hotel = _context.Hotels.Find(room.HotelId);
                var user = _context.Users.First(e => e.Email == email);
                var city = _context.Cities.First(c => c.CityId == hotel!.CityId);

                var myBook = new Booking()
                {
                    CheckIn = booking.CheckIn,
                    CheckOut = booking.CheckOut,
                    GuestQuant = booking.GuestQuant,
                    UserId = user.UserId,
                    RoomId = booking.RoomId
                };

                var responseBooking = _context.Bookings.Add(myBook);
                _context.SaveChanges();

                return new BookingResponse()
                {
                    bookingId = myBook.BookingId,
                    CheckIn = myBook.CheckIn,
                    CheckOut = myBook.CheckOut,
                    guestQuant = myBook.GuestQuant,
                    room = new RoomDto()
                    {
                        roomId = room.RoomId,
                        name = room.Name,
                        capacity = room.Capacity,
                        image = room.Image,
                        hotel = new HotelDto()
                        {
                            hotelId = hotel!.HotelId,
                            name = hotel.Name,
                            address = hotel.Address,
                            cityId = city.CityId,
                            cityName = city.Name,
                            state = city.State
                        }
                    }
                };

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        // 10. Refatore o endpoint GET /booking
        public BookingResponse GetBooking(int bookingId, string email)
        {
            try
            {
                var myBook = _context.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
                if (myBook != null)
                {
                    var user = _context.Users.FirstOrDefault(u => u.UserId == myBook.UserId);
                    if (user!.Email != email || user == null)
                    {
                        throw new Exception("Unauthorized Access");
                    }
                    var room = GetRoomById(myBook.RoomId);
                    var hotel = _context.Hotels.Find(room.HotelId);
                    var city = _context.Cities.FirstOrDefault(c => c.CityId == hotel!.CityId);
                    if (city != null)
                    {
                        return new BookingResponse()
                        {
                            bookingId = myBook.BookingId,
                            CheckIn = myBook.CheckIn,
                            CheckOut = myBook.CheckOut,
                            guestQuant = myBook.GuestQuant,
                            room = new RoomDto()
                            {
                                roomId = room.RoomId,
                                name = room.Name,
                                capacity = room.Capacity,
                                image = room.Image,
                                hotel = new HotelDto()
                                {
                                    hotelId = hotel!.HotelId,
                                    name = hotel.Name,
                                    address = hotel.Address,
                                    cityId = city.CityId,
                                    cityName = city.Name,
                                    state = city.State
                                }
                            }
                        };
                    }
                    else
                    {
                        throw new Exception("city not found");
                    }

                }
                else
                {
                    throw new Exception("Booking not found");
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Room GetRoomById(int RoomId)
        {
            var Room = _context.Rooms.FirstOrDefault(r => r.RoomId == RoomId);

            if (Room != null)
            {
                return Room;
            }
            else
            {
                throw new Exception("Room not Found");
            }
        }

    }

}