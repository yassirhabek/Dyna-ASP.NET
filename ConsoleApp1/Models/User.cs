using Interfaces.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VerifiedPassword { get; set; }

        public User(UserDTO userDTO)
        {
            UserID = userDTO.UserID;
            Email = userDTO.Email;
            UserName = userDTO.UserName;
            Password = userDTO.HashedPassword;
        }

        public User(string email, string pw)
        {
            Email = email;
            Password = pw;
        }

        public User(int id, string email, string username, string password)
        {
            UserID = id;
            Email = email;
            UserName = username;
            Password = password;
        }

        public User(string email, string userName, string pw, string verPw)
        {
            Email = email;
            UserName = userName;
            Password = pw;
            VerifiedPassword = verPw;
        }
    }
}
