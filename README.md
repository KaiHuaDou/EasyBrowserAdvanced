# 极简浏览器

![][Shields CI] ![][Shields License]
![][Shields Release] ![][Shields Downloads]
![][Shields Lines] ![][Shields Commit]

[![Gitmoji][Gitmoji]](https://gitmoji.dev)

| 中文  | [English](./docs/README.en-US.md) |
| :---: | :-------------------------------: |

极简浏览器用 C# 编写的基于 Chromium 的浏览器。

适用与性能一般的电脑或者需要 Flash 的场景。

建议访问更新更快的 [GitHub 仓库](https://github.com/KaiHuaDou/EasyBrowserAdvanced)。

> 特别鸣谢 StrollStars 对运行依赖的改进建议

## 特点

1. 体积小巧，仅有 Chrome 的 29%，Firefox 的 63%。
2. 界面干净，无广告，无插件
3. 便携版，无需安装，数据与应用程序储存在同样位置
4. 启动速度快
5. 支持 Flash，可以在不安装任何 Flash 插件的情况下运行 Flash
6. 开源代码，全部源代码都在 GitHub 和 Azure DevOps 上公开。

## 运行依赖

1. [.NET Framework 4.6.2 运行时](https://dotnet.microsoft.com/zh-cn/download/dotnet-framework/thank-you/net462-offline-installer)
2. [Visual C++ 2022 运行时](https://aka.ms/vs/17/release/vc_redist.x64.exe)

## 测试环境

+ 可以使用浏览器进入[测试页](https://pinyin.sogou.com/help.php?list=8)测试 Flash 的可用性
+ 可以使用浏览器进入[测试页](http://cachefly.cachefly.net/10mb.test)测试下载功能

## 版本说明

版本详细功能请参见[更新日志](./CHANGELOG.md)

|  通道  |           说明           |  下一个版本  |
| :----: | :----------------------: | :----------: |
| Stable |       相对稳定版本       |   v3.4.7.2   |
|  Beta  |      新功能测试版本      | v3.7.2-beta1 |
|   RC   | 总体完善但细节不足的版本 | v3.4.7.2-rc2 |

## 开发环境

1. Visual Studio 2022 (Community 即可, 64-bit)
    + .NET 桌面开发
        + NuGet 目标和生成任务
        + MSBuild

2. .NET Framework 4.6.2 或更高版本的 SDK
3. .NET Framework 4.6.2 目标包 (`Target Pack`)

使用 `dotnet build` 以自主构建

[Shields CI]: https://img.shields.io/github/actions/workflow/status/kaihuadou/easybrowseradvanced/build.yml
[Shields License]: https://img.shields.io/github/license/kaihuadou/easybrowseradvanced
[Shields Release]: https://img.shields.io/github/v/release/kaihuadou/easybrowseradvanced
[Shields Downloads]: https://img.shields.io/github/downloads/kaihuadou/easybrowseradvanced/total
[Shields Lines]: https://img.shields.io/tokei/lines/github/kaihuadou/easybrowseradvanced
[Shields Commit]: https://img.shields.io/github/commit-activity/y/kaihuadou/easybrowseradvanced
[Gitmoji]: https://img.shields.io/badge/gitmoji-%20😜%20😍-FFDD67.svg
