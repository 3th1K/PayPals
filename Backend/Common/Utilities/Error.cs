namespace Common.Utilities
{
    public class Error
    {
        public string ErrorCode { get; set; } = string.Empty;
        public int Code { get; set; } = 0;  
        public string ErrorMessage { get; set; } = string.Empty;
        public string ErrorDescription { get; set; } = string.Empty;
        public string ErrorSolution { get; set; } = string.Empty;
        public string ErrorDetails { get; set; } = string.Empty;
        public List<string>? Errors { get; set; } = new();

        public Error? InnerErrors { get; set; } = null;
    }
}
