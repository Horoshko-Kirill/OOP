using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using File = Google.Apis.Drive.v3.Data.File;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

public class GoogleDriveManager
{
    private readonly DriveService _service;
    private string _lr2FolderId;

    public GoogleDriveManager()
    {
        var credential = Authenticate();
        _service = new DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "Google Drive LR2",
        });

        _lr2FolderId = GetOrCreateFolder("LR2");
    }

    private UserCredential Authenticate()
    {
        using var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read);
        string credPath = "token.json";
        return GoogleWebAuthorizationBroker.AuthorizeAsync(
            GoogleClientSecrets.Load(stream).Secrets,
            new[] { DriveService.Scope.Drive },
            "user",
            CancellationToken.None,
            new FileDataStore(credPath, true)).Result;
    }

    private string GetOrCreateFolder(string folderName)
    {
        var request = _service.Files.List();
        request.Q = $"name='{folderName}' and mimeType='application/vnd.google-apps.folder' and trashed=false";
        request.Fields = "files(id, name)";
        var result = request.Execute();

        if (result.Files.Count > 0)
        {
            return result.Files[0].Id;
        }

        var fileMetadata = new File()
        {
            Name = folderName,
            MimeType = "application/vnd.google-apps.folder"
        };

        var createRequest = _service.Files.Create(fileMetadata);
        createRequest.Fields = "id";
        var folder = createRequest.Execute();

        return folder.Id;
    }

    public void UploadFile(string localPath)
    {
        var fileMetadata = new File()
        {
            Name = Path.GetFileName(localPath),
            Parents = new List<string> { _lr2FolderId }
        };

        using var stream = new FileStream(localPath, FileMode.Open);
        var request = _service.Files.Create(fileMetadata, stream, GetMimeType(localPath + "/LR2"));
        request.Fields = "id";
        request.Upload();
        Console.WriteLine($"Загружен файл: {Path.GetFileName(localPath)}");
    }

    public void DownloadFile(string fileName, string savePath)
    {
        var fileId = FindFileId(fileName);
        if (fileId == null)
        {
            Console.WriteLine($"Файл '{fileName}' не найден в папке LR2.");
            return;
        }

        var request = _service.Files.Get(fileId);
        using var stream = new MemoryStream();
        request.Download(stream);

        stream.Position = 0;
        using var fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write);
        stream.CopyTo(fileStream);
        Console.WriteLine($"Скачан файл: {savePath}");
    }

    public void DeleteFile(string fileName)
    {
        var fileId = FindFileId(fileName);
        if (fileId == null)
        {
            Console.WriteLine($"Файл '{fileName}' не найден в папке LR2.");
            return;
        }

        _service.Files.Delete(fileId).Execute();
        Console.WriteLine($"Удалён файл: {fileName}");
    }

    private string? FindFileId(string fileName)
    {
        var request = _service.Files.List();
        request.Q = $"name='{fileName}' and '{_lr2FolderId}' in parents and trashed=false";
        request.Fields = "files(id)";
        var result = request.Execute();

        return result.Files.FirstOrDefault()?.Id;
    }

    private string GetMimeType(string fileName)
    {
        var ext = Path.GetExtension(fileName).ToLower();
        return ext switch
        {
            ".txt" => "text/plain",
            ".pdf" => "application/pdf",
            ".jpg" => "image/jpeg",
            ".png" => "image/png",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            _ => "application/octet-stream",
        };
    }
}
