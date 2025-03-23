namespace API.DTO.Request_Response
{
    public class RegisterUserRequest
    {
        public string Usermame { get; set; } 
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
    }
}
