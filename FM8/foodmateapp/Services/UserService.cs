using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using foodmateapp.Model;
using foodmateapp.Helpers;


namespace foodmateapp.Services
//{
//    public interface IUserService
//    {
//        Users Authenticate(string Nickname, string Pswd);
//        IEnumerable<Users> GetAll();
//    }

//    public class UserService : IUserService
//    {
//        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
//        private List<Users> _users = new List<Users>
//        {
//            new Users { UserId = 7, FirstName = "Test", LastName = "User", Nickname = "test", Pswd = "test" }
//        };

//        private readonly AppSettings _appSettings;

//        public UserService(IOptions<AppSettings> appSettings)
//        {
//            _appSettings = appSettings.Value;
//        }

//        public Users Authenticate(string Nickname, string Pswd)
//        {
//            var user = _users.SingleOrDefault(x => x.Nickname == Nickname && x.Pswd == Pswd);

//            // return null if user not found
//            if (user == null)
//                return null;

//            // authentication successful so generate jwt token
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(new Claim[]
//                {
//                    new Claim(ClaimTypes.Name, user.UserId.ToString())
//                }),
//                Expires = DateTime.UtcNow.AddDays(7),
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//            };
//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            user.Token = tokenHandler.WriteToken(token);

//            return user.WithoutPassword();
//        }

//        public IEnumerable<Users> GetAll()
//        {
//            return _users.WithoutPasswords();
//        }
//    }
//}

//{
//    public interface IUserService
//{
//    User Authenticate(string username, string password);
//    IEnumerable<User> GetAll();
//}

{
    public interface IUserService
    {
        Users Authenticate(string username, string password);
        IEnumerable<Users> GetAll();
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<Users> _users = new List<Users>
        {
            new Users {FirstName = "Test", LastName = "User", Sex="M", Nickname = "test", Pswd = "test" }
        };


        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public Users Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Nickname == username && x.Pswd == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public IEnumerable<Users> GetAll()
        {
            return _users.WithoutPasswords();
        }
    }
}