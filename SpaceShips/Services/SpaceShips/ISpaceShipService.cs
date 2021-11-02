namespace SpaceShips.Services.SpaceShips
{
    using System.Collections.Generic;
    using Models.SpaceShips;

    public interface ISpaceShipService
    {
        void CreateSpaceShip(SpaceShipRegisterModel model,string adminId);

        void UpdateSpaceShip(SpaceShipUpdateModel model,int spaceShipId);

        List<SpaceShipsListingViewModel> AllSpaceShips();
    }
}
