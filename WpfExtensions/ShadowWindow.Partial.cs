
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Kfstorm.WpfExtensions
{
    partial class ShadowWindow
    {
        protected static readonly ImageSource ActiveBottomImage;
        protected static readonly ImageSource ActiveBottomLeftImage;
        protected static readonly ImageSource ActiveBottomRightImage;
        protected static readonly ImageSource ActiveLeftImage;
        protected static readonly ImageSource ActiveRightImage;
        protected static readonly ImageSource ActiveTopImage;
        protected static readonly ImageSource ActiveTopLeftImage;
        protected static readonly ImageSource ActiveTopRightImage;
        protected static readonly ImageSource InactiveBottomImage;
        protected static readonly ImageSource InactiveBottomLeftImage;
        protected static readonly ImageSource InactiveBottomRightImage;
        protected static readonly ImageSource InactiveLeftImage;
        protected static readonly ImageSource InactiveRightImage;
        protected static readonly ImageSource InactiveTopImage;
        protected static readonly ImageSource InactiveTopLeftImage;
        protected static readonly ImageSource InactiveTopRightImage;

        static ShadowWindow()
        {
            ActiveBottomImage = new BitmapImage(new Uri("pack://application:,,,/Kfstorm.WpfExtensions;component/Images/Shadow/ActiveBottom.png"));
            ActiveBottomImage.Freeze();
            ActiveBottomLeftImage = new BitmapImage(new Uri("pack://application:,,,/Kfstorm.WpfExtensions;component/Images/Shadow/ActiveBottomLeft.png"));
            ActiveBottomLeftImage.Freeze();
            ActiveBottomRightImage = new BitmapImage(new Uri("pack://application:,,,/Kfstorm.WpfExtensions;component/Images/Shadow/ActiveBottomRight.png"));
            ActiveBottomRightImage.Freeze();
            ActiveLeftImage = new BitmapImage(new Uri("pack://application:,,,/Kfstorm.WpfExtensions;component/Images/Shadow/ActiveLeft.png"));
            ActiveLeftImage.Freeze();
            ActiveRightImage = new BitmapImage(new Uri("pack://application:,,,/Kfstorm.WpfExtensions;component/Images/Shadow/ActiveRight.png"));
            ActiveRightImage.Freeze();
            ActiveTopImage = new BitmapImage(new Uri("pack://application:,,,/Kfstorm.WpfExtensions;component/Images/Shadow/ActiveTop.png"));
            ActiveTopImage.Freeze();
            ActiveTopLeftImage = new BitmapImage(new Uri("pack://application:,,,/Kfstorm.WpfExtensions;component/Images/Shadow/ActiveTopLeft.png"));
            ActiveTopLeftImage.Freeze();
            ActiveTopRightImage = new BitmapImage(new Uri("pack://application:,,,/Kfstorm.WpfExtensions;component/Images/Shadow/ActiveTopRight.png"));
            ActiveTopRightImage.Freeze();
            InactiveBottomImage = new BitmapImage(new Uri("pack://application:,,,/Kfstorm.WpfExtensions;component/Images/Shadow/InactiveBottom.png"));
            InactiveBottomImage.Freeze();
            InactiveBottomLeftImage = new BitmapImage(new Uri("pack://application:,,,/Kfstorm.WpfExtensions;component/Images/Shadow/InactiveBottomLeft.png"));
            InactiveBottomLeftImage.Freeze();
            InactiveBottomRightImage = new BitmapImage(new Uri("pack://application:,,,/Kfstorm.WpfExtensions;component/Images/Shadow/InactiveBottomRight.png"));
            InactiveBottomRightImage.Freeze();
            InactiveLeftImage = new BitmapImage(new Uri("pack://application:,,,/Kfstorm.WpfExtensions;component/Images/Shadow/InactiveLeft.png"));
            InactiveLeftImage.Freeze();
            InactiveRightImage = new BitmapImage(new Uri("pack://application:,,,/Kfstorm.WpfExtensions;component/Images/Shadow/InactiveRight.png"));
            InactiveRightImage.Freeze();
            InactiveTopImage = new BitmapImage(new Uri("pack://application:,,,/Kfstorm.WpfExtensions;component/Images/Shadow/InactiveTop.png"));
            InactiveTopImage.Freeze();
            InactiveTopLeftImage = new BitmapImage(new Uri("pack://application:,,,/Kfstorm.WpfExtensions;component/Images/Shadow/InactiveTopLeft.png"));
            InactiveTopLeftImage.Freeze();
            InactiveTopRightImage = new BitmapImage(new Uri("pack://application:,,,/Kfstorm.WpfExtensions;component/Images/Shadow/InactiveTopRight.png"));
            InactiveTopRightImage.Freeze();
        }

        /// <summary>
        /// Changes to active shadow.
        /// </summary>
        protected void ChangeToActiveShadow()
        {
            ImBottom.Source = ActiveBottomImage;
            ImBottomLeft.Source = ActiveBottomLeftImage;
            ImBottomRight.Source = ActiveBottomRightImage;
            ImLeft.Source = ActiveLeftImage;
            ImRight.Source = ActiveRightImage;
            ImTop.Source = ActiveTopImage;
            ImTopLeft.Source = ActiveTopLeftImage;
            ImTopRight.Source = ActiveTopRightImage;
        }

        /// <summary>
        /// Changes to inactive shadow.
        /// </summary>
        protected void ChangeToInactiveShadow()
        {
            ImBottom.Source = InactiveBottomImage;
            ImBottomLeft.Source = InactiveBottomLeftImage;
            ImBottomRight.Source = InactiveBottomRightImage;
            ImLeft.Source = InactiveLeftImage;
            ImRight.Source = InactiveRightImage;
            ImTop.Source = InactiveTopImage;
            ImTopLeft.Source = InactiveTopLeftImage;
            ImTopRight.Source = InactiveTopRightImage;
        }
    }
}
