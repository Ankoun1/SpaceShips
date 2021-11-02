namespace SpaceShips.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.DefaultConstants;
    using static DataConstants.UserConstants;

    public class User
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(UserMaxUsername)]
        public string Username { get; init; }


        [Required]
        public string Password { get; set; }

        public IEnumerable<SpaceShip> SpeceShips { get; init; } = new List<SpaceShip>();
    }
}
