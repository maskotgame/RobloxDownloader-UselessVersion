using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Xml.Linq;
using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;

string[] urls =
{
    "https://setup.rbxcdn.com/",
    "https://s3.amazonaws.com/setup.gametest2.robloxlabs.com/"
};

 Dictionary<string, string> Player = new Dictionary<string, string>
    {
        { "RobloxApp.zip", "" },
        { "redist.zip", "" },
        { "shaders.zip", "shaders/" },
        { "ssl.zip", "ssl/" },
        { "Libraries.zip", "" },

        { "WebView2.zip", "" },
        { "WebView2RuntimeInstaller.zip", "WebView2RuntimeInstaller/" },

        { "content-avatar.zip", "content/avatar/" },
        { "content-configs.zip", "content/configs/" },
        { "content-fonts.zip", "content/fonts/" },
        { "content-sky.zip", "content/sky/" },
        { "content-translations.zip", "content/translations/" },
        { "content-sounds.zip", "content/sounds/" },
        { "content-textures2.zip", "content/textures/" },
        { "content-models.zip", "content/models/" },

        { "content-platform-fonts.zip", "PlatformContent/pc/fonts/" },
        { "content-platform-dictionaries.zip", "PlatformContent/pc/shared_compression_dictionaries/" },
        { "content-terrain.zip", "PlatformContent/pc/terrain/" },
        { "content-textures3.zip", "PlatformContent/pc/textures/" },

        { "extracontent-luapackages.zip", "ExtraContent/LuaPackages/" },
        { "extracontent-translations.zip", "ExtraContent/translations/" },
        { "extracontent-models.zip", "ExtraContent/models/" },
        { "extracontent-textures.zip", "ExtraContent/textures/" },
        { "extracontent-places.zip", "ExtraContent/places/" }
    };

 Dictionary<string, string> Studio = new Dictionary<string, string>
    {
        { "RobloxStudio.zip", "" },
        { "RibbonConfig.zip", "RibbonConfig/" },
        { "redist.zip", "" },
        { "Libraries.zip", "" },
        { "LibrariesQt5.zip", "" },

        { "WebView2.zip", "" },
        { "WebView2RuntimeInstaller.zip", "" },

        { "shaders.zip", "shaders/" },
        { "ssl.zip", "ssl/" },

        { "Qml.zip", "Qml/" },
        { "Plugins.zip", "Plugins/" },
        { "StudioFonts.zip", "StudioFonts/" },
        { "BuiltInPlugins.zip", "BuiltInPlugins/" },
        { "ApplicationConfig.zip", "ApplicationConfig/" },
        { "BuiltInStandalonePlugins.zip", "BuiltInStandalonePlugins/" },

        { "content-qt_translations.zip", "content/qt_translations/" },
        { "content-sky.zip", "content/sky/" },
        { "content-fonts.zip", "content/fonts/" },
        { "content-avatar.zip", "content/avatar/" },
        { "content-models.zip", "content/models/" },
        { "content-sounds.zip", "content/sounds/" },
        { "content-configs.zip", "content/configs/" },
        { "content-api-docs.zip", "content/api_docs/" },
        { "content-textures2.zip", "content/textures/" },
        { "content-studio_svg_textures.zip", "content/studio_svg_textures/" },

        { "content-platform-fonts.zip", "PlatformContent/pc/fonts/" },
        { "content-platform-dictionaries.zip", "PlatformContent/pc/shared_compression_dictionaries/" },
        { "content-terrain.zip", "PlatformContent/pc/terrain/" },
        { "content-textures3.zip", "PlatformContent/pc/textures/" },

        { "extracontent-translations.zip", "ExtraContent/translations/" },
        { "extracontent-luapackages.zip", "ExtraContent/LuaPackages/" },
        { "extracontent-textures.zip", "ExtraContent/textures/" },
        { "extracontent-scripts.zip", "ExtraContent/scripts/" },
        { "extracontent-models.zip", "ExtraContent/models/" },

        { "studiocontent-models.zip", "StudioContent/models/" },
        { "studiocontent-textures.zip", "StudioContent/textures/" }
    };

