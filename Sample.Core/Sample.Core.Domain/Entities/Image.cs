namespace Sample.Core.Domain.Entities;

public sealed class Image
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string UserId { get; set; } = string.Empty;
    
    public ApplicationUser? Owner { get; set; }
 }