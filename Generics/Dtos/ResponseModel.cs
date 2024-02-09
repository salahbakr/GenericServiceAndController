namespace Generics.Dtos
{
    public class ResponseModel<T> where T : class
    {
        public string? Message { get; set; }
        public string? Errors { get; set; }
        public bool Success => Errors == null;
        public T? Data { get; set; }
    }
}
