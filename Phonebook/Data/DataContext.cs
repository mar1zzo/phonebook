using Microsoft.EntityFrameworkCore;
using Phonebook.Models;

namespace Phonebook.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<PersonPhone> PersonPhones { get; set; }
        public DbSet<PhoneNumberType> PhoneNumberTypes { get; set; }
    }
}