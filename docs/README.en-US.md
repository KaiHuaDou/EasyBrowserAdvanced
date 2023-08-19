# EasyBrowser

** This branch is no-flash version，using the higher version core of  Chromium**

![][Shields CI] ![][Shields License]
![][Shields Release] ![][Shields Downloads]
![][Shields Lines] ![][Shields Commit]

[![Gitmoji][Gitmoji]](https://gitmoji.dev)

| [中文](../README.md) | English |
| :------------------: | :-----: |

EasyBrowser Chromium-based browser written in C#.

Applicable to computers with average performance.

It is recommended to visit the updated and faster [GitHub repository](https://github.com/KaiHuaDou/EasyBrowserAdvanced).

> Special thanks to StrollStars for suggestions for improving runtime dependencies

## Features

1. Small in size, only 29% of Chrome and 63% of Firefox.
2. Clean interface, no ads, no plug-ins
3. Portable version, no installation required, data and applications are stored in the same location
4. Fast startup
5. Open source code, all source code is public on GitHub and Azure DevOps.

## Running dependencies

1. [.NET Framework 4.6.2 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet-framework/thank-you/net462-offline-installer)
2. [Visual C++ 2022 Runtime](https://aka.ms/vs/17/release/vc_redist.x64.exe)
3. [Segoe Fluent Icons Font](https://aka.ms/SegoeFluentIcons)

## Test Environment

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

Use `dotnet build` to build autonomously

[Shields CI]: https://img.shields.io/github/actions/workflow/status/kaihuadou/easybrowseradvanced/build.yml
[Shields License]: https://img.shields.io/github/license/kaihuadou/easybrowseradvanced
[Shields Release]: https://img.shields.io/github/v/release/kaihuadou/easybrowseradvanced
[Shields Downloads]: https://img.shields.io/github/downloads/kaihuadou/easybrowseradvanced/total
[Shields Lines]: https://img.shields.io/tokei/lines/github/kaihuadou/easybrowseradvanced
[Shields Commit]: https://img.shields.io/github/commit-activity/y/kaihuadou/easybrowseradvanced
[Gitmoji]: https://img.shields.io/badge/gitmoji-%20😜%20😍-FFDD67.svg
