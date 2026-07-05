using ContractDesigner.Core.Models;

namespace ContractDesigner.Core.Abstractions
{
    public interface IRentalApartamentAgreement
    {
        byte[] Generate(RentalApartamentAgreementOptions options);
    }
}