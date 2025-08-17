using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Xml.Linq;
using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;

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

string[][] RCC = {
    new string[] { 
        "RCC-Roblox22D1E478981C0E216194483993F0D3B4.exe",
        "RCCService5EF3896D702123CA38B3ACF2FBB4EB78.zip",
        "RCC-redistB1C42DC209E175BF4E98DBB3C1E056CF8.zip",
        "RCC-LibrariesB1C42DC209E175BF4E98DBB3C1E056CF8.zip",
        "RCC-shadersB1C42DC209E175BF4E98DBB3C1E056CF8.zip",
        "RCC-contentB1C42DC209E175BF4E98DBB3C1E056CF8.zip",
        "RCC-platformcontentB1C42DC209E175BF4E98DBB3C1E056CF8.zip"
    },
    new string[] {
        "RCC-Roblox7a50c7a9033709b969e4586a497b888a.exe",
        "RCCService03683f72d1cbb3f87434ffca5afd5db2.zip",
        "RCC-redistB1C42DC209E175BF4E98DBB3C1E056CF8.zip", // it looks like they didint change the content hash here, lmao
        "RCC-LibrariesB1C42DC209E175BF4E98DBB3C1E056CF8.zip",
        "RCC-shadersB1C42DC209E175BF4E98DBB3C1E056CF8.zip",
        "RCC-sslB1C42DC209E175BF4E98DBB3C1E056CF8.zip",
        "RCC-contentB1C42DC209E175BF4E98DBB3C1E056CF8.zip",
        "RCC-platformcontentB1C42DC209E175BF4E98DBB3C1E056CF8.zip"
    },
    new string[] {
        "RCC-RobloxA5XGEOZ35LAFQUL2.exe",
        "RCCServiceR7Z9CYTW7WBR95VW.zip",
        "RCC-redistXGTFDE2U040VW06D.zip",
        "RCC-LibrariesXGTFDE2U040VW06D.zip",
        "RCC-shadersXGTFDE2U040VW06D.zip",
        "RCC-contentXGTFDE2U040VW06D.zip",
        "RCC-sslXGTFDE2U040VW06D.zip",
        "RCC-extracontentXGTFDE2U040VW06D.zip",
        "RCC-platformcontentXGTFDE2U040VW06D.zip"
    },

    new string[] { // the oldest one we have, after the production wipe we havent had any other hashes that use this
        "RCC-RobloxAB1FEC8F0BB9C736E1454364EA6D7D38.exe",
        "RCCService2AFBA34ACD542E96B3890871CBA18F43.zip",
        "RCC-redistC134558C4C663041855C887179E44491.zip",
        "RCC-LibrariesC134558C4C663041855C887179E44491.zip",
        "RCC-shadersC134558C4C663041855C887179E44491.zip",
        "RCC-contentC134558C4C663041855C887179E44491.zip",
        "RCC-platformcontentC134558C4C663041855C887179E44491.zip"
    }
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

async Task<int> GetRCCService(string[][] RCC, string url)
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
    int i;
    bool found = false;
    for (i=0; i < RCC.Length; i++)
    {
        Console.WriteLine($"Trying {url}{versionHash}-{RCC[i][0]}...");
        HttpResponseMessage response = await client.GetAsync($"{url}{versionHash}-{RCC[i][0]}");
        if (response.StatusCode != System.Net.HttpStatusCode.Forbidden)
        {
            found = true;
            Console.WriteLine("Found Hash!");
            break;
        }
    }
    if (!found)
    {
        Console.WriteLine("No hash found, quitting");
        return 1;
    }
    Console.WriteLine("Creating folder for downloading..");
    System.IO.Directory.CreateDirectory(versionHash);
    for (int j = 0; j < RCC[i].Length; j++)
    {
        try {
            Console.WriteLine($"Downloading {versionHash}-{RCC[i][j]}");
            await DownloadFileTaskAsync(client, $"{url}{versionHash}-{RCC[i][j]}", $"{versionHash}/{RCC[i][j]}");
            if (j == 0) continue;
            Console.WriteLine($"Extracting {versionHash}-{RCC[i][j]}");
            System.IO.Compression.ZipFile.ExtractToDirectory($"{versionHash}/{RCC[i][j]}", $"{versionHash}");
            File.Delete($"{versionHash}/{RCC[i][j]}");
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
        HttpResponseMessage response1 = await client.GetAsync($"{url}version");
        if (response1.StatusCode == System.Net.HttpStatusCode.Forbidden)
        {
            Console.WriteLine("Invalid channel provided");
            return await GetApp(App, url, type);
        }
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
            Arguments = $"-u \"{versionHash}/{bootstrapperName}\"",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        using var process = Process.Start(psi);
        if (process == null)
        {
            Console.WriteLine("ERROR: Please put strings.exe in the same directory as the exe");
            return 1;
        }
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
        System.IO.Compression.ZipFile.ExtractToDirectory($"{versionHash}/{name}", location);
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