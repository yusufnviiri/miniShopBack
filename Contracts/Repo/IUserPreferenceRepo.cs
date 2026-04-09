using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IUserPreferenceRepo
    {
        Task<IEnumerable<UserPreference>> GetAllUserPreferences();
        IQueryable<UserPreference> HomeUserPreferencesQueryData();
        Task<UserPreference?> FindUserPreferenceById(Guid userPreferenceId, bool tracking);
        Task<UserPreference?> FindUserPreferenceForUpdate(Guid userPreferenceId);
        void CreateUserPreference(UserPreference userPreference);
        void UpdateUserPreference(UserPreference userPreference);
        void DeleteUserPreference(UserPreference userPreference);
    }

}
