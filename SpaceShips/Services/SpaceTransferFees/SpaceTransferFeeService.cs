namespace SpaceShips.Services.SpaceTransferFees
{
    using System.Linq;
    using Data;
    using Data.Models;
    using Models.SpaceShips;
    using static Data.DataConstants.SpaceShipTax;

    public class SpaceTransferFeeService : ISpaceTransferFeeService
    {
        private readonly SpaceShipsDbContext data;

        public SpaceTransferFeeService(SpaceShipsDbContext data)
        {
            this.data = data;
        }

        public void AddSpaceShipTax(int id, int yearOfPurchase, int yearOfTaxCalculation, int lightMilesTraveled)
        {
            var spaceShip = data.SpaceShips.Where(x => x.Id == id).FirstOrDefault();

            short initialFee = 0;
            short increasedTax = 0;
            short reducedTax = 0;
            int milesTraveledTax = 0;
            if (spaceShip.Type == "Cargo")
            {
                initialFee = cargoInitialFee;
                increasedTax = cargoIncreasedTax;
                reducedTax = cargoReducedTax;
                milesTraveledTax = milesTraveledTaxCargo;
            }
            else
            {
                initialFee = familyInitialFee;
                increasedTax = familyIncreasedTax;
                reducedTax = familyReducedTax;
                milesTraveledTax = milesTraveledTaxFamily;
            }
            
            var tax = (lightMilesTraveled / milesTraveledTax) * increasedTax - (yearOfTaxCalculation - yearOfPurchase) * reducedTax;
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
                YearOfPurchase = yearOfPurchase,
                YearOfTaxCalculation = yearOfTaxCalculation,
                LightMilesTraveled = lightMilesTraveled,
                Fee = tax,
                SpaceShipId = id
            };

            this.data.SpaceTransferFees.Add(currentTax);
            this.data.SaveChanges();
        }


        public void UpdateSpaceShipParamsTaxs(SpaceShipUpdateModel model, int spaceShipId)
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
            var lastSpaceTransferFee = data.SpaceTransferFees.Where(x => x.SpaceShipId == spaceShipId).OrderBy(x => x.Id).LastOrDefault();
            int calculateLightMilesTraveled = 0;
            if (lastSpaceTransferFee.LightMilesTraveled < milesTraveledTax)
            {
                calculateLightMilesTraveled = lastSpaceTransferFee.LightMilesTraveled + model.LightMilesTraveled;
            }
            else
            {
                calculateLightMilesTraveled = lastSpaceTransferFee.LightMilesTraveled % (lastSpaceTransferFee.LightMilesTraveled / milesTraveledTax * milesTraveledTax) + model.LightMilesTraveled;
            }
            AddSpaceShipTax(spaceShipId, lastSpaceTransferFee.YearOfTaxCalculation, model.YearOfTaxCalculation, calculateLightMilesTraveled);
           
        }
    }
}
