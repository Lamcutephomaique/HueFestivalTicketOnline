using Microsoft.EntityFrameworkCore;
using HueFestivalTicketOnline.Dto;
using HueFestivalTicketOnline.Models;

namespace HueFestivalTicketOnline.Models
{
    public class HueFestivalTicketOnlineContext : DbContext
    {
        public HueFestivalTicketOnlineContext(DbContextOptions<HueFestivalTicketOnlineContext> options) : base(options) { }
        public DbSet<Role> Roles { get; set; }

        public DbSet<About> Abouts { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<Checkin> Checkins { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<News> Newss { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<ProgramType> ProgramTypes { get; set; }

        public DbSet<Service> Services { get; set; }
        public DbSet<SubMenu> SubMenus { get; set; }
        public DbSet<TicketLocation> TicketLocations { get; set; }
        public DbSet<FavouriteEvent> FavouriteEvents { get; set; }
        public DbSet<FavouriteService> FavouriteServices { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Checkin>()
                .HasKey(c => new { c.AdminId, c.TicketId });
            modelBuilder.Entity<FavouriteEvent>()
                .HasKey(c => new { c.UserId, c.EventId });
            modelBuilder.Entity<FavouriteService>()
               .HasKey(c => new { c.UserId, c.ServiceId });
            modelBuilder.Entity<Notification>()
               .HasKey(c => new { c.UserId, c.EventId });
            modelBuilder.Entity<Admin>()
                .HasOne(a => a.Role)
                .WithMany(r => r.Admins)
                .HasForeignKey(a => a.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Admins)
                .WithOne(a => a.Role)
                .HasForeignKey(a => a.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Event>()
                .HasOne(p => p.Admin)
                .WithMany(a => a.Events)
                .HasForeignKey(p => p.AdminId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Admin>()
               .HasMany(r => r.Events)
               .WithOne(a => a.Admin)
               .HasForeignKey(a => a.AdminId)
               .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Event>()
               .HasOne(p => p.Group)
               .WithMany(a => a.Events)
               .HasForeignKey(p => p.GroupId)
               .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Group>()
               .HasMany(r => r.Events)
               .WithOne(a => a.Group)
               .HasForeignKey(a => a.GroupId)
               .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Event>()
             .HasOne(p => p.Location)
             .WithMany(a => a.Events)
             .HasForeignKey(p => p.LocationId)
             .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Location>()
               .HasMany(r => r.Events)
               .WithOne(a => a.Location)
               .HasForeignKey(a => a.LocationId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
            .HasOne(p => p.TicketType)
            .WithMany(a => a.Tickets)
            .HasForeignKey(p => p.TicketTypeId)
            .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TicketType>()
               .HasMany(r => r.Tickets)
               .WithOne(a => a.TicketType)
               .HasForeignKey(a => a.TicketTypeId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(p => p.User)
                .WithMany(a => a.Tickets)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
                .HasMany(r => r.Tickets)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(p => p.Event)
                .WithMany(a => a.Tickets)
                .HasForeignKey(p => p.EventId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Event>()
                .HasMany(r => r.Tickets)
                .WithOne(a => a.Event)
                .HasForeignKey(a => a.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FavouriteEvent>()
                 .HasOne(p => p.User)
                 .WithMany(a => a.FavouriteEvents)
                 .HasForeignKey(p => p.UserId)
                 .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
                .HasMany(r => r.FavouriteEvents)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<FavouriteEvent>()
                 .HasOne(p => p.Event)
                 .WithMany(a => a.FavouriteEvents)
                 .HasForeignKey(p => p.EventId)
                 .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Event>()
                .HasMany(r => r.FavouriteEvents)
                .WithOne(a => a.Event)
                .HasForeignKey(a => a.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(r => r.FavouriteServices)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Service>()
                .HasMany(r => r.FavouriteServices)
                .WithOne(a => a.Service)
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Notification>()
                 .HasOne(p => p.User)
                 .WithMany(a => a.Notifications)
                 .HasForeignKey(p => p.UserId)
                 .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
                .HasMany(r => r.Notifications)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Notification>()
                .HasOne(p => p.Event)
                .WithMany(a => a.Notifications)
                .HasForeignKey(p => p.EventId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Event>()
                .HasMany(r => r.Notifications)
                .WithOne(a => a.Event)
                .HasForeignKey(a => a.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Checkin>()
                .HasOne(p => p.Ticket)
                .WithMany(a => a.Checkins)
                .HasForeignKey(p => p.TicketId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Ticket>()
                .HasMany(r => r.Checkins)
                .WithOne(a => a.Ticket)
                .HasForeignKey(a => a.TicketId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Checkin>()
               .HasOne(p => p.Admin)
               .WithMany(a => a.Checkins)
               .HasForeignKey(p => p.AdminId)
               .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Admin>()
                .HasMany(r => r.Checkins)
                .WithOne(a => a.Admin)
                .HasForeignKey(a => a.AdminId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Event>()
               .HasOne(a => a.ProgramType)
               .WithMany(r => r.Events)
               .HasForeignKey(a => a.Type_program)
               .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProgramType>()
                .HasMany(r => r.Events)
                .WithOne(a => a.ProgramType)
                .HasForeignKey(a => a.Type_program)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Admin>()
               .HasOne(a => a.Role)
               .WithMany(r => r.Admins)
               .HasForeignKey(a => a.RoleId)
               .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Admins)
                .WithOne(a => a.Role)
                .HasForeignKey(a => a.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
