

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using okLims.Models;

namespace okLims.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
         
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<NumberSequence> NumberSequence { get; set; }
        public DbSet<Request> Request { get; set; }
        public DbSet<Laboratory> Laboratory { get; set; }
        public DbSet<FilterType> FilterType { get; set; }
        public DbSet<ControllerType> ControllerType { get; set; }
        public DbSet<FilterSize> FilterSize { get; set; }
     public DbSet<InstrumentLine> InstrumentLine { get; set; }
        public DbSet<RequestLine> RequestLine { get; set; }
        public DbSet<Instrument> Instrument { get; set; }
      public DbSet<RequestState> RequestState { get; set; }
        
    }
}
