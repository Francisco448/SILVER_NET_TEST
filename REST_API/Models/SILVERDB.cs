using System.Data.Entity;

namespace REST_API.Models
{
    public class SILVERDB : DbContext
    {
        /// <summary>
        /// constructor of SILVERDB Context.
        /// </summary>
        public SILVERDB() : base("name=SILVERDB")
        {
        }

        /// <summary>
        /// Entity of Users.
        /// </summary>
        public DbSet<Users> Users { get; set; }
        /// <summary>
        /// Entity of Products.
        /// </summary>
        public DbSet<Products> Products { get; set; }

        /// <summary>
        /// model creating of the entitys.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<Products>().ToTable("Products");
        }
    }
}