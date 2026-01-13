using Carter;
using System.Text;

namespace lproject.IOFileStream.Controllers;

public class FilesMinimalApis : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("upload", async (UploadText uploadText, IWebHostEnvironment env, IConfiguration configuration) =>
        {
            var folder = FileExtensions.GetFileTypeFromFilePath(uploadText.FilePath).GetFolderPath(configuration);
            var folderPath = Path.Combine(env.ContentRootPath, folder);
            CreateDictionaryIfNotExists(folderPath);
            var fullPath = Path.Combine(folderPath, uploadText.FilePath);
            await using (FileStream outputFile = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                await using (StreamWriter outputWriter = new StreamWriter(outputFile))
                {
                    await outputWriter.WriteAsync(uploadText.Text);
                }
            }
        });

        app.MapPost("load", async (LoadFile loadFile, IWebHostEnvironment env, IConfiguration configuration) =>
        {
            var folder = FileExtensions.GetFileTypeFromFilePath(loadFile.FilePath).GetFolderPath(configuration);
            var folderPath = Path.Combine(env.ContentRootPath, folder);
            CreateDictionaryIfNotExists(folderPath);
            StringBuilder ans = new StringBuilder();
            var fullPath = Path.Combine(folderPath, loadFile.FilePath);
            await using (FileStream fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader outputReader = new StreamReader(fileStream))
                {
                    ans.AppendLine(outputReader.ReadLine());
                }
            }
            return ans.ToString();
        });


    }
    private static void CreateDictionaryIfNotExists(string folderPath)
    {
        bool exists = Directory.Exists(folderPath);

        if (!exists)
        {
            Directory.CreateDirectory(folderPath);
        }
    }
}


public record UploadText(string Text, string FilePath);

public record LoadFile(string FilePath);


public enum FileType
{
    Text,
    Binary,
    Image,
    Video
}

public enum FileExtension
{
    Txt, 
    Bin,
    Jpg,
    Mp4
}

public static class FileExtensions
{
    public static string GetFolderPath(this FileType fileType, IConfiguration configuration)
    {
        var fileTypeName = Enum.GetName(typeof(FileType), fileType)?.ToString() ?? throw new ArgumentOutOfRangeException(nameof(fileType));
        var typeSection = $"FilesConfiguration:{fileTypeName}Folder";
        return configuration.GetRequiredSection(typeSection)?.Value?.ToString() ?? throw new InvalidOperationException($"{typeSection} configuration is missing");
    }

    public static FileType GetFileTypeFromFilePath(string filePath)
    {
        var filePathLength = filePath.Length; 
        var span = filePath.AsSpan();
        var fileExtensionString = span.Slice(filePathLength - 3, 1).ToString().ToUpper() + span.Slice(filePathLength - 2).ToString().ToLower();
      
        var fileExtension = (FileType)Enum.Parse(typeof(FileExtension), fileExtensionString);

        ArgumentException.ThrowIfNullOrEmpty(nameof(fileExtension));

        return fileExtension;
    }
}

