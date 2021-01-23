using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Globalization;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using System.Reflection;

namespace 极简浏览器Cef
{
    public partial class WebBrowserHelper
    {
        private WebBrowser _webBrowser;
        private object _cookie;
        public event CancelEventHandler NewWindow;
        public WebBrowserHelper(WebBrowser webBrowser)
        {
            if (webBrowser == null)
                throw new ArgumentNullException("webBrowser");
            _webBrowser = webBrowser;
            _webBrowser.Dispatcher.BeginInvoke(new Action(Attach), DispatcherPriority.Loaded);
        }
        public void Disconnect( )
        {
            if (_cookie != null)
            {
                _cookie.ReflectInvokeMethod("Disconnect", new Type[] { }, null);
                _cookie = null;
            }
        }
        private void Attach( )
        {
            var axIWebBrowser2 = _webBrowser.ReflectGetProperty("AxIWebBrowser2");
            var webBrowserEvent = new WebBrowserEvent(this);
            var cookieType = typeof(WebBrowser).Assembly.GetType("MS.Internal.Controls.ConnectionPointCookie");
            _cookie = Activator.CreateInstance(
                cookieType,
                ReflectionService.BindingFlags,
                null,
                new[] { axIWebBrowser2, webBrowserEvent, typeof(DWebBrowserEvents2) },
                CultureInfo.CurrentUICulture);
        }
        private void OnNewWindow(ref bool cancel)
        {
            var eventArgs = new CancelEventArgs(cancel);
            if (NewWindow != null)
            {
                NewWindow(_webBrowser, eventArgs);
                cancel = eventArgs.Cancel;
            }
        }
    }
    public static class ReflectionService
    {
        public readonly static BindingFlags BindingFlags =
            BindingFlags.Instance |
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.FlattenHierarchy |
            BindingFlags.CreateInstance;
        public static object ReflectGetProperty(this object target, string propertyName)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentException("propertyName can not be null or whitespace", "propertyName");
            var propertyInfo = target.GetType( ).GetProperty(propertyName, BindingFlags);
            if (propertyInfo == null)
                throw new ArgumentException(string.Format("Can not find property '{0}' on '{1}'", propertyName, target.GetType( )));
            return propertyInfo.GetValue(target, null);
        }
        public static object ReflectInvokeMethod(this object target, string methodName, Type[] argTypes, object[] parameters)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (string.IsNullOrWhiteSpace(methodName))
                throw new ArgumentException("methodName can not be null or whitespace", "methodName");
            var methodInfo = target.GetType( ).GetMethod(methodName, BindingFlags, null, argTypes, null);
            if (methodInfo == null)
                throw new ArgumentException(string.Format("Can not find method '{0}' on '{1}'", methodName, target.GetType( )));
            return methodInfo.Invoke(target, parameters);
        }
    }
    [ComImport, TypeLibType(TypeLibTypeFlags.FHidden), InterfaceType(ComInterfaceType.InterfaceIsIDispatch), Guid("34A715A0-6587-11D0-924A-0020AFC7AC4D")]
    public interface DWebBrowserEvents2
    {
        [DispId(0x66)]
        void StatusTextChange([In] string text);
        [DispId(0x6c)]
        void ProgressChange([In] int progress, [In] int progressMax);
        [DispId(0x69)]
        void CommandStateChange([In] long command, [In] bool enable);
        [DispId(0x6a)]
        void DownloadBegin( );
        [DispId(0x68)]
        void DownloadComplete( );
        [DispId(0x71)]
        void TitleChange([In] string text);
        [DispId(0x70)]
        void PropertyChange([In] string szProperty);
        [DispId(250)]
        void BeforeNavigate2([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp, [In] ref object URL, [In] ref object flags, [In] ref object targetFrameName, [In] ref object postData, [In] ref object headers, [In, Out] ref bool cancel);
        [DispId(0xfb)]
        void NewWindow2([In, Out, MarshalAs(UnmanagedType.IDispatch)] ref object pDisp, [In, Out] ref bool cancel);
        [DispId(0xfc)]
        void NavigateComplete2([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp, [In] ref object URL);
        [DispId(0x103)]
        void DocumentComplete([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp, [In] ref object URL);
        [DispId(0xfd)]
        void OnQuit( );
        [DispId(0xfe)]
        void OnVisible([In] bool visible);
        [DispId(0xff)]
        void OnToolBar([In] bool toolBar);
        [DispId(0x100)]
        void OnMenuBar([In] bool menuBar);
        [DispId(0x101)]
        void OnStatusBar([In] bool statusBar);
        [DispId(0x102)]
        void OnFullScreen([In] bool fullScreen);
        [DispId(260)]
        void OnTheaterMode([In] bool theaterMode);
        [DispId(0x106)]
        void WindowSetResizable([In] bool resizable);
        [DispId(0x108)]
        void WindowSetLeft([In] int left);
        [DispId(0x109)]
        void WindowSetTop([In] int top);
        [DispId(0x10a)]
        void WindowSetWidth([In] int width);
        [DispId(0x10b)]
        void WindowSetHeight([In] int height);
        [DispId(0x107)]
        void WindowClosing([In] bool isChildWindow, [In, Out] ref bool cancel);
        [DispId(0x10c)]
        void ClientToHostWindow([In, Out] ref long cx, [In, Out] ref long cy);
        [DispId(0x10d)]
        void SetSecureLockIcon([In] int secureLockIcon);
        [DispId(270)]
        void FileDownload([In, Out] ref bool cancel);
        [DispId(0x10f)]
        void NavigateError([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp, [In] ref object URL, [In] ref object frame, [In] ref object statusCode, [In, Out] ref bool cancel);
        [DispId(0xe1)]
        void PrintTemplateInstantiation([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp);
        [DispId(0xe2)]
        void PrintTemplateTeardown([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp);
        [DispId(0xe3)]
        void UpdatePageStatus([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp, [In] ref object nPage, [In] ref object fDone);
        [DispId(0x110)]
        void PrivacyImpactedStateChange([In] bool bImpacted);
    }
    public partial class WebBrowserHelper
    {
        private class WebBrowserEvent : StandardOleMarshalObject, DWebBrowserEvents2
        {
            private WebBrowserHelper _helperInstance = null;
            public WebBrowserEvent(WebBrowserHelper helperInstance)
            {
                _helperInstance = helperInstance;
            }
            #region DWebBrowserEvents2 Members
            public void StatusTextChange(string text)
            {
            }
            public void ProgressChange(int progress, int progressMax)
            {
            }
            public void CommandStateChange(long command, bool enable)
            {
            }
            public void DownloadBegin( )
            {
            }
            public void DownloadComplete( )
            {
            }
            public void TitleChange(string text)
            {
            }
            public void PropertyChange(string szProperty)
            {
            }
            public void BeforeNavigate2(object pDisp, ref object URL, ref object flags, ref object targetFrameName, ref object postData, ref object headers, ref bool cancel)
            {
            }
            public void NewWindow2(ref object pDisp, ref bool cancel)
            {
                _helperInstance.OnNewWindow(ref cancel);
            }
            public void NavigateComplete2(object pDisp, ref object URL)
            {
            }
            public void DocumentComplete(object pDisp, ref object URL)
            {
            }
            public void OnQuit( )
            {
            }
            public void OnVisible(bool visible)
            {
            }
            public void OnToolBar(bool toolBar)
            {
            }
            public void OnMenuBar(bool menuBar)
            {
            }
            public void OnStatusBar(bool statusBar)
            {
            }
            public void OnFullScreen(bool fullScreen)
            {
            }
            public void OnTheaterMode(bool theaterMode)
            {
            }
            public void WindowSetResizable(bool resizable)
            {
            }
            public void WindowSetLeft(int left)
            {
            }
            public void WindowSetTop(int top)
            {
            }
            public void WindowSetWidth(int width)
            {
            }
            public void WindowSetHeight(int height)
            {
            }
            public void WindowClosing(bool isChildWindow, ref bool cancel)
            {
            }
            public void ClientToHostWindow(ref long cx, ref long cy)
            {
            }
            public void SetSecureLockIcon(int secureLockIcon)
            {
            }
            public void FileDownload(ref bool cancel)
            {
            }
            public void NavigateError(object pDisp, ref object URL, ref object frame, ref object statusCode, ref bool cancel)
            {
            }
            public void PrintTemplateInstantiation(object pDisp)
            {
            }
            public void PrintTemplateTeardown(object pDisp)
            {
            }
            public void UpdatePageStatus(object pDisp, ref object nPage, ref object fDone)
            {
            }
            public void PrivacyImpactedStateChange(bool bImpacted)
            {
            }
            #endregion
        }
    }
}
