namespace SpaceShips.Services.SpaceTransferFees
{
    using System.Linq;
    using Data;
    using Data.Models;
    using static Data.DataConstants.SpaceShipTax;

    public class SpaceTransferFeeService : ISpaceTransferFeeService
    {
        private readonly SpaceShipsDbContext data;

        public SpaceTransferFeeService(SpaceShipsDbContext data)
        {
            this.data = data;
        }

        public void AddTax(int id)
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

            if (tax < 0)
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
