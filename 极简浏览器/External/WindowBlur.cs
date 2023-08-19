using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using static 极简浏览器.External.NativeMethods;

namespace 极简浏览器.External;
public class WindowBlur
{
    public static readonly DependencyProperty IsEnabledProperty =
        DependencyProperty.RegisterAttached(
            "IsEnabled",
            typeof(bool),
            typeof(WindowBlur),
            new PropertyMetadata(false, OnIsEnabledChanged));

    public static void SetIsEnabled(DependencyObject element, bool value)
        => element.SetValue(IsEnabledProperty, value);

    public static bool GetIsEnabled(DependencyObject element)
        => (bool) element.GetValue(IsEnabledProperty);

    private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Window window)
            return;
        if (true.Equals(e.OldValue))
        {
            GetWindowBlur(window)?.Detach( );
            window.ClearValue(WindowBlurProperty);
        }
        if (true.Equals(e.NewValue))
        {
            WindowBlur blur = new( );
            blur.Attach(window);
            window.SetValue(WindowBlurProperty, blur);
        }
    }

    public static readonly DependencyProperty WindowBlurProperty =
        DependencyProperty.RegisterAttached(
            "WindowBlur",
            typeof(WindowBlur),
            typeof(WindowBlur),
            new PropertyMetadata(null, OnWindowBlurChanged));

    public static void SetWindowBlur(DependencyObject element, WindowBlur value)
        => element.SetValue(WindowBlurProperty, value);

    public static WindowBlur GetWindowBlur(DependencyObject element)
        => (WindowBlur) element.GetValue(WindowBlurProperty);

    private static void OnWindowBlurChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Window window)
        {
            (e.OldValue as WindowBlur)?.Detach( );
            (e.NewValue as WindowBlur)?.Attach(window);
        }
    }

    private Window _window;

    private void Attach(Window window)
    {
        _window = window;
        HwndSource source = (HwndSource) PresentationSource.FromVisual(window);
        if (source == null)
        {
            window.SourceInitialized += OnSourceInitialized;
        }
        else
        {
            AttachCore( );
        }
    }

    private void Detach( )
    {
        try
        {
            DetachCore( );
        }
        finally
        {
            _window = null;
        }
    }

    private void OnSourceInitialized(object sender, EventArgs e)
    {
        ((Window) sender).SourceInitialized -= OnSourceInitialized;
        AttachCore( );
    }

    private void AttachCore( )
        => EnableBlur(_window);

    private void DetachCore( )
        => _window.SourceInitialized += OnSourceInitialized;

    private static void EnableBlur(Window window)
    {
        WindowInteropHelper windowHelper = new(window);

        AccentPolicy accent = new( )
        {
            AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND
        };
        int accentStructSize = Marshal.SizeOf(accent);
        IntPtr accentPtr = Marshal.AllocHGlobal(accentStructSize);
        Marshal.StructureToPtr(accent, accentPtr, false);

        WindowCompositionAttributeData data = new( )
        {
            Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
            SizeOfData = accentStructSize,
            Data = accentPtr
        };

        _ = SetWindowCompositionAttribute(windowHelper.Handle, ref data);

        Marshal.FreeHGlobal(accentPtr);
    }
}

