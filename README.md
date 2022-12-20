# 极简浏览器
![](https://img.shields.io/github/license/kaihuadou/easybrowseradvanced) ![](https://img.shields.io/github/v/release/kaihuadou/easybrowseradvanced) ![](https://img.shields.io/github/downloads/kaihuadou/easybrowseradvanced/total)
![](https://img.shields.io/github/repo-size/kaihuadou/easybrowseradvanced)  ![](https://img.shields.io/tokei/lines/github/kaihuadou/easybrowseradvanced)
![](https://img.shields.io/github/commit-activity/y/kaihuadou/easybrowseradvanced) ![](https://img.shields.io/github/last-commit/kaihuadou/easybrowseradvanced)

极简浏览器用 C# 编写的基于 Chromium 的浏览器。

适用与性能一般的电脑或者需要 Flash 的场景。

> 特别鸣谢 StrollStars 对运行依赖的改进建议

## 特点
1. 体积小巧，仅有 Chrome 的 29%，Firefox 的 63%。
2. 界面干净，无广告，无插件
3. 便于携带，无需安装，数据与应用程序储存在同样位置
4. 启动速度快
5. 支持 Flash，可以在不安装任何 Flash 插件的情况下运行 Flash
6. 开源代码，全部源代码都在 Github 和 Azure 上公开。

## 运行依赖

1. [.Net Framework 4.6.2](https://dotnet.microsoft.com/zh-cn/download/dotnet-framework/thank-you/net462-offline-installer)
2. [Visual C++ 运行时 2015](https://aka.ms/vs/17/release/vc_redist.x64.exe)

## 测试环境

+   可以使用浏览器进入[测试页](https://pinyin.sogou.com/help.php?list=8)测试 Flash 的可用性
+   可以使用浏览器进入[测试页](http://cachefly.cachefly.net/10mb.test)测试下载功能

## 版本更新

注：
1. 详细信息请参照 Commit
2. 每24个项更改为一个版本
3. 现在此文件是 UTF-8 了
4. 用斜体显示的是正在开发的

### 下一个更新（V 3.4.7.2)

#### 下一个RC（RC2）

+ *增加功能 #47 转换为单进程模式，提升性能*
+ *Bug修复 #39 降低 `MainWindow` 的耦合度* 
+ Bug修复 #48 为配置类启用了泛型
+ Bug修复 #47 利用`HashSet`代替`ObservableCollection`以缓解性能问题
+ 增加功能 #47 增加了储存读取 Cookies 的功能
+ Bug修复 #46 修复了下载 URL 显示不全的问题
+ 增加功能 #46 增加了有关许可证的帮助

#### RC1

+ 增加功能 #45 为错误页面的错误代码提供了中文翻译
+ Bug修复 #45 解决了多实例参数传递错误的问题
+ Bug修复 #44 解决了非网址搜索时缓慢的问题
+ Bug修复 #43 修复了状态条无法正确显示的问题
+ 更新资源 #6 更新了 LICENSE 文件
+ 框架升级 #5 删除了并不极简的`WPFMultiStyle`
+ 代码质量 #12 优化了`MainWindow`的布局
+ 增加功能 #44 删掉了并不极简的快捷键
+ Bug修复 #42 解决了设置编辑框显示不全的问题
+ Bug修复 #41 解决了下载时程序崩溃的 Bug
+ 代码质量 #11 优化了 Api 名称
+ 代码质量 #10 使用 `StackPanel`/`DockPanel` 优化界面布局
+ 增加功能 #43 对保存网页源代码的支持
+ 增加功能 #42 对 Windows 10 磁贴做了特别的支持
+ Bug修复 #40 修复了历史记录有时无法读取的问题
+ 增加功能 #41 更改了错误页面以及其地址的显示方式
+ 增加功能 #40 更改了加载进度条的样式
+ 增加功能 #38 更改了设置页面的布局和逻辑
+ Bug修复 #38 通过规则集保持代码质量
+ 增加功能 #23 使用了全新的 HTML 格式化引擎
+ Bug修复 #37 干掉了一点都不精简的扩展模块
+ Bug修复 #36 删除了被开发者工具替代的 `RunJavaScript` 模块
+ Bug修复 #35 修正了历史遗留问题若干
+ 框架升级 #4 升级到了 MaterialDesignColors 2.0.9
+ 框架升级 #3 升级到了 MaterialDesignThemes 4.6.1
+ 框架升级 #2 升级到了 .NET Framework 4.6.2

#### Beta 2

+ Bug修复 #34 更新了应用程序的颜色
+ 框架升级 #1 更新了 Material Design 的版本
+ Bug修复 #33 修复了对话框字体颜色偏灰的问题
+ Bug修复 #32 修复了菜单栏颜色字体不正确的问题
+ Bug修复 #31 修复了书签文件启动崩溃的问题
+ Bug修复 #30 全面应用 Material Design
+ Bug修复 #29 修复了下载完成后窗口崩溃的 Bug
+ 增加功能 #22 网页加载完成后不再显示”加载完成“字样
+ Bug修复 #28 修复了无法自动播放 Flash 的 Bug
+ Bug修复 #27 修复了加载完成后跳转到错误页面的 Bug

#### Beta 1

+ Bug修复 #26 更改了状态栏的样式
+ Bug修复 #25 更改了菜单的样式
+ Bug修复 #24 修复了语言为英文的问题
+ 增加功能 #21 重新添加了新版的 FLash 支持
+ Bug修复 #23 改进了可以直接跳转搜索的功能
+ Bug修复 #22 改进了帮助界面和关于界面
+ 更新资源 #5 更换了新的图标
+ 更新资源 #4 添加了 LICENSE 文件 (MIT)
+ Bug修复 #21 更改了地址栏的样式
+ 增加功能 #20 增加了导入以`easy://`开头的地址
+ 代码质量 #9 根据微软的建议进行第二轮修改
+ 代码质量 #8 根据微软的建议进行第一轮修改
+ 更新资源 #3 增加了清理垃圾和清除足迹的脚本
+ 更新资源 #2 修改主界面的四个图标
+ 代码质量 #7 整合`Api/ConfigHelper.cs`之书签部分
+ 代码质量 #6 整合`Api/ConfigHelper.cs`之历史记录部分
+ 增加功能 #10 增加下载窗口
+ 代码质量 #7 将部分函数改为属性
+ 增加功能 #18 执行 Javascript 窗口
+ Bug修复 #20 解决了下载时闪退的问题
+ Bug修复 #19 删除了设置窗口的空白区域

### V 3.3.1.11更新

+ 增加功能 #17 更好的反馈窗口
+ Bug修复 #18 权限不足导致的无法启动
+ Bug修复 #17 解决了加载错误导致的浏览中断
+ 增加功能 #16 更好的加载错误显示
+ 增加功能 #15 地址栏支持搜索
+ 增加功能 #14 将状态栏调至透明
+ 增加功能 #13 更新图标
+ 增加功能 #12 将主页颜色调换至白色
+ Bug修复 #16 修复了日志文件缺失无法自动恢复的问题
+ Bug修复 #15 修复了无痕模式不起效的问题
+ 性能优化 #2 精简了构造函数和`FileApi.cs`的代码
+ Bug修复 #14 修复了部分情况无法显示主页的问题
+ 性能优化 #1 优化了启动参数的传递
+ Bug修复 #13 修复了开发者工具导致崩溃的问题
+ 增加功能 #11 增加了开发者工具
+ 增加功能 #10 增加了清除缓存
+ Bug修复 #12 修复了全选失败的问题
+ 资源更新 #1 更新图标
+ 代码质量 #6 整合运行时数据
+ Bug修复 #11 修复了`about:blank`没有标题
+ 增加功能 #9 增加了无痕模式(由`@要饭的肥喵`提出建议)
+ 增加功能 #8 增加了在单窗口打开的功能

### V 3.2.1.11更新

+ 代码质量 #5 整合`Api/StandardApi.cs`
+ 增加功能 #7 格式化网页源代码时支持 CSS
+ 增加功能 #6 增加更多日志类型
+ 增加功能 #5 增加网页源代码格式化的功能
+ 增加功能 #4 加入全面日志的记录
+ 代码质量 #4 整合`Api/CivilizedLanguage.cs`
+ 代码质量 #3 整合`Api/Logger.cs`
+ 代码质量 #2 整合`Api/FilePath.cs`
+ 增加功能 #3 历史记录和书签可支持日期和标题
+ 代码质量 #1 将可复用代码统一进入 Api 文件夹
+ 启动任务 #1 启动代码优化流程
+ 增加功能 #2 添加了安装程序
+ Bug修复 #10修复了右键菜单打开无法关闭分问题
+ Bug修复 #9 修复了状态栏隐藏无效的问题
+ Bug修复 #8 更新了项目命名至“极简浏览器”
+ 功能恢复 #1 恢复了网页源代码的查看和刷新
+ 增加功能 #1 增加了下载文件的功能
+ Bug修复 #7 修复了无法下载文件的错误
+ Bug修复 #6 修复了历史记录删不掉的错误
+ Bug修复 #5 修复了输入流氓语言不自动宕机的错误
+ Waring修复 #3 修复了函数内部代码冗余的问题
+ Bug修复 #4 修复了快捷键失效的错误
+ Waring修复 #2 修复了`History.xaml.cs`的函数名不规范
+ Waring修复 #1 修复了`FileApi.cs`的代码质量问题

### V 3.0.0.0更新

+ Bug修复 #3 修复了写入日志错误
+ Bug修复 #2 修复了无法删除历史记录&书签的错误
+ Bug修复 #1 修复了无法清除历史记录&书签的错误
