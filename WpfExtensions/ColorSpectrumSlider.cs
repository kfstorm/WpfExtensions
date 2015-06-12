using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Kfstorm.WpfExtensions
{
    /// <summary>
    /// A control to select a color from color spectrum
    /// </summary>
    [TemplatePart(Name="PART_Thumb", Type=typeof(Thumb))]
	[TemplatePart(Name="PART_Spectrum", Type=typeof(FrameworkElement))]
	internal class ColorSpectrumSlider : Slider
	{
		static ColorSpectrumSlider()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorSpectrumSlider), new FrameworkPropertyMetadata(typeof(ColorSpectrumSlider)));
		}

		public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(Color), typeof(ColorSpectrumSlider), new PropertyMetadata(Colors.Red));
        /// <summary>
        /// Gets or sets the selected color.
        /// </summary>
        /// <value>
        /// The selected color.
        /// </value>
        public Color SelectedColor
		{
			get { return (Color)GetValue(SelectedColorProperty); }
			set { SetValue(SelectedColorProperty, value); }
		}

		private FrameworkElement _spectrum;

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			_spectrum = Template.FindName("PART_Spectrum", this) as FrameworkElement;
		}

		protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			var p = e.GetPosition(_spectrum);
			Value = (p.Y / _spectrum.ActualHeight) * (Maximum - Minimum) + Minimum;
			CaptureMouse();
			Focus();
			e.Handled = true;

			base.OnPreviewMouseLeftButtonDown(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (IsMouseCaptured && e.LeftButton == MouseButtonState.Pressed)
			{
				var p = e.GetPosition(_spectrum);
				Value = (p.Y / _spectrum.ActualHeight) * (Maximum - Minimum) + Minimum;
			}
			base.OnMouseMove(e);
		}

		protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
		{
			ReleaseMouseCapture();
			base.OnMouseLeftButtonUp(e);
		}

		protected override void OnValueChanged(double oldValue, double newValue)
		{
			base.OnValueChanged(oldValue, newValue);

			SelectedColor = new HslColor(1, newValue, 1, 1).ToArgb();
		}

	}
}
