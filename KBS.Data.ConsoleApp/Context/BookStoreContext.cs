using System;
using KBS.Data.ConsoleApp.Model;
using Microsoft.EntityFrameworkCore;
using KBS.Data.ConsoleApp.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace KBS.Data.ConsoleApp.Context {
    public class BookStoreContext : DbContext {
        private static string DefaultConnection = @"Host=127.0.0.1;Database=kbs;Username=postgres;Password=postgres";

        public static readonly LoggerFactory ConsoleLoggerFactory
            = new LoggerFactory (new[] { new DebugLoggerProvider ( ) });

        private string _connectionString;

        #region DbSet

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookSold> BooksSold { get; set; }


        #endregion

        public BookStoreContext() {

        }

        public BookStoreContext(string connectionString) {
            _connectionString = connectionString;
        }

        public string GetEnv(string name) {
            return Environment.GetEnvironmentVariable (name) ?? string.Empty;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {

                optionsBuilder.UseLoggerFactory (ConsoleLoggerFactory);

                if (string.IsNullOrEmpty (_connectionString)) {

                    _connectionString = GetEnv ("ConnectionStrings__Main");

                    if (string.IsNullOrEmpty (_connectionString)) {
#if DEBUG
                        _connectionString = DefaultConnection;
#else
                    throw new ArgumentException("Environment variable \"ConnectionStrings__Main\" not set");
#endif
                    }


                }

                optionsBuilder.UseNpgsql (_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            //            var converter = new ValueConverter<string, Guid>(
            //                v => new Guid(v),
            //                v => v.ToString());
            //            
            //            var converterGuidToString = new ValueConverter<Guid, string>(
            //                v => v.ToString(),
            //                v => new Guid(v));


            modelBuilder.Entity<User> (entity => {
                entity.Property (e => e.Id).IsRequired ( );

                entity.Property (e => e.Password).IsRequired ( ).HasMaxLength (100);
                entity.Property (e => e.Role).IsRequired ( );
                entity.Property (e => e.Username).HasMaxLength (500);
                entity.Property (e => e.IsDeleted).HasDefaultValue (0).IsRequired ( );

            });

            modelBuilder.Entity<UserProfile> (entity => {
                entity.Property (e => e.Id).IsRequired ( );

                entity.Property (e => e.Address).HasMaxLength (500);
                entity.Property (e => e.Email).HasMaxLength (200);
                entity.Property (e => e.UserId);
                entity.Property (e => e.Phone).HasMaxLength (32);

            });

            modelBuilder.Entity<Book> (entity => {
                entity.Property (e => e.Id).IsRequired ( );

                entity.Property (e => e.Author).HasMaxLength (500);
                entity.Property (e => e.Price);
                entity.Property (e => e.Title).HasMaxLength (1024);

            });

            modelBuilder.Entity<BookSold> (entity => {
                entity.Property (e => e.Id).IsRequired ( );

                entity.Property (e => e.Author).HasMaxLength (500);
                entity.Property (e => e.Price);
                entity.Property (e => e.Title).HasMaxLength (1024);
                entity.Property (e => e.UserId).IsRequired ( );
                entity.Property (e => e.SoldDate).IsRequired ( );

            });


            var mapper = new Npgsql.NameTranslation.NpgsqlSnakeCaseNameTranslator ( );
            foreach (var entity in modelBuilder.Model.GetEntityTypes ( )) {
                // Replace table names
                entity.Relational ( ).TableName = mapper.TranslateMemberName (entity.Relational ( ).TableName);

                // Replace column names            
                foreach (var property in entity.GetProperties ( )) {
                    property.Relational ( ).ColumnName = mapper.TranslateMemberName (property.Relational ( ).ColumnName);
                }

                foreach (var key in entity.GetKeys ( )) {
                    key.Relational ( ).Name = mapper.TranslateMemberName (key.Relational ( ).Name);
                }

                foreach (var key in entity.GetForeignKeys ( )) {
                    key.Relational ( ).Name = mapper.TranslateMemberName (key.Relational ( ).Name);
                }

                foreach (var index in entity.GetIndexes ( )) {
                    index.Relational ( ).Name = mapper.TranslateMemberName (index.Relational ( ).Name);
                }
            }
        }

    }
}
