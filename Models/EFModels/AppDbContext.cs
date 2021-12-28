using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace NTUB.BookStore.Site.Models.EFModels
{
	public partial class AppDbContext : DbContext
	{
		public AppDbContext()
			: base("name=AppDbContext")
		{
		}

		public virtual DbSet<Member> Members { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Member>()
				.Property(e => e.Mobile)
				.IsFixedLength();

			modelBuilder.Entity<Member>()
				.Property(e => e.ConfirmCode)
				.IsUnicode(false);
		}
	}
}
