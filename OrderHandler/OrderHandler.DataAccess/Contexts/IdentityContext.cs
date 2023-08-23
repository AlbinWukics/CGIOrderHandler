using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OrderHandler.DomainCommons.DataModels;

namespace OrderHandler.DataAccess.Contexts;

public class IdentityContext : ApiAuthorizationDbContext<ApplicationUserModel>
{
    public IdentityContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }
}