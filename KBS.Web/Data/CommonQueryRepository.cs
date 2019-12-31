using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using KBS.Web.Services;

namespace KBS.Web.Data {
    public class CommonQueryRepository {
        private readonly IConnectionFactory _factory;
        public CommonQueryRepository(IConnectionFactory factory) {
            _factory = factory;
        }

        public async Task<DateTime> CurrentDateAsync() {
            using (var connection = _factory.Connection ( )) {
                return await connection.QuerySingleAsync<DateTime> ("SELECT now()");
            }
        }
    }
}
