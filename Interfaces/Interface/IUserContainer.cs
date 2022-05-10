namespace Interfaces.Interface
{
    public interface IUserContainer
    {
        bool checkIfEmailExists(string email, string connString);
        bool Login(string connString, string email, string hashedPw);
        int Register(string connString, string email, string username, string hashedPw);
    }
}