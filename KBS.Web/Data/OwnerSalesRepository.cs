using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using KBS.Web.Data.Entity;
using KBS.Web.Data.Poco;
using KBS.Web.Model;
using KBS.Web.Services;

namespace KBS.Web.Data
{
    public class OwnerSalesRepository
    {
        private readonly IConnectionFactory _factory;
        
        public OwnerSalesRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<SalemanListItem>> List()
        {
            using (IDbConnection connection = _factory.Connection())
            {
                connection.Open();

                var sql = @"SELECT 
                    users.id as Id, 
                    users.username as Username, 
                    user_profiles.id as userId, 
                    user_profiles.email as Email,     
                    user_profiles.address as Address                    
                    FROM users LEFT JOIN user_profiles ON user_profiles.user_id = users.id 
                    WHERE users.is_deleted = 0 AND users.role = @role";
                
                return await connection.QueryAsync<SalemanListItem>(sql, new { role = (int)UserRoleEnum.Salesman});
            }
        }

       
    }
}