namespace Modsenfy.BusinessAccessLayer.DTOs.RequestDtos;

public class RequestDto
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Bio { get; set; }
    
    public string Time { get; set; }
    
    public ImageDto Image { get; set; }
    
    public UserWithIdAndDetailsAndEmailDto User { get; set; }
}