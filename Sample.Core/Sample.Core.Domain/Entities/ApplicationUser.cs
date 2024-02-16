using Microsoft.AspNetCore.Identity;

namespace Sample.Core.Domain.Entities;

public sealed class ApplicationUser : IdentityUser
{
    public ICollection<Image> Images { get; set; } = new List<Image>();
}