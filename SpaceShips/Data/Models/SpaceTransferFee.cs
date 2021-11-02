﻿namespace SpaceShips.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SpaceTransferFee
    {
        [Key]
        public int Id { get; init; }

        public decimal Fee { get; init; }
        
        public int SpaceShipId { get; init; }

        public SpaceShip SpaceShip { get; init; }
    }
}
