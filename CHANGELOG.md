# 版本更新日志

> 1. 详细信息请参照 [Commit]
> 2. 用斜体显示的是正在开发的

## 3.7.2 (规划内)

### Beta 1

## 3.4.7.2 (开发中)

### RC3w

### RC2

+ :building_construction: 将窗口参数由全局更改至每个窗口
+ :sparkles: 可选伪装 User-Agent 为新版操作系统
+ :bug: 修复设置空主页或搜索引擎导致无法使用的错误
+ :bug: 修复导致下载失败的错误并改进进度 UI
+ :sparkles: 重构用户数据模块
+ :zap: 改进 XML 处理
+ :fire: 删除重复的初始化代码
+ :bug: 降低 `MainWindow` 的耦合度
+ :coffin: 删除历史遗留的代码
+ :bug: 修复多窗口导致崩溃的错误
+ :sparkles: 采用更好的关于窗口
+ :zap: 更新 Visual Studio 以提升编译性能
+ :bug: 修复源代码格式未完成导致崩溃的错误
+ :fire: 删除 MaterialDesign
+ :sparkles: 缩放级别显示
+ :zap: 转换为单进程模式
+ :sparkles: 通过`Ctrl`快捷键进入窗口的功能
+ :bug: 为配置类启用泛型
+ :sparkles: 储存读取 Cookies 的功能
+ :bug: 修复下载 URL 显示不全的错误
+ :sparkles: 有关许可证的帮助

### RC1

+ :children_crossing: 为错误页面的错误代码提供中文翻译
+ :bug: 解决多实例参数传递错误的错误
+ :bug: 解决非网址搜索时缓慢的错误
+ :bug: 修复状态条无法正确显示的错误
+ :page_facing_up: 更新 LICENSE 文件
+ :heavy_minus_sign: 删除无用的`WPFMultiStyle`
+ :recycle: 优化`MainWindow`的布局
+ :sparkles: 删掉无用的快捷键
+ :bug: 解决设置编辑框显示不全的错误
+ :bug: 解决下载时程序崩溃的 Bug
+ :recycle: 优化 Api 名称
+ :recycle: 优化界面布局
+ :sparkles: 保存网页源代码
+ :sparkles: 对 Windows 10 磁贴做特别的支持
+ :bug: 修复历史记录有时无法读取的错误
+ :sparkles: 更改错误页面以及其地址的显示方式
+ :lipstick: 更改加载进度条的样式
+ :sparkles: 更改设置页面的布局和逻辑
+ :bug: 通过规则集保持代码质量
+ :sparkles: 使用全新的 HTML 格式化引擎
+ :fire: 删除扩展模块
+ :fire: 删除被开发者工具替代的 `RunJavaScript` 模块
+ :bug: 修正历史遗留错误若干
+ :arrow_up: 升级到 MaterialDesignColors 2.0.9
+ :arrow_up: 升级到 MaterialDesignThemes 4.6.1
+ :arrow_up: 升级到 .NET Framework 4.6.2

### Beta2

+ :art: 更新应用程序的颜色
+ :arrow_up: 更新 MaterialDesign 的版本
+ :bug: 修复对话框字体颜色偏灰的错误
+ :bug: 修复菜单栏颜色字体不正确的错误
+ :bug: 修复书签文件启动崩溃的错误
+ :lipstick: 应用 MaterialDesign 样式
+ :bug: 修复下载完成后窗口崩溃的 Bug
+ :sparkles: 网页加载完成后不再显示"加载完成"字样
+ :bug: 修复无法自动播放 Flash 的 Bug
+ :bug: 修复加载完成后跳转到错误页面的 Bug

### Beta1

+ :lipstick: 更改状态栏的样式
+ :lipstick: 更改菜单的样式
+ :bug: 修复语言为英文的错误
+ :sparkles: 重新添加新版的 FLash 支持
+ :bug: 改进可以直接跳转搜索的功能
+ :children_crossing: 改进帮助界面和关于界面
+ :bento: 更换新的图标
+ :page_facing_up: 添加 LICENSE 文件 (MIT)
+ :lipstick: 更改地址栏的样式
+ :sparkles: 支持以`easy://`开头的地址
+ :rotating_light: 根据规则集修复了安全问题
+ :rotating_light: 根据规则集修复了语法错误
+ :bento: 清理垃圾和清除足迹的脚本
+ :bento: 修改主界面的四个图标
+ :art: 整合`Api/ConfigHelper.cs`之书签部分
+ :art: 整合`Api/ConfigHelper.cs`之历史记录部分
+ :sparkles: 下载窗口
+ :art: 将部分函数改为属性
+ :sparkles: 执行 Javascript 窗口
+ :bug: 解决下载时闪退的错误
+ :bug: 删除设置窗口的空白区域

## V3.3.1.11

+ :sparkles: 更好的反馈窗口
+ :bug: 权限不足导致的无法启动
+ :bug: 解决加载错误导致的浏览中断
+ :children_crossing: 更好的加载错误显示
+ :sparkles: 地址栏支持搜索
+ :lipstick: 将状态栏调至透明
+ :sparkles: 更新图标
+ :lipstick: 将主页颜色调换至白色
+ :bug: 修复日志文件缺失无法自动恢复的错误
+ :bug: 修复无痕模式不起效的错误
+ :zap: 精简构造函数和`FileApi.cs`的代码
+ :bug: 修复部分情况无法显示主页的错误
+ :zap: 优化启动参数的传递
+ :bug: 修复开发者工具导致崩溃的错误
+ :sparkles: 开发者工具
+ :sparkles: 清除缓存
+ :bug: 修复历史记录全选失败的错误
+ :bento: 优化图标的显示
+ :recycle: 整合运行时数据
+ :bug: 修复`about:blank`没有标题
+ :sparkles: 无痕模式(由`@要饭的肥喵`提出建议)
+ :sparkles: 在本窗口打开的功能

## V3.2.1.11

+ :art: 整合`Api/StandardApi.cs`
+ :sparkles: 格式化网页源代码时支持 CSS
+ :sparkles: 更多日志类型
+ :sparkles: 网页源代码格式化的功能
+ :sparkles: 加入全面日志的记录
+ :art: 整合`Api/CivilizedLanguage.cs`
+ :art: 整合`Api/Logger.cs`
+ :art: 整合`Api/FilePath.cs`
+ :sparkles: 历史记录和书签可支持日期和标题
+ :art: 将可复用代码统一进入 Api 文件夹
+ :sparkles: 添加安装程序
+ :bug: 修复右键菜单打开无法关闭的错误
+ :bug: 修复状态栏隐藏无效的错误
+ :bug: 更新项目命名至"极简浏览器"
+ :sparkles: 下载文件的功能
+ :bug: 修复无法下载文件的错误
+ :bug: 修复历史记录删不掉的错误
+ :bug: 修复输入流氓语言不自动宕机的错误
+ :recycle: 修复函数内部代码冗余的问题
+ :bug: 修复快捷键失效的错误
+ :art: 修复`History.xaml.cs`的函数名不规范
+ :recycle: 修复`FileApi.cs`的代码质量问题

## V3.0.0.0

+ :bug: 修复写入日志错误
+ :bug: 修复无法删除历史记录和书签的错误
+ :bug: 修复无法清空历史记录和书签的错误

[Commit]: https://github.com/KaiHuaDou/EasyBrowserAdvanced/commits/main/
