# 极简浏览器
![](https://img.shields.io/github/license/kaihuadou/easybrowseradvanced) ![](https://img.shields.io/github/v/release/kaihuadou/easybrowseradvanced) ![](https://img.shields.io/github/downloads/kaihuadou/easybrowseradvanced/total)
![](https://img.shields.io/github/repo-size/kaihuadou/easybrowseradvanced)  ![](https://img.shields.io/tokei/lines/github/kaihuadou/easybrowseradvanced)
![](https://img.shields.io/github/commit-activity/y/kaihuadou/easybrowseradvanced) ![](https://img.shields.io/github/last-commit/kaihuadou/easybrowseradvanced)

极简浏览器用 C# 编写的基于 Chromium 的浏览器。

适用与性能一般的电脑或者需要 Flash 的场景。

[GitHub 储存库](https://github.com/KaiHuaDou/EasyBrowserAdvanced)更新快，[Azure 储存库](https://dev.azure.com/qpsh/%E6%9E%81%E7%AE%80%E6%B5%8F%E8%A7%88%E5%99%A8)更新慢，建议访问 [GitHub 储存库](https://github.com/KaiHuaDou/EasyBrowserAdvanced)。

> 特别鸣谢 StrollStars 对运行依赖的改进建议

## 特点
1. 体积小巧，仅有 Chrome 的 29%，Firefox 的 63%。
2. 界面干净，无广告，无插件
3. 便于携带，无需安装，数据与应用程序储存在同样位置
4. 启动速度快
5. 支持 Flash，可以在不安装任何 Flash 插件的情况下运行 Flash
6. 开源代码，全部源代码都在 Github 和 Azure 上公开。

## 运行依赖

1. [.NET Framework 4.6.2](https://dotnet.microsoft.com/zh-cn/download/dotnet-framework/thank-you/net462-offline-installer)
2. [Visual C++ 运行时 2015](https://aka.ms/vs/17/release/vc_redist.x64.exe)

## 测试环境

+ 可以使用浏览器进入[测试页](https://pinyin.sogou.com/help.php?list=8)测试 Flash 的可用性
+ 可以使用浏览器进入[测试页](http://cachefly.cachefly.net/10mb.test)测试下载功能

## 开发环境

### IDE

1. Visual Studio 2015 Update 3，全选 Visual C# 工作负载。
2. .NET Framework 4.6.2 SDK

### NuGet 包

|      NuGet 包名      |   版本    |        备注        |
| :------------------: | :-------: | :----------------: |
|    cef.redist.x64    | v87.1.13  | 高版本不支持 Flash |
|    cef.redist.x86    | v87.1.13  | 高版本不支持 Flash |
|   CefSharp.Common    | v87.1.132 | 高版本不支持 Flash |
|     CefSharp.Wpf     | v87.1.132 | 高版本不支持 Flash |

## 版本更新

注：
1. 详细信息请参照 Commit
2. 每24个项更改为一个版本
3. 现在此文件是 UTF-8 了
4. 用斜体显示的是正在开发的

### 下一个更新（V 3.4.7.2)

#### 下一个RC（RC2）

+ *BugFix #39 降低 `MainWindow` 的耦合度* 
+ Feature #49 增加了缩放级别显示
+ Performance #3 转换为单进程模式，提升性能
+ Feature #48 增加通过`Ctrl`快捷键进入窗口的功能
+ BugFix #48 为配置类启用了泛型
+ BugFix #47 利用`HashSet`代替`ObservableCollection`以缓解性能问题
+ Feature #47 增加了储存读取 Cookies 的功能
+ BugFix #46 修复了下载 URL 显示不全的问题
+ Feature #46 增加了有关许可证的帮助

#### RC1

+ Feature #45 为错误页面的错误代码提供了中文翻译
+ BugFix #45 解决了多实例参数传递错误的问题
+ BugFix #44 解决了非网址搜索时缓慢的问题
+ BugFix #43 修复了状态条无法正确显示的问题
+ Resource #6 更新了 LICENSE 文件
+ Framework #5 删除了并不极简的`WPFMultiStyle`
+ Quality #12 优化了`MainWindow`的布局
+ Feature #44 删掉了并不极简的快捷键
+ BugFix #42 解决了设置编辑框显示不全的问题
+ BugFix #41 解决了下载时程序崩溃的 Bug
+ Quality #11 优化了 Api 名称
+ Quality #10 使用 `StackPanel`/`DockPanel` 优化界面布局
+ Feature #43 对保存网页源代码的支持
+ Feature #42 对 Windows 10 磁贴做了特别的支持
+ BugFix #40 修复了历史记录有时无法读取的问题
+ Feature #41 更改了错误页面以及其地址的显示方式
+ Feature #40 更改了加载进度条的样式
+ Feature #38 更改了设置页面的布局和逻辑
+ BugFix #38 通过规则集保持Quality
+ Feature #23 使用了全新的 HTML 格式化引擎
+ BugFix #37 干掉了一点都不精简的扩展模块
+ BugFix #36 删除了被开发者工具替代的 `RunJavaScript` 模块
+ BugFix #35 修正了历史遗留问题若干
+ Framework #4 升级到了 MaterialDesignColors 2.0.9
+ Framework #3 升级到了 MaterialDesignThemes 4.6.1
+ Framework #2 升级到了 .NET Framework 4.6.2

#### Beta 2

+ BugFix #34 更新了应用程序的颜色
+ Framework #1 更新了 MaterialDesign 的版本
+ BugFix #33 修复了对话框字体颜色偏灰的问题
+ BugFix #32 修复了菜单栏颜色字体不正确的问题
+ BugFix #31 修复了书签文件启动崩溃的问题
+ BugFix #30 全面应用 MaterialDesign
+ BugFix #29 修复了下载完成后窗口崩溃的 Bug
+ Feature #22 网页加载完成后不再显示”加载完成“字样
+ BugFix #28 修复了无法自动播放 Flash 的 Bug
+ BugFix #27 修复了加载完成后跳转到错误页面的 Bug

#### Beta 1

+ BugFix #26 更改了状态栏的样式
+ BugFix #25 更改了菜单的样式
+ BugFix #24 修复了语言为英文的问题
+ Feature #21 重新添加了新版的 FLash 支持
+ BugFix #23 改进了可以直接跳转搜索的功能
+ BugFix #22 改进了帮助界面和关于界面
+ Resource #5 更换了新的图标
+ Resource #4 添加了 LICENSE 文件 (MIT)
+ BugFix #21 更改了地址栏的样式
+ Feature #20 增加了导入以`easy://`开头的地址
+ Quality #14 根据微软的建议进行第二轮修改
+ Quality #13 根据微软的建议进行第一轮修改
+ Resource #3 增加了清理垃圾和清除足迹的脚本
+ Resource #2 修改主界面的四个图标
+ Quality #12 整合`Api/ConfigHelper.cs`之书签部分
+ Quality #11 整合`Api/ConfigHelper.cs`之历史记录部分
+ Feature #10 增加下载窗口
+ Quality #10 将部分函数改为属性
+ Feature #18 执行 Javascript 窗口
+ BugFix #20 解决了下载时闪退的问题
+ BugFix #19 删除了设置窗口的空白区域

### V 3.3.1.11更新

+ Feature #17 更好的反馈窗口
+ BugFix #18 权限不足导致的无法启动
+ BugFix #17 解决了加载错误导致的浏览中断
+ Feature #16 更好的加载错误显示
+ Feature #15 地址栏支持搜索
+ Feature #14 将状态栏调至透明
+ Feature #13 更新图标
+ Feature #12 将主页颜色调换至白色
+ BugFix #16 修复了日志文件缺失无法自动恢复的问题
+ BugFix #15 修复了无痕模式不起效的问题
+ Performance #2 精简了构造函数和`FileApi.cs`的代码
+ BugFix #14 修复了部分情况无法显示主页的问题
+ Performance #1 优化了启动参数的传递
+ BugFix #13 修复了开发者工具导致崩溃的问题
+ Feature #11 增加了开发者工具
+ Feature #10 增加了清除缓存
+ BugFix #12 修复了历史记录全选失败的问题
+ Resource #1 优化了图标的显示
+ Quality #9 整合运行时数据
+ BugFix #11 修复了`about:blank`没有标题
+ Feature #9 增加了无痕模式(由`@要饭的肥喵`提出建议)
+ Feature #8 增加了在单窗口打开的功能

### V 3.2.1.11更新

+ Quality #8 整合`Api/StandardApi.cs`
+ Feature #7 格式化网页源代码时支持 CSS
+ Feature #6 增加更多日志类型
+ Feature #5 增加网页源代码格式化的功能
+ Feature #4 加入全面日志的记录
+ Quality #7 整合`Api/CivilizedLanguage.cs`
+ Quality #6 整合`Api/Logger.cs`
+ Quality #5 整合`Api/FilePath.cs`
+ Feature #3 历史记录和书签可支持日期和标题
+ Quality #4 将可复用代码统一进入 Api 文件夹
+ Feature #2 添加了安装程序
+ BugFix #10修复了右键菜单打开无法关闭分问题
+ BugFix #9 修复了状态栏隐藏无效的问题
+ BugFix #8 更新了项目命名至“极简浏览器”
+ Feature #1 增加了下载文件的功能
+ BugFix #7 修复了无法下载文件的错误
+ BugFix #6 修复了历史记录删不掉的错误
+ BugFix #5 修复了输入流氓语言不自动宕机的错误
+ Quality #3 修复了函数内部代码冗余的问题
+ BugFix #4 修复了快捷键失效的错误
+ Quality #2 修复了`History.xaml.cs`的函数名不规范
+ Quality #1 修复了`FileApi.cs`的Quality问题

### V 3.0.0.0更新

+ BugFix #3 修复了写入日志错误
+ BugFix #2 修复了无法删除历史记录&书签的错误
+ BugFix #1 修复了无法清除历史记录&书签的错误
