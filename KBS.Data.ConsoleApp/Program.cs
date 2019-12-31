using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KBS.Data.ConsoleApp.Context;
using KBS.Data.ConsoleApp.Model;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.EntityFrameworkCore;

namespace KBS.Data.ConsoleApp {
    public class Program {
        static int Main(string[] args) {
            var app = new CommandLineApplication ( );

            app.HelpOption ( );
            var optionCommand = app.Option ("-c|--command <Command>", "The command", CommandOptionType.SingleValue);

            app.OnExecute (async () => {
                var command = optionCommand.HasValue ( )
                    ? optionCommand.Value ( )
                    : "n/a";

                if (command.Trim ( ).Equals ("update") || command.Trim ( ).Equals ("both")) {
                    var store = new BookStoreContext ( );
                    Console.WriteLine ("Migrate database - begin");

                    await store.Database.MigrateAsync ( );

                    Console.WriteLine ("Migrate database - completed");

                    if (!command.Trim ( ).Equals ("both")) {
                        return 0;
                    }
                }

                if (command.Trim ( ).Equals ("seed") || command.Trim ( ).Equals ("both")) {
                    Console.WriteLine ("Seed database - begin");

                    await SeedAsync ( );

                    Console.WriteLine ("Seed database - completed");

                    return 0;

                }

                if (command.Trim ( ).Equals ("drop")) {
                    await DropAsync ( );

                    Console.WriteLine ("Drop database - completed");

                    return 0;
                }

                Console.WriteLine ("No command executed!!!");

                return 0;
            });

            return app.Execute (args);
        }

        private static async Task<int> SeedAsync() {
            var adminId = Guid.Parse ("1b7a9dac-c0bf-4342-bf1b-240be892842e");
            var johnId = Guid.Parse ("bf7393ed-49ee-4a96-8701-03f4916d3820");
            var aliceId = Guid.Parse ("14bdb12b-35f1-4c3f-88ac-5cb1ef39b9eb");
            var magdaId = Guid.Parse ("a5cee148-eccd-4121-9308-bdb618537e30");

            var store = new BookStoreContext ( );


            var usersCount = await store.Users.Select (x => x).CountAsync ( );
            if (usersCount > 0) {
                Console.WriteLine ("DB Seeded");
                return 0;
            }

            store.Users.Add (new User {
                Id = adminId,
                Username = "admin",
                Password = Encrypt ("admin"),
                Role = UserRoleEnum.Owner
            });

            store.Users.Add (new User {
                Id = johnId,
                Username = "john",
                Password = Encrypt ("john"),
                Role = UserRoleEnum.Salesman
            });

            store.Users.Add (new User {
                Id = aliceId,
                Username = "alice",
                Password = Encrypt ("alice"),
                Role = UserRoleEnum.Salesman
            });

            store.Users.Add (new User {
                Id = magdaId,
                Username = "magda",
                Password = Encrypt ("magda"),
                Role = UserRoleEnum.Salesman
            });


            int i = 0;
            int MAX_SALESMAN = 50;

            while (i++ < MAX_SALESMAN) {
                string username = Faker.Internet.UserName ( );
                store.Users.Add (new User {
                    Role = UserRoleEnum.Salesman,
                    Username = username,
                    Password = Encrypt (username),
                    Id = Guid.NewGuid ( )
                });
            }

            int MAX_BOOKS = 1000;
            i = 0;


            var books = new List<Book> ( );

            while (i++ < MAX_BOOKS) {
                var book = new Book ( ) {
                    Id = Guid.NewGuid ( ),
                    Author = $"{Faker.Name.First ( )} ${Faker.Name.FullName ( )}",
                    Title = String.Join (" ", Faker.Lorem.Words (4)),
                    Price = Faker.RandomNumber.Next (5, 25) * 1.25M
                };

                if (i < 10) {
                    books.Add (book);
                }

                store.Books.Add (book);
            }

            var j = 0;
            foreach (var book in books) {
                Guid guid = johnId;
                j++;
                if (j % 5 == 0) {
                    guid = aliceId;
                }

                if (j % 25 == 0) {
                    guid = magdaId;

                }
                store.BooksSold.Add (new BookSold ( ) {
                    Id = Guid.NewGuid ( ),
                    Author = book.Author,
                    Price = book.Price * 1.15M,
                    Title = book.Title,

                    SoldDate = DateTime.Now,
                    UserId = guid
                });
            }





            await store.SaveChangesAsync ( );

            return 0;
        }



        private static async Task<int> DropAsync() {
            var store = new BookStoreContext ( );
            await store.Database.ExecuteSqlCommandAsync ("DROP TABLE IF EXISTS _EFMigrationsHistory");
            await store.Database.ExecuteSqlCommandAsync ("DROP TABLE IF EXISTS book_solds");
            await store.Database.ExecuteSqlCommandAsync ("DROP TABLE IF EXISTS books");
            await store.Database.ExecuteSqlCommandAsync ("DROP TABLE IF EXISTS user_profiles");
            await store.Database.ExecuteSqlCommandAsync ("DROP TABLE IF EXISTS users");

            await store.SaveChangesAsync ( );
            return 0;
        }

        public static string Encrypt(string input) {
            return BCrypt.Net.BCrypt.HashPassword (input);
        }
    }
}
