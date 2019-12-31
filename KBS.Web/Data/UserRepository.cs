using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using KBS.Web.Data.Poco;
using Dapper.Contrib;
using KBS.Web.Infrastructure;
using KBS.Web.Model;
using KBS.Web.Services;

namespace KBS.Web.Data {
    public class UserRepository {
        private readonly IConnectionFactory _factory;

        private readonly IPasswordService _passwordService;
        public UserRepository(IConnectionFactory factory, IPasswordService passwordService) {
            _factory = factory;
            _passwordService = passwordService;
        }

        public UserRepository(IConnectionFactory factory) {
            _factory = factory;
        }


        public async Task<User> GetByUsernameAsync(String username) {
            using (IDbConnection connection = _factory.Connection ( )) {
                return await connection.QuerySingleOrDefaultAsync<User> ($"SELECT * from users WHERE username = @username", new {
                    username = username
                });
            }
        }

        public async Task<string> Add(AddUserRequest model, UserRoleEnum userRole) {

            if (String.IsNullOrEmpty (model.Username)) throw new ArgumentException ("Username not valid");
            if (String.IsNullOrEmpty (model.Password)) throw new ArgumentException ("Password not valid");

            var isUsed = await IsUsernameUsed (model.Username);
            if (isUsed) throw new ArgumentException ("Username already in use");

            using (IDbConnection connection = _factory.Connection ( )) {
                Guid userId = Guid.NewGuid ( );
                Guid profileId = Guid.NewGuid ( );

                var sql = @"INSERT INTO users(
	                            id, username, password, role, is_deleted)
	                            VALUES (uuid( @id), @username, @password, @role, 0);";


                await connection.ExecuteAsync (sql,
                    new {
                        id = userId.ToString ( ),
                        username = model.Username,
                        password = _passwordService.Encrypt (model.Password),
                        role = (int)userRole
                    });

                var profileSql = @"INSERT INTO user_profiles(
	                                id, user_id, email, phone, address)
	                                VALUES (uuid(@id), uuid(@userId), @email, @phone, @address);";


                await connection.ExecuteAsync (profileSql,
                    new {
                        id = profileId.ToString ( ),
                        userId = userId.ToString ( ),
                        email = model.Email ?? "",
                        phone = model.Phone ?? "",
                        address = model.Address ?? ""

                    });



                return userId.ToString ( );
            }
        }

        public async Task<bool> Delete(DeleteUserRequest model) {
            using (IDbConnection connection = _factory.Connection ( )) {
                connection.Open ( );
                var sql = @"UPDATE users set is_deleted = 1 WHERE CAST(id as VARCHAR) =  @id";
                await connection.ExecuteAsync (sql, new { id = model.UserId });

                return true;
            }
        }

        public async Task<bool> Update(UpdateProfileRequest model) {
            using (IDbConnection connection = _factory.Connection ( )) {
                connection.Open ( );

                var sql = @"UPDATE user_profiles
	                       SET email=@email, phone=@phone, address=@address    
                            WHERE CAST(userId as VARCHAR) = @userId;";

                await connection.ExecuteAsync (sql,
                    new {
                        userId = model.UserId,
                        email = model.Email,
                        address = model.Address,
                        phone = model.Phone
                    });

                return true;
            }
        }

        private async Task<bool> IsUsernameUsed(String username) {
            using (IDbConnection connection = _factory.Connection ( )) {
                connection.Open ( );
                var sql = "select count(id) from users where username = @username";
                var count = await connection.ExecuteScalarAsync<int> (sql, new { username = username });

                return count > 0;
            }
        }
    }
}
