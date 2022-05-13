using Logic.Containers;
using Logic.Models;
using DAL.DAL;
using Microsoft.AspNetCore.Mvc;
using TicketApplication.Models;

namespace TicketApplication.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserViewModel userModel)
        {
            List<string> errors = new List<string>();
            User user = new User(userModel.Email, userModel.Password);
            UserContainer userContainer = new UserContainer(new UserDAL());

            if (userContainer.Login(user, out errors))
            {
                return Ok();
            }
            else
            {
                return BadRequest(errors);
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserViewModel userModel)
        {
            List<string> errors = new List<string>();
            User user = new User(userModel.Email, userModel.UserName, userModel.Password, userModel.VerPw);
            UserContainer userContainer = new UserContainer(new UserDAL());

            if (userContainer.Register(user, out errors) == 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest(errors);
            }
        }

    }
}
