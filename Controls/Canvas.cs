﻿using System.Windows;

namespace StreamFeedstock.Controls
{
    public class Canvas : System.Windows.Controls.Canvas, IUIElement
    {
        public void Update(BrushPaletteManager palette, TranslationManager translation)
        {
            foreach (UIElement element in Children)
            {
                if (element is IUIElement updatable)
                    updatable.Update(palette, translation);
            }
        }
    }
}
