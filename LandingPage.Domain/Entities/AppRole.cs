using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LandingPage.Domain.Entities
{
    [Table("AppRole")]
    public class AppRole : IdentityRole<Guid>
    {
    }
}
