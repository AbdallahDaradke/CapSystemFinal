using System.Security.Claims;
using CapSystemFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore;

namespace CapSystemFinal.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<CapSystemFinal.Models.Complaint> Complaints { get; set; } = default!;

        public DbSet<Student> students { get; set; }

        public DbSet<CapSystemFinal.Models.ComplaintType> ComplaintType { get; set; } = default!;

        public DbSet<CapSystemFinal.Models.ComplaintStatus> ComplaintStatus { get; set; } = default!;

        public DbSet<CapSystemFinal.Models.TransformationDirection> TransformationDirection { get; set; } = default!;

        public DbSet<CapSystemFinal.Models.UserType> UserType { get; set; } = default!;


        public DbSet<Dean> Deans { get; set; } = default!;

        //public DbSet<Dean> Deans { get; set; } = default!;








        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            ////Complaints Relationships

            //builder.Entity<Complaint>()
            //    .HasOne(c => c.Students)
            //    .WithMany(s => s.complaint)
            //    .HasForeignKey(c => c.StudentId);

            //builder.Entity<Complaint>()
            //   .HasOne(c => c.complaintStatus)
            //   .WithMany(cs => cs.complaint)
            //   .HasForeignKey(c => c.ComplaintStatusId);

            //builder.Entity<Complaint>()
            //    .HasOne(c => c.complaintType)
            //    .WithMany(ct => ct.Complaint)
            //    .HasForeignKey(c => c.CompTypeId);

            //builder.Entity<Complaint>()
            //    .HasOne(c => c.transformationDirection)
            //    .WithMany(td => td.Complaint)
            //    .HasForeignKey(c => c.TransformationDirectionId);


            ////Student Relationships

            //builder.Entity<Student>()
            //    .HasOne(s => s.user)
            //    .WithOne(u => u.student)
            //    .HasForeignKey<Student>(s => s.userId);

            ////User Relationships

            //builder.Entity<User>()
            //    .HasOne(u => u.userType)
            //    .WithMany(ut => ut.User)
            //    .HasForeignKey(u => u.userTypeId);




            builder.Entity<ComplaintType>(b =>
            {
                b.HasData(
                    new ComplaintType { Id = 1, Description = "Academic" },
                    new ComplaintType { Id = 2, Description = "Behavior" }
                    );
            });

            builder.Entity<ComplaintStatus>(b =>
            {
                b.HasData(
                        new ComplaintStatus { Id = 1, status = "Closed" },
                        new ComplaintStatus { Id = 2, status = "Pending" },
                        new ComplaintStatus { Id = 3, status = "In Progress" },
                        new ComplaintStatus { Id = 4, status = "Resolved" }
                    );
            });

            builder.Entity<TransformationDirection>(b =>
            {
                b.HasData(
                    new TransformationDirection { Id = 1, Description = "Samir Tartir" },
                    new TransformationDirection { Id = 2, Description = "Orwa Aladaileh" }
                    );
            });

            builder.Entity<UserType>(b =>
            {
                b.HasData(
                    new UserType { Id = 1, Description = "Admin" },
                    new UserType { Id = 2, Description = "Student" }
                    );
            });


         







        }

        }


    }

