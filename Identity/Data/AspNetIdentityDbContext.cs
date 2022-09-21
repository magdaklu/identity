using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Data
{
    public class AspNetIdentityDbContext : IdentityDbContext
    {
        public AspNetIdentityDbContext(DbContextOptions<AspNetIdentityDbContext> options) : base(options)
        { }
    }
}
