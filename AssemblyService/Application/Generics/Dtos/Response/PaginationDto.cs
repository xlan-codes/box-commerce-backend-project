namespace Application.Generics.Dtos.Response
{
    public class PaginationDto<T>
    {
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public T Data { get; set; }
    }
}
