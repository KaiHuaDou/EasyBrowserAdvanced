﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace 极简浏览器.Properties {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("极简浏览器.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   使用此强类型资源类，为所有资源查找
        ///   重写当前线程的 CurrentUICulture 属性。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 极简浏览器 的本地化字符串。
        /// </summary>
        public static string browserName {
            get {
                return ResourceManager.GetString("browserName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 您已被取消软件使用资格 的本地化字符串。
        /// </summary>
        public static string civiRefuse {
            get {
                return ResourceManager.GetString("civiRefuse", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 我认为您是不善意的，软件将强行关闭！ 的本地化字符串。
        /// </summary>
        public static string civiShutdown {
            get {
                return ResourceManager.GetString("civiShutdown", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 网络文明监察局 的本地化字符串。
        /// </summary>
        public static string civiTitle {
            get {
                return ResourceManager.GetString("civiTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 原因: 的本地化字符串。
        /// </summary>
        public static string exCause {
            get {
                return ResourceManager.GetString("exCause", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 方法引发了此异常。 的本地化字符串。
        /// </summary>
        public static string exEnd {
            get {
                return ResourceManager.GetString("exEnd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 帮助链接: 的本地化字符串。
        /// </summary>
        public static string exLink {
            get {
                return ResourceManager.GetString("exLink", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 很抱歉，程序发生了未处理的异常， 的本地化字符串。
        /// </summary>
        public static string exMain {
            get {
                return ResourceManager.GetString("exMain", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 程序集中的 的本地化字符串。
        /// </summary>
        public static string exPlace {
            get {
                return ResourceManager.GetString("exPlace", resourceCulture);
            }
        }
    }
}
