namespace SpaceShips
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using MyWebServer;
    using MyWebServer.Controllers;
    using MyWebServer.Results.Views;
    using Data;
    using Services.SpaceShips;
    using Services.Users;
    using Services.Validators;
    using Services.SpaceTransferFees;

    public class StartUp
    {
        public static async Task Main()
              => await HttpServer
                  .WithRoutes(routes => routes
                      .MapStaticFiles()
                      .MapControllers())
                  .WithServices(services => services
                      .Add<IViewEngine, CompilationViewEngine>()
                      .Add<IValidator, Validators>()
                      .Add<IUserService, UserService>()
                      .Add<ISpaceShipService, SpaceShipService>()
                      .Add<ISpaceTransferFeeService, SpaceTransferFeeService>()
                      .Add<SpaceShipsDbContext>()                  
                  )
                  .WithConfiguration<SpaceShipsDbContext>(context => context
                     .Database.Migrate())
                  .Start();
        //Add MyWebServer project add Reference
        //localhost:5000
        //Username = Big Sister

    }
}
