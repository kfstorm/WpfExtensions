using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace Kfstorm.WpfExtensions
{
    /// <summary>
    /// A control to pick font or font family.
    /// </summary>
    public class FontPicker : ComboBox
    {

        /// <summary>
        /// Get the name of the font with specified culture info.
        /// </summary>
        /// <param name="fontFamily">The font.</param>
        /// <param name="cultureInfo">The culture info.</param>
        /// <returns>
        /// The name of the font.
        /// </returns>
        /// <remarks>Only supports single font, not font family.</remarks>
        protected static string GetFontName(FontFamily fontFamily, CultureInfo cultureInfo)
        {
            if (fontFamily == null) return string.Empty;
            try
            {
                if (fontFamily.FamilyNames.ContainsKey(XmlLanguage.GetLanguage(cultureInfo.Name)))
                {
                    return fontFamily.FamilyNames[XmlLanguage.GetLanguage(cultureInfo.Name)];
                }
                return fontFamily.FamilyNames.First().Value;
            }
            catch (ArgumentException)
            {
                // WPF Bug: http://code.logos.com/blog/2012/11/how-to-crash-many-wpf-applications-wpf-4-edition.html
                return fontFamily.Source;
            }
        }

        static FontPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FontPicker), new FrameworkPropertyMetadata(typeof(FontPicker)));

            // Get installed fonts
            var fonts = from font in Fonts.SystemFontFamilies let name = GetFontName(font, CultureInfo.CurrentCulture) where !string.IsNullOrWhiteSpace(name) select name;
            SystemFonts = fonts.ToList();
            SystemFonts.Sort();
        }

        /// <summary>
        /// The picked font
        /// </summary>
        [Category("Common")]
        public FontFamily Font
        {
            get { return (FontFamily)GetValue(FontProperty); }
            set { SetValue(FontProperty, value); }
        }

        public static readonly DependencyProperty FontProperty =
            DependencyProperty.Register("Font", typeof(FontFamily), typeof(FontPicker), new FrameworkPropertyMetadata(System.Windows.SystemFonts.MessageFontFamily, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnFontChanged));

        private static void OnFontChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var picker = (FontPicker)d;
            if (e.NewValue != null)
            {
                var str = ((FontFamily)e.NewValue).ToString();
                if (picker.Text != str)
                {
                    picker.Text = str;
                }
            }
            picker.RaiseEvent(new RoutedPropertyChangedEventArgs<FontFamily>((FontFamily)e.OldValue, (FontFamily)e.NewValue, FontChangedEvent));
        }

        /// <summary>
        /// Occurs when the picked font changed.
        /// </summary>
        public event RoutedPropertyChangedEventHandler<FontFamily> FontChanged
        {
            add
            {
                AddHandler(FontChangedEvent, value);
            }
            remove
            {
                RemoveHandler(FontChangedEvent, value);
            }
        }

        public static readonly RoutedEvent FontChangedEvent = EventManager.RegisterRoutedEvent("FontChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<FontFamily>), typeof(FontPicker));

        /// <summary>
        /// Names of installed fonts.
        /// </summary>
        protected static readonly List<string> SystemFonts;

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ItemsSource = SystemFonts;
            SelectionChanged += (o, e) =>
            {
                // At this time, the Text property is still unchanged. so we have to read the Text property later.
                Dispatcher.BeginInvoke(new Action(UpdatePickedFont));
            };
            KeyUp += (o, e) =>
            {
                // The content of combobox may changed, so we need to update the picked font.
                UpdatePickedFont();
            };
            if (Font != null)
            {
                Text = Font.ToString();
            }
        }

        /// <summary>
        /// Update picked font based on the input.
        /// </summary>
        protected void UpdatePickedFont()
        {
            try
            {
                Font = new FontFamily(Text);
            }
            catch
            {
                Font = System.Windows.SystemFonts.MessageFontFamily;
            }
        }
    }
}