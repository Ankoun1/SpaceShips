namespace SpaceShips.Models.SpaceShips
{
    using System.Collections.Generic;
    using Data.Models;

    public class SpaceShipsListingViewModel
    {
        public int Id { get; init; }

        public int YearStartSpace { get; init; }

        public List<SpaceTransferFee> ParamsTaxs { get; init;}
        
        public decimal TotalTax {get; init;} 
    }
}
