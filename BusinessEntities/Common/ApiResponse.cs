namespace BusinessEntities.Common
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string Errors { get; set; }

        public static ApiResponse<T> Fail(string message)
        {
            return new ApiResponse<T> { IsSuccess = false, Errors = message };
        }
        public static ApiResponse<T> Success(T data)
        {
            return new ApiResponse<T> { IsSuccess = true, Data = data };
        }
    }
}
