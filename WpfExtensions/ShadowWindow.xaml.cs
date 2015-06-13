using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Interop;
using Kfstorm.WpfExtensions.Interop;

namespace Kfstorm.WpfExtensions
{
    /// <summary>
    /// A Window shows shadows around another window. Like the window shadow of Windows 7 OS.
    /// </summary>
    public partial class ShadowWindow
    {
        /// <summary>
        /// The native window handle of shadow window
        /// </summary>
        protected IntPtr Handle = IntPtr.Zero;

        /// <summary>
        /// The native window handle of owner window
        /// </summary>
        protected IntPtr OwnerHandle = IntPtr.Zero;

        private const double ShadowSize = 24;
        private static readonly GridLength GridShadowSizeField = new GridLength(ShadowSize, GridUnitType.Pixel);

        public GridLength GridShadowSize { get { return GridShadowSizeField; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShadowWindow"/> class.
        /// </summary>
        /// <param name="owner">The owner window.</param>
        public ShadowWindow(Window owner)
        {
            InitializeComponent();

            if (owner.IsLoaded)
            {
                Init(owner);
            }
            else
            {
                owner.ContentRendered += delegate
                {
                    Init(owner);
                };
            }
            LocationChanged += ShadowWindow_LocationChanged;
        }

        private bool _initialized;

        private void Init(Window owner)
        {
            if (_initialized) return;

            Owner = owner;
            Handle = new WindowInteropHelper(this).EnsureHandle();
            OwnerHandle = new WindowInteropHelper(Owner).EnsureHandle();

            var extendedStyle = NativeMethods.GetWindowLong(Handle, GWL.EXSTYLE);
            NativeMethods.SetWindowLong(Handle, GWL.EXSTYLE, extendedStyle | WS.EX.TRANSPARENT /* bypass mouse events*/);

            // Monitor events of owner window
            Owner.Activated += Owner_Activated;
            Owner.Deactivated += Owner_Deactivated;
            Owner.StateChanged += Owner_StateChanged;
            Owner.IsVisibleChanged += Owner_IsVisibleChanged;
            Owner.LocationChanged += Owner_LocationChanged;
            Owner.SizeChanged += Owner_SizeChanged;

            _initialized = true;

            // Initialize the shadows
            if (Owner.IsActive)
            {
                ChangeToActiveShadow();
            }
            else
            {
                ChangeToInactiveShadow();
            }

            // Initialize location
            Owner_LocationChanged(null, null);
            Owner_SizeChanged(null, null);

            TryShowShadow();
        }

        private void Owner_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Width = Owner.ActualWidth + ShadowSize * 2;
            Height = Owner.ActualHeight + ShadowSize * 2;
        }

        private bool _isOwnerLocationChanged;
        private readonly object _lockObject = new object();

        private void Owner_LocationChanged(object sender, EventArgs e)
        {
            lock (_lockObject)
            {
                _isOwnerLocationChanged = true;
                Left = Owner.Left - ShadowSize;
                Top = Owner.Top - ShadowSize;
                _isOwnerLocationChanged = false;
            }
        }

        private void ShadowWindow_LocationChanged(object sender, EventArgs e)
        {
            if (!_isOwnerLocationChanged)
            {
                Left = Owner.Left - ShadowSize;
                Top = Owner.Top - ShadowSize;
            }
        }

        private void Owner_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                TryShowShadow();
            }
            else
            {
                HideShadow();
            }
        }

        private void Owner_StateChanged(object sender, EventArgs e)
        {
            switch (Owner.WindowState)
            {
                case WindowState.Maximized:
                case WindowState.Minimized:
                    HideShadow();
                    break;

                case WindowState.Normal:
                    TryShowShadow();
                    break;
            }
        }

        private void Owner_Activated(object sender, EventArgs e)
        {
            ChangeToActiveShadow();
        }

        private void Owner_Deactivated(object sender, EventArgs e)
        {
            ChangeToInactiveShadow();
        }

        /// <summary>
        /// Hide the shadows
        /// </summary>
        protected void HideShadow()
        {
            try
            {
                Hide();
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Try to show the shadows when the owner window satisfies conditions.
        /// </summary>
        protected void TryShowShadow()
        {
            if (Owner.IsVisible && Owner.Visibility == Visibility.Visible && Owner.WindowState == WindowState.Normal)
            {
                try
                {
                    Show();
                    // Ensure shadow window always use adjacent Z order. Incase sometimes it shows on top of another window when the other window shows on top of the owner window.
                    NativeMethods.SetWindowPos(Handle, OwnerHandle, 0, 0, 0, 0, SWP.SHOWWINDOW | SWP.NOACTIVATE | SWP.NOOWNERZORDER | SWP.NOREPOSITION | SWP.NOMOVE | SWP.NOSIZE);
                }
                catch
                {
                    // ignored
                }
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            try
            {
                Owner.Activate();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Attaches the specified window to show shadows.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <returns>The new instance of <see cref="ShadowWindow"/> class.</returns>
        public static ShadowWindow Attach(Window window)
        {
            return new ShadowWindow(window);
        }

        /// <summary>
        /// Detaches from the owner window.
        /// </summary>
        public void Detach()
        {
            Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            if (_initialized)
            {
                Owner.Activated -= Owner_Activated;
                Owner.Deactivated -= Owner_Deactivated;
                Owner.StateChanged -= Owner_StateChanged;
                Owner.IsVisibleChanged -= Owner_IsVisibleChanged;
                Owner.LocationChanged -= Owner_LocationChanged;
                Owner.SizeChanged -= Owner_SizeChanged;
                _initialized = false;
            }

            base.OnClosed(e);
        }
    }
}