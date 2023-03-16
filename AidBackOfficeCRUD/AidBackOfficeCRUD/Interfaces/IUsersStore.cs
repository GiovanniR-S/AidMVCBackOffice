using AidBackOfficeCRUD.Models;
using Microsoft.AspNetCore.Identity;

namespace AidBackOfficeCRUD.Interfaces {
    public interface IUsersStore {

        Task<IdentityResult> CreateAsync (MyUser user, CancellationToken cancellationToken);

        Task<IdentityResult> DeleteAsync (MyUser user, CancellationToken cancellationToken);

        Task<MyUser?> FindByIdAsync (string userId, CancellationToken cancellationToken);

        Task<MyUser?> FindByNameAsync (string normalizedUserName, CancellationToken cancellationToken);

        Task<string?> GetNormalizedUserNameAsync (MyUser user, CancellationToken cancellationToken);

        Task<int> UserGetIdAsync (MyUser user, CancellationToken cancellationToken);

        Task<string?> GetUserNameAsync (MyUser user, CancellationToken cancellationToken);

        Task SetNormalizedUserNameAsync (MyUser user, string? normalizedName, CancellationToken cancellationToken);

        Task SetUserNameAsync (MyUser user, string? userName, CancellationToken cancellationToken);

        Task<IdentityResult> UpdateAsync (MyUser user, CancellationToken cancellationToken);

    }
}
