﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace StreamFeedstock.Controls
{
    public class Track : System.Windows.Controls.Primitives.Track, IUIElement
    {
        public Track()
        {
            DecreaseRepeatButton = new System.Windows.Controls.Primitives.RepeatButton
            {
                Style = Helper.GetScrollBarRepeatButtonStyle()
            };
            IncreaseRepeatButton = new System.Windows.Controls.Primitives.RepeatButton
            {
                Style = Helper.GetScrollBarRepeatButtonStyle()
            };
            Thumb = new System.Windows.Controls.Primitives.Thumb();
        }

        public void Update(BrushPaletteManager palette, TranslationManager translation)
        {
            if (palette.TryGetColor("chat_scrollbar", out var scrollbarBrush))
                Thumb.Style = Helper.GetScrollBarThumbStyle(scrollbarBrush);
        }
    }
}
