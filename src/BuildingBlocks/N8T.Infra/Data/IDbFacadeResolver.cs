using Microsoft.EntityFrameworkCore.Infrastructure;

namespace N8T.Infrastructure.Data
{
    public interface IDbFacadeResolver
    {
        DatabaseFacade Database { get; }
    }
}