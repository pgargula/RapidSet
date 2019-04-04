using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


public class TransparentRichTextBox : RichTextBox
{
    public TransparentRichTextBox()
    {
        base.ScrollBars = RichTextBoxScrollBars.None;
    }

    override protected CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.ExStyle |= 0x20;
            return cp;
        }
    }

    override protected void OnPaintBackground(PaintEventArgs e)
    {
    }
}

