namespace Modsenfy.DataAccessLayer.Entities;

public class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; }
    
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<User> Users { get; set; }
}