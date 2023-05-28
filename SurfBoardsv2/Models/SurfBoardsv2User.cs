using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Globalization;



namespace SurfBoardsv2.Models;

// Add profile data for application users by adding properties to the SurfBoardsv2User class
public class SurfBoardsv2User : IdentityUser
{
    [PersonalData]
    public string? FirstName { get; set; }
    [PersonalData]
    public string? LastName { get; set; }
    [PersonalData]
    public DateTime DOB { get; set; }

    public ICollection<Rent>? Rents { get; set; }

    public string? GetFullName()
    {
        return this.FirstName + " " + this.LastName;
    }
}
public class SurfBoardsv2Role : IdentityRole
{


}
