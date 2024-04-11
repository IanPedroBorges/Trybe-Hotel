using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly ITrybeHotelContext _context;
        public UserRepository(ITrybeHotelContext context)
        {
            _context = context;
        }
        public UserDto GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public UserDto Login(LoginDto login)
        {
            try
           {
              var userLogin = _context.Users.FirstOrDefault(u => u.Email == login.Email && u.Password == login.Password);
              if(userLogin != null) {
                return new UserDto(){
                    Email = userLogin.Email,
                    Name = userLogin.Name,
                    userId = userLogin.UserId,
                    userType = userLogin.UserType
                };
              } else {
                throw new Exception("Incorrect e-mail or password");
              }
           }
           catch (Exception e)
           {
            
            throw new Exception(e.Message);
           }
        }
        public UserDto Add(UserDtoInsert user)
        {
            try
            {
                var userExist = _context.Users.FirstOrDefault(u => u.Email == user.Email);
                if(userExist != null) {
                    throw new Exception("User email already exists");
                } else {
                    var newUser = new User(){ Email = user.Email, Name = user.Name, Password = user.Password, UserType = "client"   };
                    _context.Users.Add(newUser);
                    _context.SaveChanges();
                    var responseNewUser = _context.Users.First(u => u.Email == newUser.Email);
                    return new UserDto(){
                        Email = responseNewUser.Email,
                        Name = responseNewUser.Name,
                        userId = responseNewUser.UserId,
                        userType = responseNewUser.UserType
                    };
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public UserDto GetUserByEmail(string userEmail)
        {
             throw new NotImplementedException();
        }

        public IEnumerable<UserDto> GetUsers()
        {
            var allUsers = _context.Users;
           return allUsers.Select(u => new UserDto(){
            userId = u.UserId,
            Name = u.Name,
            Email = u.Email,
            userType = u.UserType
           });
        }

    }
}