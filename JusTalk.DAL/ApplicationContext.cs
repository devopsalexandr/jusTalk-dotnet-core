using System.Threading;
using System.Threading.Tasks;
using JusTalk.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace JusTalk.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { set; get; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // relations here
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            BeforeSaveChanges();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            BeforeSaveChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            BeforeSaveChanges();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected virtual void BeforeSaveChanges()
        {
            ChangeTracker.TrackTimestampBeforeSaveChanges();
        }
    }
}