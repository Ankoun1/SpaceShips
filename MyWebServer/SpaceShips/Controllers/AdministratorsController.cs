namespace SpaceShips.Controllers
{
    using System.Linq;
    using MyWebServer.Controllers;
    using MyWebServer.Http;    
    using Models.Administrator;
    using Services.Users;
    using Services.Validators;

    public class AdministratorsController : Controller
    {
        private readonly IValidator validator;       
        private readonly IUserService userService;       

        public AdministratorsController(IValidator validator, IUserService userService)
        {
            this.validator = validator;                       
            this.userService = userService;            
        }

        public HttpResponse Register() => View();

        [HttpPost]
        public HttpResponse Register(AdministratorRegisterModel user)
        {
            var errorsModels = this.validator.ValidateRegisterUser(user);            

            if (errorsModels.Any())
            {
                return Error(errorsModels);
            }
            userService.CreateAdministrator(user.Username, user.Password);

            return Redirect("/Administrators/Login");
        }

        public HttpResponse Login() => View();

        [HttpPost]
        public HttpResponse Login(AdministratorLoginFormModel user)
        {
           
            string userId = userService.GetUserId(user.Username, user.Password);

            if (userId == null)
            {
                return Error("Username and password combination is not valid.");
            }

            this.SignIn(userId);           

            return Redirect("/Home/Index");
        } 
        [Authorize]
        public HttpResponse Logout()
        {

            if (!userService.AdministratorExists())
            {
                return Unauthorized();
            }
            this.SignOut();
            return Redirect("/Home/Index");
        }
    }
}
