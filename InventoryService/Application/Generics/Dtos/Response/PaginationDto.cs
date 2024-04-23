namespace Application.Generics.Dtos.ResponseDtos
{
    public class PaginationDto<T>
    {
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public T Data { get; set; }
    }
}
