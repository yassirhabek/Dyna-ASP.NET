using Interfaces.DTO;

namespace Interfaces.Interface
{
    public interface IUserContainer
    {
        bool checkIfEmailExists(string email);
        bool Login(UserDTO userDTO);
        int Register(UserDTO userDTO);
    }
}