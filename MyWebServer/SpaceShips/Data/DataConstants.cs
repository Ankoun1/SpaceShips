namespace SpaceShips.Data
{
    public class DataConstants
    {
        public class DefaultConstants
        {
            public const int IdMaxLength = 40;
            public const int DefaultMaxLength = 10;
        }

        public class UserConstants
        {
            public const int UserMaxUsername = 10;
            public const int UserMinPassword = 5;
        } 
        
        public class SpaceShipConstants
        {
            public const int TypeMaxLength = 6;
            public const int YearOfPurchase = 1100;
            public const int MinYearOfTaxCalculation = 0;
            public const int MinSpaceTransferTax = 0;                  
        }     
        public class SpaceShipTax
        {
            public const short cargoInitialFee = 10000;
            public const short familyInitialFee = 5000;
            public const short cargoIncreasedTax = 1000;
            public const short familyIncreasedTax = 100;
            public const short cargoReducedTax = 736;
            public const short familyReducedTax = 355;       
           
        }
    }                                   
 
}
