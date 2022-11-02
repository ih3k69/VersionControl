﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace _7HET_ih3k69.Entities
{
    internal class Ball : Label
    {
        public Ball()
        {
            AutoSize = false;
            Width = 50;
            Height = Width;
            Paint += Ball_Paint;
        }

        private void Ball_Paint(object sender, PaintEventArgs e)
        {
            DrawImage(e.Graphics);
        }
        protected void DrawImage(Graphics graf)
        {
            graf.FillEllipse(new SolidBrush(Color.Blue), 0, 0, Width, Height);
        }
        public void MoveBall()
        {
            Left += 1;
        }
    }
    

}
