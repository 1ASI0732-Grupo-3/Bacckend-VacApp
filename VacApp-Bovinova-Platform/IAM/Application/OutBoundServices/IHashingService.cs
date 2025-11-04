namespace VacApp_Bovinova_Platform.IAM.Application.OutBoundServices;

public interface IHashingService
{
    public string GenerateHash(string password);
    public bool VerifyHash(string password, string hash);
}
