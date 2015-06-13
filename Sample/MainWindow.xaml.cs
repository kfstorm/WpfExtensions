using System;
using System.Windows;
using System.Windows.Input;
using Kfstorm.WpfExtensions;

namespace Sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnMin_OnClick(object sender, EventArgs args)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnMax_OnClick(object sender, EventArgs args)
        {
            SizeToContent = SizeToContent.Manual;
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void BtnClose_OnClick(object sender, EventArgs args)
        {
            Close();
        }

        private void TitleBar_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
            DragMove();
        }

        private ShadowWindow _shadowWindow;

        private void CbShowShadowWindow_OnChecked(object sender, EventArgs args)
        {
            _shadowWindow = ShadowWindow.Attach(this);
        }

        private void CbShowShadowWindow_OnUnchecked(object sender, EventArgs args)
        {
            _shadowWindow.Detach();
            _shadowWindow = null;
        }
    }
}
