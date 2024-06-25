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
title = title.Split('-')[0].Trim();

var markdown = $"**[{title}](/c/{communityName}@{serverHost})** | {serverHost} | [Kbin](/m/{communityName}@{serverHost}) | [lemmyverse.link](https://lemmyverse.link/c/{communityName}@{serverHost}) | ![](https://img.shields.io/lemmy/{communityName}@{serverHost}?style=flat&label=Subs&color=pink)";

Console.WriteLine(markdown);
ClipboardService.SetText(markdown);