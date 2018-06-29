using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace seminar
{
    public partial class ListBoxEx : ListBox
    {
        public ListBoxEx()
        {
           this.DrawMode= DrawMode.OwnerDrawFixed;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {

            e.DrawBackground();

            Brush myBrush = Brushes.Black;

            switch (e.Index%7)
            {
                case 0:
                    myBrush = Brushes.Red;
                    break;
                case 1:
                    myBrush = Brushes.Orange;
                    break;
                case 2:
                    myBrush = Brushes.Gold;
                    break;
                case 3:
                    myBrush = Brushes.Green;
                    break;
                case 4:
                    myBrush = Brushes.Blue;
                    break;
                case 5:
                    myBrush = Brushes.Purple;
                    break;
                case 6:
                    myBrush = Brushes.Pink;
                    break;
            }

            e.Graphics.DrawString(this.Items[e.Index].ToString(),e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);

            e.DrawFocusRectangle();

            base.OnDrawItem(e);
        }
        

    }
}
