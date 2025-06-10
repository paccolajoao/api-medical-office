namespace ApiMedicalOffice.Application.Dtos
{
    public class PaginatedResponse<T>
    {
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
    }
}
