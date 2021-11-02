namespace SpaceShips.Models.SpaceShips
{
    using System.Collections.Generic;
    public class SpaceShipsListingViewModel
    {
        public int Id { get; init; }

        public List<decimal> Taxs {get; init;}
        
        public decimal TotalTax {get; init;} 
    }
}
