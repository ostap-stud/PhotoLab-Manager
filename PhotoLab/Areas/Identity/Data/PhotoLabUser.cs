using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PhotoLab.Areas.Identity.Data;

// Add profile data for application users by adding properties to the PhotoLabUser class
public class PhotoLabUser : IdentityUser
{
    [PersonalData]
    [MinLength(2)]
    [MaxLength(40, ErrorMessage = "Name should be < 40 characters")]
    public string? Name { get; set; }

    [PersonalData]
    [MinLength(2)]
    [MaxLength(40, ErrorMessage = "Surame should be < 40 characters")]
    public string? Surname { get; set; }
}

