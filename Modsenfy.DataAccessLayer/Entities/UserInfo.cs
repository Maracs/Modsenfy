using System;

namespace Modsenfy.DataAccessLayer.Entities;

public class UserInfo
{
    public int UserInfoId { get; set; }

    public string UserInfoFirstName { get; set; }

    public string UserInfoLastName { get; set; }

    public string UserInfoMiddleName { get; set; }

    public string UserInfoPhone { get; set; }

    public string UserInfoAddress { get; set; }

    public DateTime UserInfoRegistrationDate { get; set; }

    public int ImageId { get; set; }

    public Image Image { get; set; }
}