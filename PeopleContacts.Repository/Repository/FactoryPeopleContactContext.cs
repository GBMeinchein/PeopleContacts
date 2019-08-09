using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PeopleContacts.Repository.Repository
{
    class FactoryPeopleContactContext : IDesignTimeDbContextFactory<PeopleContactsContext>        
    {
        public PeopleContactsContext CreateDbContext(string[] args)
        {
            var connectionString = "Data Source=(localdb)\\MsSqlLocalDb;initial catalog=PeopleContactsDB;Integrated Security=True; MultipleActiveResultSets=True";

            var construtor = new DbContextOptionsBuilder<PeopleContactsContext>();
            construtor.UseSqlServer(connectionString);

            return new PeopleContactsContext(construtor.Options);
        }
    }
}
