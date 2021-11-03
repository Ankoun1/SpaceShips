namespace SpaceShips.Services.SpaceShips
{
    using Data.Models;
    using Data;
    using Models.SpaceShips;
    using System.Linq;
    using System.Collections.Generic;    
    using Services.SpaceTransferFees;
    using static Data.DataConstants.SpaceShipTax;
    using System.Text;

    public class SpaceShipService : ISpaceShipService
    {
        private readonly SpaceShipsDbContext data;
        private readonly ISpaceTransferFeeService spaceTransferFeeService;

        public SpaceShipService(SpaceShipsDbContext data, ISpaceTransferFeeService spaceTransferFeeService)
        {
            this.data = data;
            this.spaceTransferFeeService = spaceTransferFeeService;
        }
        
        public void CreateSpaceShip(SpaceShipRegisterModel model,string adminId)
        {
            
            var spaceShip = new SpaceShip
            {
                Type = model.Type,
                YearStartSpace = model.YearOfPurchase,                
                UserId = adminId
            };
            this.data.SpaceShips.Add(spaceShip);            
           
            this.data.SaveChanges();

            spaceTransferFeeService.AddSpaceShipTax(spaceShip.Id, model.YearOfPurchase, model.YearOfTaxCalculation, model.LightMilesTraveled);
        }
       

        public List<SpaceShipsListingViewModel> AllSpaceShips()
        {
            StringBuilder  sb = new StringBuilder();

            var spaceShips = data.SpaceShips.Select(x => new SpaceShipsListingViewModel
            {
                Id = x.Id,
                YearStartSpace = x.YearStartSpace,
                ParamsTaxs = data.SpaceTransferFees.Where(y => y.SpaceShipId == x.Id).ToList(),                             
                TotalTax = data.SpaceTransferFees.Where(y => y.SpaceShipId == x.Id).Select(x => x.Fee).Sum()
            }).ToList();
            
            return spaceShips;
        }      
             
    }
}
