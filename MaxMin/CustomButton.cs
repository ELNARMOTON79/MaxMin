using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class CustomButton : Button
{
    public CustomButton()
    {
        this.FlatStyle = FlatStyle.Flat;
        this.FlatAppearance.BorderSize = 0;
        this.BackColor = Color.Transparent;
        this.ForeColor = Color.White;
        this.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
        this.TextAlign = ContentAlignment.MiddleLeft;
        this.Padding = new Padding(20, 0, 0, 0);
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
        base.OnPaint(pevent);
        Graphics g = pevent.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        // Fondo con gradiente y bordes redondeados
        GraphicsPath path = new GraphicsPath();
        int radius = 20;
        Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
        path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
        path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
        path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
        path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
        path.CloseAllFigures();

        using (LinearGradientBrush brush = new LinearGradientBrush(rect,
                                                                  Color.FromArgb(60, 80, 150),
                                                                  Color.FromArgb(100, 50, 200),
                                                                  LinearGradientMode.Horizontal))
        {
            g.FillPath(brush, path);
        }

        // Dibuja el nombre y la descripción separados
        var titleFont = new Font("Segoe UI", 14F, FontStyle.Bold);
        var descFont = new Font("Segoe UI", 10F, FontStyle.Regular);
        var textBrush = new SolidBrush(this.ForeColor);

        // Nombre (Text)
        var titleRect = new Rectangle(this.Padding.Left, 18, this.Width - this.Padding.Horizontal, 28);
        var sfTitle = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near };
        g.DrawString(this.Text, titleFont, textBrush, titleRect, sfTitle);

        // Descripción (Tag)
        string descText = this.Tag?.ToString() ?? "";
        var descRect = new Rectangle(this.Padding.Left, 50, this.Width - this.Padding.Horizontal - 10, this.Height - 60);
        var sfDesc = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near };
        g.DrawString(descText, descFont, textBrush, descRect, sfDesc);
    }
}