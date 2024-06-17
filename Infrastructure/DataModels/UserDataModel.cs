using System.ComponentModel.DataAnnotations.Schema;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.DataModels;

[Table("User")]
public class UserDataModel : IdentityUser<int>, IModel
{
}
