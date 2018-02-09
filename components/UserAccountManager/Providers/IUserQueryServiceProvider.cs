using UserAccountManager.Domain;

namespace UserAccountManager.Providers
{
    public interface IUserQueryServiceProvider
    {
        UserProfile LoadUserProfile(string userName);
    }
}
