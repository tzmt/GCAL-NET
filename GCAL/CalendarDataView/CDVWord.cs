﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GCAL.CalendarDataView
{
    public class CDVWord: CDVAtom
    {
        public string Text { get; set; }

        public CDVWord(CDVAtom owner): base(owner)
        {

        }

        public CDVWord(CDVAtom owner, params object[] args): base(owner)
        {
            foreach(object obj in args)
            {
                if (obj is CDVTextStyle)
                {
                    this.TextStyle = (CDVTextStyle)obj;
                }
                else if (obj is CDVParaStyle)
                {
                    this.ParaStyle = (CDVParaStyle)obj;
                }
                else if (obj is string)
                {
                    this.Text = (string)obj;
                }
            }
        }

        public override int GetMinimumWidth(CDVContext context)
        {
            Font f = CDVContext.GetFont(TextStyle.Font, TextStyle.FontSize, TextStyle.Bold, TextStyle.Italic, TextStyle.Underline);
            SizeF sf = context.g.MeasureString(Text, f);
            return base.GetMinimumWidth(context);
        }

        public override void DrawInRect(CDVContext context)
        {
            if (!Visible) return;

            base.DrawInRect(context);

            int x, y;
            GetAbsoluteLocation(out x, out y);

            int bx = p_para_style == null ? 0 : p_para_style.Margin.Left + p_para_style.Padding.Left;
            int by = p_para_style == null ? 0 : p_para_style.Margin.Top + p_para_style.Padding.Top;

            Font f = CDVContext.GetFont(TextStyle.Font, TextStyle.FontSize, TextStyle.Bold, TextStyle.Italic, TextStyle.Underline);
            context.g.DrawString(Text, f, CDVContext.GetBrush(TextStyle.Color), x + bx, y + by);
        }

        public override void MeasureRect(CDVContext context, int maxWidth)
        {
            int bx = p_para_style == null ? 0 : p_para_style.Margin.Left + p_para_style.Padding.Left;
            int by = p_para_style == null ? 0 : p_para_style.Margin.Top + p_para_style.Padding.Top;
            int bw = p_para_style == null ? 0 : p_para_style.Margin.Right + p_para_style.Padding.Right;
            int bh = p_para_style == null ? 0 : p_para_style.Margin.Bottom + p_para_style.Padding.Bottom;

            Font f = CDVContext.GetFont(TextStyle.Font, TextStyle.FontSize, TextStyle.Bold, TextStyle.Italic, TextStyle.Underline);
            SizeF sf = context.g.MeasureString(Text, f);
            Bounds = new Rectangle(0, 0, (int)sf.Width + 1 + bx + bw, (int)sf.Height + 1 + by + bh);

            base.MeasureRect(context, maxWidth);
        }
    }
}
