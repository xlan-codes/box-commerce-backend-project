namespace Application.UseCases.CMSModule.Dtos
{
    public class SingleAgentFlowsDto
    {
        public string OfferCode { get; set; }
        public DateTime? OfferDate { get; set; }
        public string Paidcode { get; set; }
        public string CNP { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal? Premium { get; set; }
        public string Status { get; set; }
        public bool? IsError { get; set; } = null;
        public string Error { get; set; } = null;
        public DateTime Timestamp { get; set; }
        public decimal? InsuredValue { get; set; }
        public decimal? PremiumCommercial { get; set; }
        public string OfferAgentName { get; set; }
        public string PolicyAgentName { get; set; }
        public string CISLOfferRequest { get; set; } = null;
        public string CISLOfferResponse { get; set; } = null;
    }


}
