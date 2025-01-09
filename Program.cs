using System.Net;
using System.Text.RegularExpressions;
using TextCopy;


if (args.Length == 0)
{
    Console.WriteLine("Community URL in the format similar to https://lemmy.world/c/imaginarystarships or https://lemmy.dbzer0.com/c/imaginarystarships@lemmy.world is required");
    return;
}

var communityUrl = args[0];

var serverHost = communityUrl.Split('/')[2];
var communityName = communityUrl.Split('/')[4].Split('@')[0];

if (communityUrl.Contains("@"))
{
    serverHost = communityUrl.Split('/')[4].Split('@')[1];
}

//get the title of the webpage by visiting it
WebClient x = new WebClient();
string title = Regex.Match(x.DownloadString(communityUrl), @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;
var splitTitle = title.Split('-');
title = "";
for(int i = 0; i < splitTitle.Length-1; i++)
{
   title += splitTitle[i] + "-";
}
title = title.Trim('-');
title = title.Trim();
var markdown = $"**[{title}](/c/{communityName}@{serverHost})** | {serverHost} | [Kbin](/m/{communityName}@{serverHost}) | [lemmyverse.link](https://lemmyverse.link/c/{communityName}@{serverHost}) | ![](https://img.shields.io/lemmy/{communityName}@{serverHost}?style=flat&label=Subs&cacheSeconds=172800&color=pink)";

Console.WriteLine(markdown);

try
{
    ClipboardService.SetText(markdown);
}
catch
{
    Console.WriteLine("Failed to copy to clipboard");
}

try
{
    File.WriteAllText(title+".txt", markdown);
}
catch
{
    Console.WriteLine("Failed to write to file");
}
