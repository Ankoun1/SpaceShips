namespace SpaceShips.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.SpaceShipConstants;

    public class SpaceShip
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(TypeMaxLength)]
        public string Type { get; init; }
       
        public int YearOfPurchase { get; set; }        
        
        public int YearOfTaxCalculation { get; set; }
      
        public int LightMilesTraveled { get; set; }     

        [Required]
        public string UserId { get; init; }

        public User User { get; init; }

        public IEnumerable<SpaceTransferFee> SpaceTransferFees { get; init; } = new List<SpaceTransferFee>();
    }
}
