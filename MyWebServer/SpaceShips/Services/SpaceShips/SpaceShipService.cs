namespace SpaceShips.Services.SpaceShips
{
    using Data.Models;
    using Data;
    using Models.SpaceShips;
    using System.Linq;
    using System.Collections.Generic;

    using static Data.DataConstants.SpaceShipTax;

    public class SpaceShipService : ISpaceShipService
    {
        private readonly SpaceShipsDbContext data;

        public SpaceShipService(SpaceShipsDbContext data)
        {
            this.data = data;
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

            Tax(spaceShip.Id);
        }

        public void UpdateSpaceShip(SpaceShipUpdateModel model, int spaceShipId)
        {
            var spaceShip = data.SpaceShips.Find(spaceShipId);
            spaceShip.YearOfPurchase = spaceShip.YearOfTaxCalculation;
            spaceShip.YearOfTaxCalculation = model.YearOfTaxCalculation;
            spaceShip.LightMilesTraveled = model.LightMilesTraveled;
            this.data.SaveChanges();

            Tax(spaceShip.Id);
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

        private void Tax(int id)
        {
            var spaceShip = data.SpaceShips.Where(x => x.Id == id).FirstOrDefault();                            
                      
            short initialFee = 0;
            short increasedTax = 0;
            short reducedTax = 0;
            if (spaceShip.Type == "Cargo")
            {
                initialFee = cargoInitialFee;
                increasedTax = cargoIncreasedTax;
                reducedTax = cargoReducedTax;
            }
            else
            {
                initialFee = familyInitialFee;
                increasedTax = familyIncreasedTax;
                reducedTax = familyReducedTax;
            }
            var tax = (spaceShip.LightMilesTraveled / 1000) * increasedTax - (spaceShip.YearOfTaxCalculation - spaceShip.YearOfPurchase) * reducedTax;
            if (!data.SpaceTransferFees.Any(x => x.SpaceShipId == id))
            {
                tax += initialFee;
            }

            if(tax < 0)
            {
                tax = 0;
            }
            var currentTax = new SpaceTransferFee
            {
                Fee = tax,
                SpaceShipId = id
            };

            this.data.SpaceTransferFees.Add(currentTax);
            this.data.SaveChanges();
        }
       
    }
}
