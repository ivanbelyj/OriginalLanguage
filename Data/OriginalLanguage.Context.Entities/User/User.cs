using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Context.Entities.User;
public class User : IdentityUser<Guid>
{
    public string FullName { get; set; }
    public UserStatus Status { get; set; }
}
