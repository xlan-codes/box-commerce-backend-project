namespace Application.Generics.Dtos.Auth.Cisl
{
    public class SendDocumentsDto
    {
        public int EmailId { get; set; }
        public bool IsEmailSent { get; set; }
        public bool IsEmailSentAsync { get; set; }
        public string Message { get; set; }
        public bool ErrorHasOccured { get; set; }
    }
}
