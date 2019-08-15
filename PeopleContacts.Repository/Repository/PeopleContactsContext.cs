using Microsoft.EntityFrameworkCore;
using PeopleContacts.Domain;
using PeopleContacts.Domain.Entitys;

namespace PeopleContacts.Repository
{
    public class PeopleContactsContext : DbContext
    {
        public PeopleContactsContext() { }

        public PeopleContactsContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("People");
            modelBuilder.Entity<Contact>().ToTable("Contacts");
        }
    }
}
