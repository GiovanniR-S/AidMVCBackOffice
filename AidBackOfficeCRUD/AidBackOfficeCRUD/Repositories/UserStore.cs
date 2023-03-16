using AidBackOfficeCRUD.Interfaces;
using AidBackOfficeCRUD.Models;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace AidBackOfficeCRUD.Repositories
{
    public class UserStore : IUserStore<MyUser>, IUserPasswordStore<MyUser>
    {
        public async Task<IdentityResult> CreateAsync(MyUser user, CancellationToken cancellationToken)
        {
            using(var connection = GetOpenConnection()) {

                await connection.ExecuteAsync("insert into Users([Id], " +
                    "[UserName], [NormalizedUserName], [PasswordHash], " +
                    "[UserEmail]) " +
                    "values " +
                    "(@Id, @UserName, @NormalizedUserName, @PasswordHash, @UserEmail)",
                    new {
                        Id = user.Id,
                        UserName = user.UserName,
                        NormalizedUserName = user.NormalizedUserName,
                        PasswordHash = user.PasswordHash,
                        UserEmail = user.UserEmail

                    });
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(MyUser user, CancellationToken cancellationToken)
        {
            using(var connection = GetOpenConnection()) {

                await connection.ExecuteAsync("delete from Users where Id = @Id",
                    new {
                        Id = user.Id,

                    });
            }
            return IdentityResult.Success;
        }

        public void Dispose()
        {
            
        }

        public static DbConnection GetOpenConnection() {

            var connection = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AidCrusader;Data Source=DESKTOP-79R28QA\\SQLEXPRESS;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

            connection.Open();

            return connection;

        }

        public async Task<MyUser?> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            using(var connection = GetOpenConnection()) {

                return await connection.QueryFirstOrDefaultAsync<MyUser>(

                    "Select * from Users where Id = @Id",
                    new { Id = userId }

                );

            }


        }

        public async Task<MyUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            using(var connection = GetOpenConnection()) {

                return await connection.QueryFirstOrDefaultAsync<MyUser>(

                    "Select * from Users where normalizedUserName = @name",
                    new { name = normalizedUserName }

                );

            }
        }

        public async Task<string?> GetNormalizedUserNameAsync(MyUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.NormalizedUserName);
        }

        public async Task<string> UserGetIdAsync (MyUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Id);
        }
        public async Task<string> GetUserIdAsync(MyUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Id.ToString());
        }

        public async Task<string?> GetUserNameAsync(MyUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(MyUser user, string? normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(MyUser user, string? userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(MyUser user, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection()) {

                await connection.ExecuteAsync("update Users " +
                    "set [Id] = @Id " +
                    "[UserName] = @UserName " +
                    "[NormalizedUserName] = @NormalizedUserName " +
                    "[PasswordHash] = @PasswordHash " +
                    "[UserEmail] = @UserEmail " +
                    "where [Id] = @Id", 
                    new {
                        Id = user.Id,
                        UserName = user.UserName,
                        NormalizedUserName = user.NormalizedUserName,
                        PasswordHash = user.PasswordHash,
                        UserEmail = user.UserEmail

                    });
            }

            return IdentityResult.Success;

        }

        public Task SetPasswordHashAsync (MyUser user, string? passwordHash, CancellationToken cancellationToken) {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string?> GetPasswordHashAsync (MyUser user, CancellationToken cancellationToken) {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync (MyUser user, CancellationToken cancellationToken) {
            return Task.FromResult(user.PasswordHash != null);
        }
    }
}
