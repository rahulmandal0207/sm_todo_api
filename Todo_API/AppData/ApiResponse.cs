namespace Todo_API.AppData
{
    public class ApiResponse<T>
    {
        public string? Message { get; set; }

        public T? Data { get; set; }
    }
}
