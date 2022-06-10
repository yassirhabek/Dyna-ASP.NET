using Interfaces.DTO;
using Interfaces.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Test.Stubs
{
    public class UserDalStub : IUserContainer
    {
        public List<UserDTO> UserDTOs { get; set; } = new List<UserDTO>()
        {
            new UserDTO(){ UserID = 1, UserName = "yassir", Email = "yassirhabek@hotmail.com"},
            new UserDTO(){ UserID = 2, UserName = "xyz456", Email = "Test321@hotmail.com", HashedPassword = "990b3146d7c382446c076debf4c2dc222aa5d6f907a5e37f8c1b4ad42a6ccab8"}
        };

        public bool checkIfEmailExists(string email)
        {
            return UserDTOs.Where(x => x.Email == email).Any();
        }

        public bool Login(UserDTO userDTO)
        {
            if (!UserDTOs.Where(x => x.Email == userDTO.Email).Any())
                return false;

            if (!UserDTOs.Where(x => x.HashedPassword == userDTO.HashedPassword).Any())
                    return false;

            return true;
        }

        public int Register(UserDTO userDTO)
        {
            UserDTOs.Add(userDTO);
            return 1;
        }

        public UserDTO GetSingleUser(UserDTO userDTO)
        {
            return UserDTOs.SingleOrDefault(x => x.Email == userDTO.Email);
        }
    }
}
