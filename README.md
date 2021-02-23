# 极简浏览器
极简浏览器是基于Chromium的浏览器，轻巧易用，启动速度快。

## 版本更新
注：详细信息请参照Commit
+ FeatureAdd #7 格式化网页源代码时支持CSS
+ FeatureAdd #6 增加更多日志类型
+ FeatureAdd #5 增加网页源代码格式化的功能
+ FeatureAdd #4 加入全面日志的记录
+ CodeQuality #4 整合Api/CivilizedLanguage.cs
+ CodeQuality #3 整合Api/Logger.cs
+ CodeQuality #2 整合Api/FilePath.cs
+ FeatureAdd #3 历史记录和书签可支持日期和标题
+ CodeQuality #1 将可复用代码统一进入Api文件夹
+ TaskStart #1 启动代码优化流程
+ FeatureAdd #2 添加了安装程序
+ BugFix #10修复了右键菜单打开无法关闭分问题
+ BugFix #9 修复了状态栏隐藏无效的问题
+ BugFix #8 更新了项目命名至“极简浏览器”
+ FeatureRestore #1 恢复了网页源代码的查看和刷新
+ FeatureAdd #1 增加了下载文件的功能
+ BugFix #7 修复了无法下载文件的错误
+ BugFix #6 修复了历史记录删不掉的错误
+ BugFix #5 修复了输入流氓语言不自动宕机的错误
+ WaringFix #3 修复了函数内部代码冗余的问题
+ BugFix #4 修复了快捷键失效的错误
+ WaringFix #2 修复了History.xaml.cs的函数名不规范
+ WaringFix #1 修复了FileApi.cs的代码质量问题
+ BugFix #3 修复了写入日志错误
+ BugFix #2 修复了无法删除历史记录&书签的错误
+ BugFix #1 修复了无法清除历史记录&书签的错误

## 内核更换计划（已成功）

1. 创建极简浏览器的副本
（在副本上）
2. 删除全部后端
3. 更换内核
4. 重写后端
8. 进行黑盒测试
9. 发布到GitHub