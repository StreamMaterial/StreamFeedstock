﻿using System.ComponentModel;
using System.Windows;

namespace StreamFeedstock.Controls
{
    public class TextBox : System.Windows.Controls.TextBox, IUIElement
    {
        #region BrushPaletteKey
        public static readonly DependencyProperty BrushPaletteKeyProperty = DependencyProperty.Register("BrushPaletteKey", typeof(string), typeof(TextBox), new FrameworkPropertyMetadata("background_2"));
        [Description("The brush palette key of the text box's background"), Category("Common Properties")]
        public string BrushPaletteKey
        {
            get => (string)GetValue(BrushPaletteKeyProperty);
            set => SetValue(BrushPaletteKeyProperty, value);
        }
        #endregion BrushPaletteKey

        #region TextBrushPaletteKey
        public static readonly DependencyProperty TextBrushPaletteKeyProperty = DependencyProperty.Register("TextBrushPaletteKey", typeof(string), typeof(TextBox), new FrameworkPropertyMetadata("text"));
        [Description("The brush palette key of the text box's text"), Category("Common Properties")]
        public string TextBrushPaletteKey
        {
            get => (string)GetValue(TextBrushPaletteKeyProperty);
            set => SetValue(TextBrushPaletteKeyProperty, value);
        }
        #endregion TextBrushPaletteKey

        public void Update(BrushPaletteManager palette, TranslationManager _)
        {
            if (palette.TryGetColor(BrushPaletteKey, out var background))
                Background = background;
            if (palette.TryGetColor(TextBrushPaletteKey, out var foreground))
                Foreground = foreground;
        }
    }
}
