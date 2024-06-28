namespace N.G.HRS.Repository.File_Upload
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAsync(IFormFile file, string baseUploadPath);

    }
}
