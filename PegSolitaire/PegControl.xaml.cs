using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PegSolitaire
{
    public partial class PegControl : UserControl
    {
        public Brush NormalColor { get; set; }
        public Brush ClickedColor { get; set; }

        public int XK { get; set; }
        public int YK { get; set; }

        private bool live;

        public bool Live
        {
            get { return live; }

            set
            {
                live = value;

                this.Visibility = (live ? Visibility.Visible : Visibility.Collapsed);
            }
        }

        public PegControl()
        {
            InitializeComponent();

            NormalColor = new SolidColorBrush(Colors.Yellow);
            ClickedColor = new SolidColorBrush(Color.FromArgb(0xFF, 0x57, 0x95, 0xDB));

            XK = 0;
            YK = 0;
            Live = true;
        }

        public void ChangeColor(bool clicked)
        {
            clicked = !clicked;

            if (clicked)
            {
                EllipsePeg.Fill = ClickedColor;
            }
            else
            {
                EllipsePeg.Fill = NormalColor;
            }
        }
    }
}
