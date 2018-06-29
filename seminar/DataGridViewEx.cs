using System;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace seminar
{
    class DataGridViewEx : DataGridView
    {
        SolidBrush solidBrush;
        public DataGridViewEx()
        {
            solidBrush = new SolidBrush(this.RowHeadersDefaultCellStyle.ForeColor);
        }
        protected override void OnRowPostPaint(DataGridViewRowPostPaintEventArgs e)
        {
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, solidBrush, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + 5);
            base.OnRowPostPaint(e);
        }
    }
}
