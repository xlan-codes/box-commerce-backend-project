
using System.Runtime.Serialization;

namespace Application.UseCases.PreOfferModule.Enums
{
    /// <summary>
    /// Third party role enum.
    /// </summary>
    public enum ThirdPartyRoleEnum
    {
        /// <summary>
        /// Insured.
        /// </summary>
        /// <remarks>Insured</remarks>
        [EnumMember(Value = "Insured")]
        Insured = 1,
        /// <summary>
        /// Spouse.
        /// </summary>
        /// <remarks>Beneficiary</remarks>
        [EnumMember(Value = "Beneficiary")]
        Beneficiary = 2,

    }
}
