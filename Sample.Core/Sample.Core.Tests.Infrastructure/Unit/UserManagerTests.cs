using AutoFixture;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NSubstitute;
using Sample.Core.Domain.Settings;

namespace Sample.Core.Infrastructure.Unit.Tests.Unit;

public class UserManagerTests
{
    private readonly UserManager<Sample.Core.Domain.Entities.ApplicationUser> _userManager;
    private readonly IFixture _fixture = new Fixture();

    public UserManagerTests()
    {
        _userManager = Substitute.For<UserManager<Sample.Core.Domain.Entities.ApplicationUser>>(
            Substitute.For<IUserStore<Sample.Core.Domain.Entities.ApplicationUser>>(),
            null, null, null, null, null, null, null, null);
        
        _fixture.Register(() =>
        {
            var applicationSettings = _fixture.Create<ApplicationSettings>(); // Create an instance of ApplicationSettings
            
            return Options.Create(applicationSettings); // Return the IOptions<ApplicationSettings> instance
        });
        
        var options = _fixture.Create<IOptions<ApplicationSettings>>();
    }
    
    // Implement User Manager Tests
}