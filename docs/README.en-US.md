# EasyBrowser

![][Shields License] ![][Shields Release] ![][Shields Downloads] ![][Shields Commit] ![][Shields CI]

![][Shields CSharp] ![][Shields .NET] ![][Shields OS] ![][Shields IDE] ![][Shields Chromium]

[![Gitmoji][Gitmoji]](https://gitmoji.dev)

| [中文](../README.md) | English |
| :------------------: | :-----: |

EasyBrowser Chromium-based browser written in C#.

Applicable to computers with average performance or scenes that require Flash.

It is recommended to visit the updated and faster [GitHub repository](https://github.com/KaiHuaDou/EasyBrowserAdvanced).

> Special thanks to StrollStars for suggestions for improving runtime dependencies

## Features

1. Small in size, only 29% of Chrome and 63% of Firefox.
2. Clean interface, no ads, no plug-ins
3. Portable version, no installation required, data and applications are stored in the same location
4. Fast startup
5. Support Flash, you can run Flash without installing any Flash plug-ins
6. Open source code, all source code is public on GitHub and Azure DevOps.

## Running dependencies

1. [.NET Framework 4.6.2 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet-framework/thank-you/net462-offline-installer)
2. [Visual C++ 2022 Runtime](https://aka.ms/vs/17/release/vc_redist.x64.exe)
3. [Segoe Fluent Icons Font](https://aka.ms/SegoeFluentIcons)

## Test Environment

+ You can use a browser to enter the [test page](https://pinyin.sogou.com/help.php?list=8) to test the usability of Flash
+ You can use a browser to enter the [test page](http://cachefly.cachefly.net/10mb.test) to test the download function

## Release Notes

Please refer to [Change Log](./CHANGELOG.md) for version details

| Channel |                     Description                     | Next Release |
| :-----: | :-------------------------------------------------: | :----------: |
| Stable  |              relatively stable version              |   v3.4.7.2   |
|  Beta   |              new feature test version               | v3.7.2-beta1 |
|   RC    | version that is generally perfect but lacks details | v3.4.7.2-rc3 |

## Development Environment

1. Visual Studio 2022 (Community is ok, 64-bit)
     + .NET desktop development
         + NuGet targets and build tasks
         + MSBuild

2. .NET Framework 4.6.2 or later SDK
3. .NET Framework 4.6.2 Target Pack

Use `dotnet build` to build independently. To build a version that supports video, you need to download [Cef H.264 Library], unzip it and place it in the source code root directory.

[Shields CI]: https://img.shields.io/github/actions/workflow/status/kaihuadou/easybrowseradvanced/build.yml
[Shields License]: https://img.shields.io/github/license/kaihuadou/easybrowseradvanced
[Shields Release]: https://img.shields.io/github/v/release/kaihuadou/easybrowseradvanced
[Shields Downloads]: https://img.shields.io/github/downloads/kaihuadou/easybrowseradvanced/total
[Shields Commit]: https://img.shields.io/github/commit-activity/y/kaihuadou/easybrowseradvanced
[Shields CSharp]: https://img.shields.io/badge/11.0-version?logo=csharp&label=C%23&color=%23512BD4
[Shields .NET]: https://img.shields.io/badge/>=4.6.2-version?logo=dotnet&label=.NET%20Framework&color=%23512BD4
[Shields OS]: https://img.shields.io/badge/>=Windows%207%20SP1-version?logo=windows&label=OS&color=%230078D4
[Shields IDE]: https://img.shields.io/badge/2022-version?logo=visual%20studio&label=Visual%20Studio&color=%235C2D91
[Shields Chromium]: https://img.shields.io/badge/87.1.132-version?logo=googlechrome&label=Chromium&color=%234285F4%logoColor=white
[Gitmoji]: https://img.shields.io/badge/gitmoji-%20😜%20😍-FFDD67.svg
[Cef H.264 Library]: https://github.com/KaiHuaDou/EasyBrowserAdvanced/releases/download/v3.4.7.2-h264test/cef-h264-library.zip
