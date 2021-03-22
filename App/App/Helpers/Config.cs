using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace App.Helpers
{
    static class Config
    {
        public static string getApiBaseUrl()
        {
            // Get the assembly this code is executing in
            var assembly = Assembly.GetExecutingAssembly();

            // Look up the resource names and find the one that ends with settings.json
            // Your resource names will generally be prefixed with the assembly's default namespace
            // so you can short circuit this with the known full name if you wish
            var resName = assembly.GetManifestResourceNames()
             ?.FirstOrDefault(r => r.EndsWith("settings.json", StringComparison.OrdinalIgnoreCase));

            // Load the resource file
            var file = assembly.GetManifestResourceStream(resName);

            // Stream reader to read the whole file
            var sr = new StreamReader(file);

            // Read the json from the file
            var json = sr.ReadToEnd();

            var appSettings = JsonDocument.Parse(json, new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip });
            var result = appSettings.RootElement.GetProperty("apiUrlBase").GetString();

            return result;
        }

    }
}
