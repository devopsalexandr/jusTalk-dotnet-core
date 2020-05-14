using JusTalk.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace JusTalk.DAL
{
	internal class ContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
	{
		public ApplicationContext CreateDbContext(string[] args)
		{
			var builder = new DbContextOptionsBuilder<ApplicationContext>()
				.UseMySql("server=localhost;port=3306;database=justalk_v2;uid=root;password=password;");

			return new ApplicationContext(builder.Options);
		}
	}
}
