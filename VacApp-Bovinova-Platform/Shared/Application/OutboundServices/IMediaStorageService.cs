namespace VacApp_Bovinova_Platform.Shared.Application.OutboundServices;

public interface IMediaStorageService
{
    public string UploadFileAsync(string fileName, Stream fileData);
    public void UpdateFileAsync(string url, Stream fileData);
}
