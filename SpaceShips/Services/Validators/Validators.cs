namespace SpaceShips.Services.Validators
{   
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;     
    using Data;
    using Models.Administrator;
    using Models.SpaceShips;

    using static Data.DataConstants.DefaultConstants;
    using static Data.DataConstants.UserConstants;
    using static Data.DataConstants.SpaceShipConstants;

    public class Validators : IValidator
    {
        private readonly SpaceShipsDbContext data;

        public Validators(SpaceShipsDbContext data)
        {
            this.data = data;
        }

        public ICollection<string> ValidateRegisterUser(AdministratorRegisterModel model)
        {
            var errors = new List<string>();
            if (data.Users.Any())
            {
                errors.Add($"User is register."); 
            }

            if (model.Username != "Big Sister" && model.Username.Length != DefaultMaxLength )
            {
                errors.Add($"Username '{model.Username}' is not valid. It must be between {DefaultMaxLength} characters long.");
            }            

            if (model.Password == null || model.Password.Length < UserMinPassword || model.Password.Length > DefaultMaxLength)
            {
                errors.Add($"The provided password is not valid. It must be between {UserMinPassword} and {DefaultMaxLength} characters long.");
            }

            if (model.Password.Any(x => x == ' '))
            {
                errors.Add("The provided password cannot contain whitespace.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add($"Password and its confirmation are different.");
            }

            if (this.data.Users.Any(u => u.Username == model.Username))
            {
                errors.Add($"User with '{model.Username}' username already exists.");
            }

            return errors;
        }

        public ICollection<string> ValidateShipRegister(SpaceShipRegisterModel model)
        {
            var errors = new List<string>();

            if (!(model.Type == "Cargo" || model.Type == "Family"))
            {
                errors.Add($"Model '{model.Type}' is not valid. It must be between {DefaultMaxLength} characters long.");
            }

            if (model.YearOfTaxCalculation < YearOfPurchase)
            {
                errors.Add($"Year '{model.YearOfTaxCalculation}' is not valid. It must be between {YearOfPurchase}.");
            }
           
            if (model.YearOfPurchase < YearOfPurchase)
            {
                errors.Add($"Year '{model.YearOfPurchase}' is not valid. It must be between {YearOfPurchase}.");
            }

            if (model.YearOfTaxCalculation < model.YearOfPurchase)
            {
                errors.Add($"Year '{model.YearOfTaxCalculation}' is not valid. It must be between {model.YearOfPurchase}.");
            }
           
            if (model.LightMilesTraveled <= MinYearOfTaxCalculation)
            {
                errors.Add($"Miles Traveled {model.LightMilesTraveled} is not a valid.");
            }

            return errors;
        }

        public ICollection<string> ValidateShipUpdate(SpaceShipUpdateModel model, int id)
        {
            var errors = new List<string>();

            if (model.YearOfTaxCalculation < data.SpaceTransferFees.Where(x => x.SpaceShipId == id).OrderBy(x => x.Id).Select(x => x.YearOfTaxCalculation).LastOrDefault())
            {
                errors.Add($"Year '{model.YearOfTaxCalculation}'.");
            }

            if (model.LightMilesTraveled <= MinYearOfTaxCalculation)
            {
                errors.Add($"Miles Traveled {model.LightMilesTraveled} is not a valid.");
            }

            return errors;
        }
    }
}
