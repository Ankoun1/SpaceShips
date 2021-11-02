namespace SpaceShips.Services.SpaceShips
{
    using Data.Models;
    using Data;
    using Models.SpaceShips;
    using System.Linq;
    using System.Collections.Generic;    
    using Services.SpaceTransferFees;
    using static Data.DataConstants.SpaceShipTax;
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
                YearOfPurchase = model.YearOfPurchase,
                YearOfTaxCalculation = model.YearOfTaxCalculation,
                LightMilesTraveled = model.LightMilesTraveled,
                UserId = adminId
            };
            this.data.SpaceShips.Add(spaceShip);            
           
            this.data.SaveChanges();

            spaceTransferFeeService.AddTax(spaceShip.Id);
        }

        public void UpdateSpaceShip(SpaceShipUpdateModel model, int spaceShipId)
        {
            var spaceShip = data.SpaceShips.Find(spaceShipId);

            int milesTraveledTax = 0;
            if (spaceShip.Type == "Cargo")
            {               
                milesTraveledTax = milesTraveledTaxCargo;
            }
            else
            {                
                milesTraveledTax = milesTraveledTaxFamily;
            }

            int calculateLightMilesTraveled = 0;
            if(spaceShip.LightMilesTraveled <= milesTraveledTax)
            {
                calculateLightMilesTraveled = spaceShip.LightMilesTraveled + model.LightMilesTraveled;
            }
            else
            {
                calculateLightMilesTraveled = spaceShip.LightMilesTraveled % (spaceShip.LightMilesTraveled / milesTraveledTax * milesTraveledTax) + model.LightMilesTraveled;
            }

            spaceShip.YearOfPurchase = spaceShip.YearOfTaxCalculation;
            spaceShip.YearOfTaxCalculation = model.YearOfTaxCalculation;
            spaceShip.LightMilesTraveled = calculateLightMilesTraveled;
            this.data.SaveChanges();

            spaceTransferFeeService.AddTax(spaceShip.Id);
        }

        public List<SpaceShipsListingViewModel> AllSpaceShips()
        {
         
            var spaceShips = data.SpaceShips.Select(x => new SpaceShipsListingViewModel
            {
                Id = x.Id,
                Taxs = data.SpaceTransferFees.Where(y => y.SpaceShipId == x.Id).Select(x => x.Fee).ToList(),
                TotalTax = data.SpaceTransferFees.Where(y => y.SpaceShipId == x.Id).Select(x => x.Fee).Sum()
            }).ToList();
            
            return spaceShips;
        }      
             
    }
}
