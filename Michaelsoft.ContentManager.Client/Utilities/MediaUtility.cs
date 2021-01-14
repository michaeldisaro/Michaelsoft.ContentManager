using System;
using System.IO;
using System.Threading.Tasks;
using Michaelsoft.ContentManager.Common.BaseClasses;

namespace Michaelsoft.ContentManager.Client.Utilities
{
    public class MediaUtility : InjectableServicesBaseStaticClass
    {

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

    }
}