string[] RCC = { 
        "RCC-Roblox22D1E478981C0E216194483993F0D3B4.exe",
        "RCC-Roblox7a50c7a9033709b969e4586a497b888a.exe",
        "RCC-RobloxA5XGEOZ35LAFQUL2.exe",
        "RCC-RobloxAB1FEC8F0BB9C736E1454364EA6D7D38.exe",
};

Console.WriteLine("Roblox version downloader, made by icscata");
Console.WriteLine("Please enter source (prod, gt2): ");
string urlChoice = Console.ReadLine()!;

string url;

switch (urlChoice.ToLower())
{
    case "prod":
        url = urls[0];
        break;
    case "gt2":
        url = urls[1];
        break;
    default:
        url = urls[0];
        break;
}

Console.WriteLine("Please enter your client type (RobloxApp, Studio, RCCService): ");
string clientType = Console.ReadLine()!;
//string clientType = "rccservice"; //todo add more downloaders
switch (clientType.ToLower())
{
    case "robloxapp":
        await GetApp(Player, url, "robloxapp");
        break;
    case "rccservice":
        await GetRCCService(RCC, url);
        break; 
    case "studio":
        await GetApp(Studio, url, "studio");
        break;
    default:
        Console.WriteLine("Enter a valid type please");
        break;
}

