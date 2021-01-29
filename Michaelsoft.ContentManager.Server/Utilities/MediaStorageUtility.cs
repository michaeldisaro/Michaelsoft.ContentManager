using System;
using System.IO;
using System.Threading.Tasks;
using Michaelsoft.ContentManager.Common.BaseClasses;
using Michaelsoft.ContentManager.Server.Exceptions;
using Michaelsoft.ContentManager.Server.Settings;

namespace Michaelsoft.ContentManager.Server.Utilities
{
    public class MediaStorageUtility : InjectableServicesBaseStaticClass
    {

        private static string Init(string contentFolder,
                                   bool forCreation = true)
        {
            var mediaStorageSetting = (MediaStorageSettings) Services.GetService(typeof(IMediaStorageSettings));
            var rootFolder = mediaStorageSetting.Path;
            var storageFolder = Path.Combine(rootFolder, contentFolder);
            if (forCreation && !Directory.Exists(storageFolder))
                Directory.CreateDirectory(storageFolder);
            return storageFolder;
        }

        public static string GetMimeTypeFromMediaName(string name)
        {
            var extension = name.Split(".")[^1].ToLower();
            return extension switch
            {
                "jpg" => "image/jpeg",
                "jpeg" => "image/jpeg",
                "png" => "image/png",
                _ => throw new Exception("Media type not supported")
            };
        }

        public static async Task CreateMedia(byte[] data,
                                             string folder,
                                             string fileName)
        {
            var path = Init(folder);
            var filePath = Path.Combine(path, $"{fileName}");
            if (File.Exists(filePath)) return;
            await using var fileStream = new FileStream(filePath, FileMode.Create);
            await fileStream.WriteAsync(data);
        }

        public static async Task<byte[]> GetMedia(string folder,
                                                  string fileName)
        {
            var path = Init(folder, false);
            var filePath = Path.Combine(path, $"{fileName}");
            if (File.Exists(filePath))
                return await File.ReadAllBytesAsync(filePath);
            throw new MediaNotFoundException("Media not found");
        }

        public static bool DeleteMedia(string folder,
                                       string fileName)
        {
            var path = Init(folder, false);
            var filePath = Path.Combine(path, $"{fileName}");
            if (File.Exists(filePath))
                File.Delete(filePath);
            return !File.Exists(filePath);
        }

    }
}