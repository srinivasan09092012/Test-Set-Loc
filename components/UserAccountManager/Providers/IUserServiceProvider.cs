using UserAccountManager.Domain;

namespace UserAccountManager.Providers
{
    public interface IUserServiceProvider
    {
        void AddProfile(UserProfile userProfile);

        void UpdateProfile(UserProfile userProfile);

        void ActiveProfile(string userId);

        void InactiveProfile(string userId);
    }
}
