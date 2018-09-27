using UserAccountManager.Domain;

namespace UserAccountManager.Providers
{
    public interface IUserServiceProvider
    {
        void AddProfile(UserProfile userProfile);

        void UpdateProfile(UserProfile newUserProfile, UserProfile oldUserProfile);

        void ActiveProfile(string userId);

        void InactiveProfile(string userId);
    }
}
