namespace SecurityService.UserServices
{
    public interface IUserRepository
    {
        bool ValidateCredentials(string username, string password);

        TaskUser FindBySubjectId(string subjectId);

        TaskUser FindByUsername(string username);
    }
}
