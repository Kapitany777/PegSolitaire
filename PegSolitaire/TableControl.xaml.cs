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
    public partial class TableControl : UserControl
    {
        public int XK { get; set; }
        public int YK { get; set; }
        
        public TableControl()
        {
            InitializeComponent();

            XK = 0;
            YK = 0;
        }
    }
}
