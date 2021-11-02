namespace SpaceShips.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using Models.SpaceShips;
    using Services.Users;
    using Services.Validators;
    using System.Linq;
    using Services.SpaceShips;

    public class SpaceShipsController : Controller
    {
        private readonly IValidator validator;
        private readonly IUserService userService;
        private readonly ISpaceShipService spaceShipService;
        

        public SpaceShipsController(
            IValidator validator,
            IUserService userService, ISpaceShipService spaceShipService)
        {
            this.validator = validator;
            this.userService = userService;            
            this.spaceShipService = spaceShipService;            
        }

        [Authorize]
        public HttpResponse Create()
        {
            if (!userService.AdministratorExists())
            {
                return Unauthorized();
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Create(SpaceShipRegisterModel spaceShip)
        {
            if (!userService.AdministratorExists())
            {
                return Unauthorized();
            }

            var modelErrors = this.validator.ValidateShipRegister(spaceShip);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }
            
            spaceShipService.CreateSpaceShip(spaceShip, User.Id);
            return Redirect("/SpaceShips/All");
        }

        [Authorize]
        public HttpResponse Update()
        {
            if (!userService.AdministratorExists())
            {
                return Unauthorized();
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Update(SpaceShipUpdateModel spaceShip,int id)
        {
            if (!userService.AdministratorExists())
            {
                return Unauthorized();
            }

            var modelErrors = this.validator.ValidateShipUpdate(spaceShip,id);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }
            
            spaceShipService.UpdateSpaceShip(spaceShip,id);
            return Redirect("/SpaceShips/All");
        }
        
        [Authorize]
        public HttpResponse All()
        {
            if (!userService.AdministratorExists())
            {
                return Unauthorized();
            }           
            
            var spaceShips = spaceShipService.AllSpaceShips();
            return View(spaceShips);
        }
    }
}
