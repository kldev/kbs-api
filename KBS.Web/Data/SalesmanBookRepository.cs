using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using KBS.Web.Data.Entity;
using KBS.Web.Services;

namespace KBS.Web.Data
{
    public class SalesmanBookRepository
    {
        private readonly IConnectionFactory _factory;
        
        public SalesmanBookRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<BookSoldItem>> List(Guid salesmanId)
        {
            var sql = @"SELECT CAST(id as VARCHAR) as Id, CAST(user_id as VARCHAR) as UserId,
                                title as Title,
                                author as Author,
                                price as Price,
                                sold_date as SoldDate
                    FROM books_sold WHERE CAST(user_id as VARCHAR) = @userId";
            
            using (IDbConnection connection = _factory.Connection())
            {
                return await connection.QueryAsync<BookSoldItem>(sql, new { userId = salesmanId.ToString()});
            }
        }
    }
}