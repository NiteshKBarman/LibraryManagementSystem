using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.ViewModel;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Models
{
    public class EntityFramework : DbContext
    {
        public EntityFramework(DbContextOptions<EntityFramework> options) : base(options)
        {

        }
        public DbSet<LibraryManagementSystem.ViewModel.SignUp>? SignUp { get; set; }
        public DbSet<LibraryManagementSystem.ViewModel.Book>? Book { get; set; }
        public DbSet<LibraryManagementSystem.ViewModel.Student>? Student { get; set; }
        public DbSet<LibraryManagementSystem.Models.StudentBookEnrollment>? StudentBookEnrollment { get; set; }
    }
}
