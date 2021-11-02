namespace SpaceShips.Services.Validators
{
    using System.Collections.Generic;
    using Models.SpaceShips;
    using Models.Administrator;

    public interface IValidator
    {
        ICollection<string> ValidateRegisterUser(AdministratorRegisterModel model);        

        ICollection<string> ValidateShipRegister(SpaceShipRegisterModel model);
        
        ICollection<string> ValidateShipUpdate(SpaceShipUpdateModel model,int id);        
    }
}
