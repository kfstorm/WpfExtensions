using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Kfstorm.WpfExtensions
{
    /// <summary>
    /// A control to pick color with popup
    /// </summary>
    [TemplatePart(Name = "PART_Popup", Type = typeof(Popup))]
    [TemplatePart(Name = "PART_Border", Type = typeof(Border))]
    public class PopupableColorPicker : Control
    {
        static PopupableColorPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PopupableColorPicker), new FrameworkPropertyMetadata(typeof(PopupableColorPicker)));
        }

        #region Color property

        /// <summary>
        /// Gets or sets the picked color.
        /// </summary>
        /// <value>
        /// The picked color.
        /// </value>
        [Category("Common")]
        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Color), typeof(PopupableColorPicker), new FrameworkPropertyMetadata(Colors.White, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnColorChanged));

        public static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var picker = (PopupableColorPicker)d;
            picker.RaiseEvent(new RoutedPropertyChangedEventArgs<Color>((Color)e.OldValue, (Color)e.NewValue, ColorChangedEvent));
        }

        public event RoutedPropertyChangedEventHandler<Color> ColorChanged
        {
            add { AddHandler(ColorChangedEvent, value); }
            remove { RemoveHandler(ColorChangedEvent, value); }
        }

        public static readonly RoutedEvent ColorChangedEvent = EventManager.RegisterRoutedEvent("ColorChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<Color>), typeof(PopupableColorPicker));

        #endregion

        #region IsAlphaEnabled property

        /// <summary>
        /// Gets or sets a value indicating whether the alpha channel is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if the alpha channel is enabled; otherwise, <c>false</c>.
        /// </value>
        [Category("Common")]
        public bool IsAlphaEnabled
        {
            get { return (bool)GetValue(IsAlphaEnabledProperty); }
            set { SetValue(IsAlphaEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsAlphaEnabledProperty =
            DependencyProperty.Register("IsAlphaEnabled", typeof(bool), typeof(PopupableColorPicker), new FrameworkPropertyMetadata(true, OnIsAlphaEnabledChanged));

        public static void OnIsAlphaEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var picker = (PopupableColorPicker)d;
            picker.RaiseEvent(new RoutedPropertyChangedEventArgs<bool>((bool)e.OldValue, (bool)e.NewValue, IsAlphaEnabledChangedEvent));
        }

        public event RoutedPropertyChangedEventHandler<bool> IsAlphaEnabledChanged
        {
            add { AddHandler(IsAlphaEnabledChangedEvent, value); }
            remove { RemoveHandler(IsAlphaEnabledChangedEvent, value); }
        }

        public static readonly RoutedEvent IsAlphaEnabledChangedEvent = EventManager.RegisterRoutedEvent("IsAlphaEnabledChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<bool>), typeof(PopupableColorPicker));

        #endregion

        private Popup _popup;
        private Border _button;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _popup = Template.FindName("PART_Popup", this) as Popup;
            _button = Template.FindName("PART_Border", this) as Border;

            if (_button != null)
            {
                _button.MouseLeftButtonUp += (sender, args) =>
                {
                    _popup.IsOpen = true;
                };
            }
        }
    }
}