async Task<int> GetRCCService(string[] RCC, string url)
{
    string appSettings = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<Settings>\r\n\t<ContentFolder>content</ContentFolder>\r\n\t<BaseUrl>http://www.roblox.com</BaseUrl>\r\n</Settings>\r\n";
    Console.WriteLine("Enter version-hash: ");
    string versionHash = Console.ReadLine()!;
    if (versionHash.Length != 24)
    {
        Console.WriteLine("Invalid version-hash provided");
        return await GetRCCService(RCC, url);
    }
    HttpClient client = new HttpClient();
    string stringsExe = @"strings.exe";
    if (!File.Exists(stringsExe))
    {
        await DownloadFileTaskAsync(client, $"https://live.sysinternals.com/strings.exe", $"strings.exe");
    }
    int i;
    bool found = false;
    Console.WriteLine("Creating folder for downloading..");
    System.IO.Directory.CreateDirectory(versionHash);
    string[] lines = [];
    for (i=0; i < RCC.Length; i++)
    {
        Console.WriteLine($"Trying {url}{versionHash}-{RCC[i]}...");
        try
        {
            await DownloadFileTaskAsync(client, $"{url}{versionHash}-{RCC[i]}", $"{versionHash}/{RCC[i]}");
            Console.WriteLine($"Found Hash!");
            found = true;

            var psi = new ProcessStartInfo
            {
                FileName = stringsExe,
                Arguments = $"-accepteula -u \"{versionHash}/{RCC[i]}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using var process = Process.Start(psi);
            if (process == null)
            { return 1; }

            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            lines = output.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None);
            break;
        } catch (Exception) { }
    }

    if (!found)
    {
        Console.WriteLine("No hash found, quitting");
        System.IO.Directory.Delete(versionHash);
        return 1;
    }

    var fileNames = lines
               .Where(line => line.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
               .ToList();
    foreach (var name in fileNames)
    {
        try {
            Console.WriteLine($"Downloading {versionHash}-{name}");
            await DownloadFileTaskAsync(client, $"{url}{versionHash}-{name}", $"{versionHash}/{name}");
            Console.WriteLine($"Extracting {versionHash}-{name}");
            System.IO.Compression.ZipFileEx.ExtractToDirectory($"{versionHash}/{name}", $"{versionHash}");
            File.Delete($"{versionHash}/{name}");
        }
        catch (Exception e) {
            Console.WriteLine($"{e.Message}, if 403 do not wory, some assets may not be available");
        }
    }
    Console.WriteLine("Creating AppSettings.xml...");
    File.WriteAllText($"{versionHash}/AppSettings.xml", appSettings);
    Console.WriteLine("Done!");
    return 0;
}

async Task<int> GetApp(Dictionary<string, string> App, string url, string type)
{
    HttpClient client = new HttpClient();

    string appSettings = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<Settings>\r\n\t<ContentFolder>content</ContentFolder>\r\n\t<BaseUrl>http://www.roblox.com</BaseUrl>\r\n</Settings>\r\n";
    Console.WriteLine("Enter version-hash: ");
    string versionHash = Console.ReadLine()!;
    if (versionHash.Length != 24)
    {
        Console.WriteLine("Invalid version-hash provided");
        return await GetApp(App, url, type);
    }
    Console.WriteLine("Enter channel (only for prod)(enter nothing for LIVE): ");
    string channel = Console.ReadLine()!;
    if (channel != "")
    {
        url = $"{url}channel/{channel}/";
        //Console.WriteLine(url);
        /*HttpResponseMessage response1 = await client.GetAsync($"{url}version");
        if (response1.StatusCode == System.Net.HttpStatusCode.Forbidden)
        {
            Console.WriteLine("Invalid channel provided");
            return await GetApp(App, url, type);
        }*/
    }
    HttpResponseMessage response = await client.GetAsync($"{url}{versionHash}-rbxPkgManifest.txt");
    bool hasPkg = true;
    if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
    {
        //Console.WriteLine("Client does not have rbxPkgManifest, please ping me to implement a method to download earlier clients");
        //return await GetApp(App, url);
        Console.WriteLine("WARNING: Client does not have rbxPkgManifest, either invalid or old. Trying to extract the filenames from the bootstrapper");
        hasPkg = false;
    }
    Console.WriteLine("Creating folder for downloading..");
    System.IO.Directory.CreateDirectory(versionHash);
    string[] lines = { };
    if (hasPkg) {
        string content = await response.Content.ReadAsStringAsync();
        lines = content.Split(
            new[] { "\r\n", "\r", "\n" },
            StringSplitOptions.None
        );
    } else {
        string stringsExe = @"strings.exe";
        if (!File.Exists(stringsExe))
        {
            await DownloadFileTaskAsync(client, $"https://live.sysinternals.com/strings.exe", $"strings.exe");
        }
        string bootstrapperName = "";
        switch (type)
        {
            case "robloxapp":
                bootstrapperName = "Roblox.exe";
                break;
            case "studio":
                bootstrapperName = "RobloxStudioLauncherBeta.exe";
                break;
            default:
                break;
        }
        try {
            await DownloadFileTaskAsync(client, $"{url}{versionHash}-{bootstrapperName}", $"{versionHash}/{bootstrapperName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Invalid version-hash provided, ex{ex.Message}, url{url}{versionHash}-{bootstrapperName}");
            return await GetApp(App, url, type);
        }
        var psi = new ProcessStartInfo
        {
            FileName = stringsExe,
            Arguments = $"-accepteula -u \"{versionHash}/{bootstrapperName}\"",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        using var process = Process.Start(psi);
        if (process == null)
        { return 0; }
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        lines = output.Split(
            new[] { "\r\n", "\r", "\n" },
            StringSplitOptions.None);
    }
    var fileNames = lines
        .Where(line => line.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
        .ToList();
    foreach (var name in fileNames)
    {
        Console.WriteLine($"Downloading {versionHash}-{name}");
        try
        {
            await DownloadFileTaskAsync(client, $"{url}{versionHash}-{name}", $"{versionHash}/{name}");
        }
        catch (Exception ex) {
            Console.WriteLine($"WARNING: Didint find file {versionHash}-{name}, skipping file");
            continue;
        }
        
        Console.WriteLine($"Extracting {versionHash}-{name}");
        string location;
        try
        {
            location = $"{versionHash}/{App[name]}/";
        }
        catch (Exception ex)
        {
            if (name.Contains("content"))
            {
                location = $"{versionHash}/{name.Replace("-", "/").Replace(".zip", "")}/"; //works most of the time for this
            } else
            {
                location = $"{versionHash}/";
            }
        }
        System.IO.Compression.ZipFileEx.ExtractToDirectory($"{versionHash}/{name}", location);
        File.Delete($"{versionHash}/{name}");
    }
    var fileexe = lines
    .Where(line => line.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
    .ToList();
    if (!hasPkg)
    {
        Console.WriteLine("Creating AppSettings.xml...");
        File.WriteAllText($"{versionHash}/AppSettings.xml", appSettings);
        Console.WriteLine("Done!");
        return 0;
    }
    foreach (var exe in fileexe)
    {
        Console.WriteLine($"Downloading {versionHash}-{exe}");
        try
        {
            await DownloadFileTaskAsync(client, $"{url}{versionHash}-{exe}", $"{versionHash}/{exe}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"WARNING: Didint find file {versionHash}-{exe}, skipping file");
            continue;
        }
    }
    Console.WriteLine("Creating AppSettings.xml...");
    File.WriteAllText($"{versionHash}/AppSettings.xml", appSettings);
    Console.WriteLine("Done!");
    return 0;
}

async Task DownloadFileTaskAsync(HttpClient client, string uri, string FileName)
{
    using (var s = await client.GetStreamAsync(uri))
    {
        using (var fs = new FileStream(FileName, FileMode.CreateNew))
        {
            await s.CopyToAsync(fs);
        }
    }
}

//thanks stackoverflow guy
namespace System.IO.Compression
{
    public static class ZipFileEx
    {
        public static void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName) =>
        ExtractToDirectory(sourceArchiveFileName, destinationDirectoryName, entryNameEncoding: null, overwriteFiles: false);

        public static void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName, bool overwriteFiles) =>
            ExtractToDirectory(sourceArchiveFileName, destinationDirectoryName, entryNameEncoding: null, overwriteFiles: overwriteFiles);

        public static void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName, Encoding? entryNameEncoding) =>
            ExtractToDirectory(sourceArchiveFileName, destinationDirectoryName, entryNameEncoding: entryNameEncoding, overwriteFiles: false);

        public static void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName, Encoding? entryNameEncoding, bool overwriteFiles)
        {
            ArgumentNullException.ThrowIfNull(sourceArchiveFileName);
            ArgumentNullException.ThrowIfNull(destinationDirectoryName);

            using ZipArchive archive = ZipFile.Open(sourceArchiveFileName, ZipArchiveMode.Read, entryNameEncoding);

            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                DirectoryInfo di = Directory.CreateDirectory(destinationDirectoryName);
                string destinationDirectoryFullPath = di.FullName;
                if (!destinationDirectoryFullPath.EndsWith(Path.DirectorySeparatorChar))
                {
                    char sep = Path.DirectorySeparatorChar;
                    destinationDirectoryFullPath = string.Concat(destinationDirectoryFullPath, new ReadOnlySpan<char>(in sep));
                }

                string entryFullName = entry.FullName;
                if (entryFullName.Length > 0 && entryFullName[0] == '\\') entryFullName = entryFullName[1..]; // remove leading root

                string fileDestinationPath = Path.GetFullPath(Path.Combine(destinationDirectoryFullPath, entryFullName.Replace('\0', '_')));

                var IsCaseSensitive = !(OperatingSystem.IsWindows() || OperatingSystem.IsMacOS() || OperatingSystem.IsIOS() || OperatingSystem.IsTvOS() || OperatingSystem.IsWatchOS());
                var stringComparison = IsCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

                if (!fileDestinationPath.StartsWith(destinationDirectoryFullPath, stringComparison)) Console.WriteLine(@"Extracting Zip entry would have resulted in a file outside the specified destination directory.");

                if (Path.GetFileName(fileDestinationPath).Length == 0)
                {
                    if (entry.Length != 0) throw new IOException(@"Zip entry name ends in directory separator character but contains data.");
                    Directory.CreateDirectory(fileDestinationPath);

                }
                else
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(fileDestinationPath)!);
                    entry.ExtractToFile(fileDestinationPath, overwrite: overwriteFiles);
                }
            }
        }
    }
}