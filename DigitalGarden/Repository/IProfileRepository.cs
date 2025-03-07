namespace MVCView.Models
{
    public interface IProfileRepository
    {
        void AddProfile(Profile profile);
        Profile GetProfile(int id);
        void UpdateProfile(Profile profile);
        void DeleteProfile(int id);
        IEnumerable<Profile> GetAllProfiles();
    }
}
