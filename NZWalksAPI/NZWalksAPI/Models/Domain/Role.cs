namespace NZWalksAPI.Models.Domain
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // Navigation property
        public List<Users_Roles> UserRoles { get; set; }
    }
}
