using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class ProjectConfigurations : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(project => project.Id);

            builder.HasOne(project => project.Freelancer)
                   .WithMany(user => user.FreelanceProjects)
                   .HasForeignKey(project => project.IdFreelancer)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(project => project.Client)
                   .WithMany(user => user.OwnedProjects)
                   .HasForeignKey(project => project.IdClient)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
