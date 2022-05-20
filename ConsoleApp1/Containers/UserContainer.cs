using Logic.Models;
using Interfaces.Interface;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Interfaces.DTO;

namespace Logic.Containers
{
    public class UserContainer
    {
        private IUserContainer _iUserContainer;

        public UserContainer(IUserContainer iUserContainer)
        {
            _iUserContainer = iUserContainer;
        }

        /// <summary>
        /// Attempt to login with username and password(hashed) and map return data to object of T
        /// </summary>
        /// <typeparam name="T">UserDTO</typeparam>
        /// <param name="email">userinput email</param>
        /// <param name="password">userinput password</param>
        /// <param name="errors">list of errors</param>
        /// <returns>returns T when successful else returns null and list of errors</returns>
        public bool Login(User user, out List<string> errors)
        {
            errors = new List<string>();

            // validate if values are filled
            if (string.IsNullOrEmpty(user.Email))
            {
                errors.Add("email is required");
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                errors.Add("password is required");
            }

            if (errors.Count == 0) // if all required fields are properly filled
            {
                string hashedPw = ComputeSha256Hash(user.Password + user.Email);
                UserDTO userDTO = new UserDTO() { Email = user.Email, HashedPassword = hashedPw};
                return _iUserContainer.Login(userDTO);
            }
            errors.Add("unexpected error");
            return false;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="username"></param>
        /// <param name="verifyPassword"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public int Register(User user, out List<string> errors)
        {
            errors = new List<string>();

            if (string.IsNullOrEmpty(user.Email))
            {
                errors.Add("email is required");
            }
            if (string.IsNullOrEmpty(user.UserName))
            {
                errors.Add("username is required");
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                errors.Add("password is required");
            }
            if (string.IsNullOrEmpty(user.VerifiedPassword))
            {
                errors.Add("password confirmation is required");
            }

            if (errors.Count == 0)
            {
                string emailRegEx = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                    + "@"
                    + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z";
                if (!Regex.IsMatch(user.Email, emailRegEx))
                {
                    errors.Add("email must be valid");
                }
                if (_iUserContainer.checkIfEmailExists(user.Email))
                {
                    errors.Add("account already linked to this email");
                }
                if (user.Password.Length < 7)
                {
                    errors.Add("password must be at least 8 characters");
                }
                if (!Regex.IsMatch(user.Password, @"[a-z]"))
                {
                    errors.Add("password must contain at least 1 lowercase letter");
                }
                if (!Regex.IsMatch(user.Password, @"[A-Z]"))
                {
                    errors.Add("password must contain at least 1 uppercase letter");
                }
                if (!Regex.IsMatch(user.Password, @"(?=.*\d)|(?=.*[!-\/:-@[-`{-~])")) // digit | symbol
                {
                    errors.Add("password must contain at least 1 symbol/digit");
                }
                if (user.Password != user.VerifiedPassword)
                {
                    errors.Add("two passwords must match");
                }
            }

            if (errors.Count == 0) // if any errors occurred
            {
                string passwordHash = ComputeSha256Hash(user.Password + user.Email);
                UserDTO userDTO = new UserDTO()
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    HashedPassword = passwordHash,
                };
                return _iUserContainer.Register(userDTO);
            }
            else
                return 0;
        }
        private string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public User GetSingleUser(User user)
        {
            UserDTO userDTO = new UserDTO()
            {
                Email = user.Email
            };
            UserDTO completedUserDTO = _iUserContainer.GetSingleUser(userDTO);

            User completedUser = new User(completedUserDTO);
            return completedUser;
        }
    }
}
