namespace Sample.Core.Domain.Settings;

public sealed class AuthenticationSettings
{
    public string? Issuer { get; set; }
    
    public string[]? Audiences { get; set; }
    
    public string? IssuerSigningKey { get; set; }
}