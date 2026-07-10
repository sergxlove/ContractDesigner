namespace ContractDesigner.Core.Models
{
    public class RentalApartamentAgreementOptions
    {
        public string FullNameLandlord { get; set; } = "text text text";
        public string PassportDataLandlord { get; set; } = "series number";
        public string AddressRegLandlord { get; set; } = "text";
        public string IssuedLandlord { get; set; } = "text";
        public string FullNameTenant { get; set; } = "text text text";
        public string PassportDataTenant { get; set; } = "series number";
        public string AddressRegTenant { get; set; } = "text";
        public string IssuedTenant { get; set; } = "text";
        public string ApartamentAddress { get; set; } = "text text";
        public string ApartamentSquare { get; set; } = "text";
        public string OwnershipDocument { get; set; } = "text";
        public List<People> PeoplesLives { get; set; } = []; 
        public int QuantityChildren { get; set; } = 0;
        public string TypeAnimal { get; set; } = string.Empty;
        public bool IsCanAnimals { get; set; } = false;
        public int QuantityMonthAgreement { get; set; } = 0;
        public DateOnly DateStartAgreement { get; set; }
        public int QuantityMonthBeforeOffer { get; set; } = 0;
        public int QuantityDayWarningCancel { get; set; } = 0; 
        public int Compensation { get; set; } = 0;
        public int PaymentMonth { get; set; } = 0;
        public bool IsHaveCom { get; set; } = false;
        public List<string> ServiceCom { get; set; } = [];
        public int PaymentZl { get; set; } = 0;
        public int CountExample { get; set; } = 0;
    }
}
