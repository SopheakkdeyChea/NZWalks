namespace NZWalksAPI.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Userame { get; set; }
        public string EmailAdress { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
