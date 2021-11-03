namespace SpaceShips.Services.SpaceTransferFees
{
    using Models.SpaceShips;   

    public interface ISpaceTransferFeeService
    {
        void AddSpaceShipTax(int id, int yearOfPurchase,int yearOfTaxCalculation,int lightMilesTraveled);

        void UpdateSpaceShipParamsTaxs(SpaceShipUpdateModel model, int spaceShipId);
    }
}
