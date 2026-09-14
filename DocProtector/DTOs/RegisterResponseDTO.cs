namespace DocProtector.DTOs
{
    public class RegisterResponseDTO
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();
    }
}
