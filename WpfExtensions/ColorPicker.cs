using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Kfstorm.WpfExtensions
{
    /// <summary>
    /// A control to pick color
    /// </summary>
    [TemplatePart(Name = "PART_ColorgradualCanvas", Type = typeof(Canvas))]
	[TemplatePart(Name = "PART_ColorgradualSelector", Type = typeof(FrameworkElement))]
	[TemplatePart(Name = "PART_ColorSpectrumSlider", Type = typeof(ColorSpectrumSlider))]
	[TemplatePart(Name = "PART_TextBoxA", Type = typeof(TextBox))]
	[TemplatePart(Name = "PART_TextBoxR", Type = typeof(TextBox))]
	[TemplatePart(Name = "PART_TextBoxG", Type = typeof(TextBox))]
	[TemplatePart(Name = "PART_TextBoxB", Type = typeof(TextBox))]
	[TemplatePart(Name = "PART_TextBoxHexString", Type = typeof(TextBox))]
	public class ColorPicker : Control
	{
		static ColorPicker()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPicker), new FrameworkPropertyMetadata(typeof(ColorPicker)));
		}

		public ColorPicker()
		{
			// Update color after loaded
			Loaded += (sender, e) =>
				{
					UpdateColor(Color, null);
				};
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
			DependencyProperty.Register("Color", typeof(Color), typeof(ColorPicker), new FrameworkPropertyMetadata(Colors.White, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnColorChanged));

		public static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var picker = (ColorPicker)d;
			
			// Update other properties
			picker.A = picker.Color.A;
			picker.R = picker.Color.R;
			picker.G = picker.Color.G;
			picker.B = picker.Color.B;
		    picker.HexString = picker.IsAlphaEnabled ? string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", picker.A, picker.R, picker.G, picker.B) : string.Format("{0:X2}{1:X2}{2:X2}", picker.R, picker.G, picker.B);
			
			picker.RaiseEvent(new RoutedPropertyChangedEventArgs<Color>((Color)e.OldValue, (Color)e.NewValue, ColorChangedEvent));

			picker.UpdateContentsOfTextBoxesByForce();
		}

		public event RoutedPropertyChangedEventHandler<Color> ColorChanged
		{
			add
			{
				AddHandler(ColorChangedEvent, value);
			}
			remove
			{
				RemoveHandler(ColorChangedEvent, value);
			}
		}

		public static readonly RoutedEvent ColorChangedEvent = EventManager.RegisterRoutedEvent("ColorChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<Color>), typeof(ColorPicker));

        #endregion

        #region A property

        /// <summary>
        /// Gets or sets the alpha channel.
        /// </summary>
        /// <value>
        /// The alpha channel.
        /// </value>
        [Category("Common")]
        public byte A
		{
			get { return (byte)GetValue(AProperty); }
			set { SetValue(AProperty, value); }
		}

		public static readonly DependencyProperty AProperty =
			DependencyProperty.Register("A", typeof(byte), typeof(ColorPicker), new FrameworkPropertyMetadata((byte)255, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnAChanged));

		public static void OnAChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var picker = (ColorPicker)d;
			picker.UpdateColor(Color.FromArgb((byte)e.NewValue, picker.Color.R, picker.Color.G, picker.Color.B), null);
			picker.RaiseEvent(new RoutedPropertyChangedEventArgs<byte>((byte)e.OldValue, (byte)e.NewValue, AChangedEvent));
		}

		public event RoutedPropertyChangedEventHandler<byte> AChanged
		{
			add
			{
				AddHandler(AChangedEvent, value);
			}
			remove
			{
				RemoveHandler(AChangedEvent, value);
			}
		}

		public static readonly RoutedEvent AChangedEvent = EventManager.RegisterRoutedEvent("AChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<byte>), typeof(ColorPicker));

        #endregion

        #region R property

        /// <summary>
        /// Gets or sets the red channel.
        /// </summary>
        /// <value>
        /// The red channel.
        /// </value>
        [Category("Common")]
        public byte R
		{
			get { return (byte)GetValue(RProperty); }
			set { SetValue(RProperty, value); }
		}

		public static readonly DependencyProperty RProperty =
			DependencyProperty.Register("R", typeof(byte), typeof(ColorPicker), new FrameworkPropertyMetadata((byte)255, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnRChanged));

		public static void OnRChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var picker = (ColorPicker)d;
			picker.UpdateColor(Color.FromArgb(picker.Color.A, (byte)e.NewValue, picker.Color.G, picker.Color.B), null);
			picker.RaiseEvent(new RoutedPropertyChangedEventArgs<byte>((byte)e.OldValue, (byte)e.NewValue, RChangedEvent));
		}

		public event RoutedPropertyChangedEventHandler<byte> RChanged
		{
			add
			{
				AddHandler(RChangedEvent, value);
			}
			remove
			{
				RemoveHandler(RChangedEvent, value);
			}
		}

		public static readonly RoutedEvent RChangedEvent = EventManager.RegisterRoutedEvent("RChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<byte>), typeof(ColorPicker));

        #endregion

        #region G property

        /// <summary>
        /// Gets or sets the green channel.
        /// </summary>
        /// <value>
        /// The green channel.
        /// </value>
        [Category("Common")]
        public byte G
		{
			get { return (byte)GetValue(GProperty); }
			set { SetValue(GProperty, value); }
		}

		public static readonly DependencyProperty GProperty =
			DependencyProperty.Register("G", typeof(byte), typeof(ColorPicker), new FrameworkPropertyMetadata((byte)255, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnGChanged));

		public static void OnGChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var picker = (ColorPicker)d;
			picker.UpdateColor(Color.FromArgb(picker.Color.A, picker.Color.R, (byte)e.NewValue, picker.Color.B), null);
			picker.RaiseEvent(new RoutedPropertyChangedEventArgs<byte>((byte)e.OldValue, (byte)e.NewValue, GChangedEvent));
		}

		public event RoutedPropertyChangedEventHandler<byte> GChanged
		{
			add
			{
				AddHandler(GChangedEvent, value);
			}
			remove
			{
				RemoveHandler(GChangedEvent, value);
			}
		}

		public static readonly RoutedEvent GChangedEvent = EventManager.RegisterRoutedEvent("GChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<byte>), typeof(ColorPicker));

        #endregion

        #region B property

        /// <summary>
        /// Gets or sets the blue channel.
        /// </summary>
        /// <value>
        /// The blue channel.
        /// </value>
        [Category("Common")]
        public byte B
		{
			get { return (byte)GetValue(BProperty); }
			set { SetValue(BProperty, value); }
		}

		public static readonly DependencyProperty BProperty =
			DependencyProperty.Register("B", typeof(byte), typeof(ColorPicker), new FrameworkPropertyMetadata((byte)255, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBChanged));

		public static void OnBChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var picker = (ColorPicker)d;
			picker.UpdateColor(Color.FromArgb(picker.Color.A, picker.Color.R, picker.Color.G, (byte)e.NewValue), null);
			picker.RaiseEvent(new RoutedPropertyChangedEventArgs<byte>((byte)e.OldValue, (byte)e.NewValue, BChangedEvent));
		}

		public event RoutedPropertyChangedEventHandler<byte> BChanged
		{
			add
			{
				AddHandler(BChangedEvent, value);
			}
			remove
			{
				RemoveHandler(BChangedEvent, value);
			}
		}

		public static readonly RoutedEvent BChangedEvent = EventManager.RegisterRoutedEvent("BChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<byte>), typeof(ColorPicker));

        #endregion

        #region HexString property

        /// <summary>
        /// Gets or sets the hexadecimal string representation of the color.
        /// </summary>
        /// <value>
        /// The hexadecimal string representation of the color.
        /// </value>
        [Category("Common")]
        public string HexString
		{
			get { return (string)GetValue(HexStringProperty); }
			set { SetValue(HexStringProperty, value); }
		}

		public static readonly DependencyProperty HexStringProperty =
			DependencyProperty.Register("HexString", typeof(string), typeof(ColorPicker), new FrameworkPropertyMetadata("FFFFFFFF", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnHexStringChanged),
			o =>
			{
			    var hex = o as string;
			    if (hex == null) return false;
			    if (hex.Length != 8 && hex.Length != 6) return false;
			    return hex.All(ch => (ch >= '0' && ch <= '9') || (ch >= 'A' && ch <= 'F') || (ch >= 'a' && ch <= 'f'));
			});

		public static void OnHexStringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var picker = (ColorPicker)d;

			if (((string)e.NewValue).Length == 8 && picker.IsAlphaEnabled)
			{
				picker.UpdateColor(Color.FromArgb(
					byte.Parse(((string)e.NewValue).Substring(0, 2), NumberStyles.HexNumber),
					byte.Parse(((string)e.NewValue).Substring(2, 2), NumberStyles.HexNumber),
					byte.Parse(((string)e.NewValue).Substring(4, 2), NumberStyles.HexNumber),
					byte.Parse(((string)e.NewValue).Substring(6, 2), NumberStyles.HexNumber)),
					null);
				picker.RaiseEvent(new RoutedPropertyChangedEventArgs<string>((string)e.OldValue, (string)e.NewValue, HexStringChangedEvent));
			}
			else if (((string)e.NewValue).Length == 6 && !picker.IsAlphaEnabled)
			{
				picker.UpdateColor(Color.FromRgb(
					byte.Parse(((string)e.NewValue).Substring(0, 2), NumberStyles.HexNumber),
					byte.Parse(((string)e.NewValue).Substring(2, 2), NumberStyles.HexNumber),
					byte.Parse(((string)e.NewValue).Substring(4, 2), NumberStyles.HexNumber)),
					null);
			}
		}

		public event RoutedPropertyChangedEventHandler<string> HexStringChanged
		{
			add
			{
				AddHandler(HexStringChangedEvent, value);
			}
			remove
			{
				RemoveHandler(HexStringChangedEvent, value);
			}
		}

		public static readonly RoutedEvent HexStringChangedEvent = EventManager.RegisterRoutedEvent("HexStringChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<string>), typeof(ColorPicker));

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
			DependencyProperty.Register("IsAlphaEnabled", typeof(bool), typeof(ColorPicker), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsAlphaEnabledChanged));

		public static void OnIsAlphaEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var picker = (ColorPicker)d;
			if (!(bool)e.NewValue)
			{
				var color = picker.Color;
				color.A = 255;
				picker.HexString = string.Format("{0:X2}{1:X2}{2:X2}", picker.R, picker.G, picker.B);
				picker.UpdateColor(color, null);
			}
			else
			{
				picker.HexString = string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", picker.A, picker.R, picker.G, picker.B);
			}
			picker.RaiseEvent(new RoutedPropertyChangedEventArgs<bool>((bool)e.OldValue, (bool)e.NewValue, IsAlphaEnabledChangedEvent));
		}

		public event RoutedPropertyChangedEventHandler<bool> IsAlphaEnabledChanged
		{
			add
			{
				AddHandler(IsAlphaEnabledChangedEvent, value);
			}
			remove
			{
				RemoveHandler(IsAlphaEnabledChangedEvent, value);
			}
		}

		public static readonly RoutedEvent IsAlphaEnabledChangedEvent = EventManager.RegisterRoutedEvent("IsAlphaEnabledChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<bool>), typeof(ColorPicker));

        #endregion

        #region Fields
        /// <summary>
        /// The last precise color
        /// </summary>
        private HslColor? _lastPreciseColor;
		/// <summary>
		/// Indicating if is updating the color
		/// </summary>
		bool _updatingColor;

		private Canvas _gradualCanvas;
		private FrameworkElement _gradualSelector;
		private ColorSpectrumSlider _colorSpectrumSlider;
		private TextBox _tbA;
		private TextBox _tbR;
		private TextBox _tbG;
		private TextBox _tbB;
		private TextBox _tbHexString;
        #endregion

        #region Logics

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			_gradualCanvas = Template.FindName("PART_GradualCanvas", this) as Canvas;
			_gradualSelector = Template.FindName("PART_GradualSelector", this) as FrameworkElement;
			_colorSpectrumSlider = Template.FindName("PART_ColorSpectrumSlider", this) as ColorSpectrumSlider;
			_tbA = Template.FindName("PART_TextBoxA", this) as TextBox;
			_tbR = Template.FindName("PART_TextBoxR", this) as TextBox;
			_tbG = Template.FindName("PART_TextBoxG", this) as TextBox;
			_tbB = Template.FindName("PART_TextBoxB", this) as TextBox;
			_tbHexString = Template.FindName("PART_TextBoxHexString", this) as TextBox;

            if (_gradualCanvas != null)
            {
                _gradualCanvas.PreviewMouseLeftButtonDown += (o, e) =>
                {
                    var p = e.GetPosition(_gradualCanvas);
                    UpdateColorByPosition(p);
                    _gradualCanvas.CaptureMouse();
                    e.Handled = true;
                };
                _gradualCanvas.MouseMove += (o, e) =>
                {
                    if (_gradualCanvas.IsMouseCaptured && e.LeftButton == MouseButtonState.Pressed)
                    {
                        var position = e.GetPosition(_gradualCanvas);
                        UpdateColorByPosition(position);
                    }
                };
                _gradualCanvas.MouseLeftButtonUp += (o, e) =>
                {
                    _gradualCanvas.ReleaseMouseCapture();
                };
                _gradualCanvas.SizeChanged += (o, e) =>
                {
                    var hslColor = _lastPreciseColor ?? HslColor.FromArgb(Color);
                    hslColor.H = _colorSpectrumSlider.Value;
                    UpdateColor(hslColor.ToArgb(), hslColor);
                };
            }
            if (_colorSpectrumSlider != null)
            {
                _colorSpectrumSlider.ValueChanged += (o, e) =>
                {
                    var hslColor = _lastPreciseColor ?? HslColor.FromArgb(Color);
                    hslColor.H = _colorSpectrumSlider.Value;
                    UpdateColor(hslColor.ToArgb(), hslColor);
                };
            }

            UpdateColor(Color, null);
		}

        /// <summary>
        /// Updates the color based on the position of the gradual selector
        /// </summary>
        /// <param name="position">The center position of the gradual selector relative to the gradual canvas.</param>
        protected void UpdateColorByPosition(Point position)
		{
			if (position.X < 0) position.X = 0;
			if (position.X > _gradualCanvas.ActualWidth) position.X = _gradualCanvas.ActualWidth;
			if (position.Y < 0) position.Y = 0;
			if (position.Y > _gradualCanvas.ActualHeight) position.Y = _gradualCanvas.ActualHeight;

			// Calculate the new color
			var hslColor = _lastPreciseColor ?? HslColor.FromArgb(_colorSpectrumSlider.SelectedColor);
			if (!_lastPreciseColor.HasValue)
			{
				hslColor.A = (double)Color.A / 255;
			}
			hslColor.S = position.X / _gradualCanvas.ActualWidth;
			hslColor.L = 1 - position.Y / _gradualCanvas.ActualHeight;

			UpdateColor(hslColor.ToArgb(), hslColor);
		}

        /// <summary>
        /// Updates the color.
        /// </summary>
        /// <param name="newColor">The new color.</param>
        /// <param name="preciseColor">The precise color if the new color is calculated by it, otherwise null.</param>
        protected void UpdateColor(Color newColor, HslColor? preciseColor)
		{
			if (_updatingColor) return;
			_updatingColor = true;

			var hslColor = preciseColor ?? HslColor.FromArgb(newColor);
			_lastPreciseColor = preciseColor;

			if (_gradualCanvas != null)
			{
				if (preciseColor == null && Math.Abs(hslColor.S) < 1e-7 && Math.Abs(hslColor.L) < 1e-7)
				{
					Canvas.SetLeft(_gradualSelector, _gradualCanvas.ActualWidth - _gradualSelector.ActualWidth / 2);
				}
				else
				{
					Canvas.SetLeft(_gradualSelector, hslColor.S * _gradualCanvas.ActualWidth - _gradualSelector.ActualWidth / 2);
				}
				Canvas.SetTop(_gradualSelector, (1 - hslColor.L) * _gradualCanvas.ActualHeight - _gradualSelector.ActualHeight / 2);
				if (hslColor.S > 0)
				{
					_colorSpectrumSlider.Value = hslColor.H;
				}
			}
			if (Color == newColor)
			{
				Color = newColor;
                UpdateContentsOfTextBoxesByForce();
			}
			else
			{
				Color = newColor;
			}

			_updatingColor = false;
		}

        /// <summary>
        /// Updates the contents of text boxes by force.
        /// </summary>
        public void UpdateContentsOfTextBoxesByForce()
		{
			if (_tbA != null)
			{
				var expression = _tbA.GetBindingExpression(TextBox.TextProperty);
				if (expression != null)
					expression.UpdateTarget();
			}

			if (_tbR != null)
			{
				var expression = _tbR.GetBindingExpression(TextBox.TextProperty);
				if (expression != null)
					expression.UpdateTarget();
			}

			if (_tbG != null)
			{
				var expression = _tbG.GetBindingExpression(TextBox.TextProperty);
				if (expression != null)
					expression.UpdateTarget();
			}

			if (_tbB != null)
			{
				var expression = _tbB.GetBindingExpression(TextBox.TextProperty);
				if (expression != null)
					expression.UpdateTarget();
			}

			if (_tbHexString != null)
			{
				var expression = _tbHexString.GetBindingExpression(TextBox.TextProperty);
				if (expression != null)
					expression.UpdateTarget();
			}
		}

		#endregion
	}
}