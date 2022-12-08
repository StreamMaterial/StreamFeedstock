namespace StreamFeedstock.Controls
{
    public class ScrollViewer : System.Windows.Controls.ScrollViewer, IUIElement
    {
        public ScrollViewer(): base()
        {
            Resources["RepeatButtonTransparent"] = Helper.GetScrollBarRepeatButtonStyle();
        }

        public void Update(BrushPaletteManager palette, TranslationManager translation)
        {
            if (palette.TryGetColor("chat_scrollbar", out var scrollbarBrush))
                Resources["ScrollBarThumbVertical"] = Helper.GetScrollBarThumbStyle(scrollbarBrush);
            if (Content is IUIElement updatable)
                updatable.Update(palette, translation);
        }
    }
}
