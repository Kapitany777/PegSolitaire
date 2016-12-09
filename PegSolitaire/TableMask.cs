using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PegSolitaire
{
    public class TableMask
    {
        private byte[,] level = 
        { 
          { 0, 0, 1, 1, 1, 0, 0 },
          { 0, 0, 1, 1, 1, 0, 0 },
          { 1, 1, 1, 1, 1, 1, 1 },
          { 1, 1, 1, 1, 1, 1, 1 },
          { 1, 1, 1, 1, 1, 1, 1 },
          { 0, 0, 1, 1, 1, 0, 0 },
          { 0, 0, 1, 1, 1, 0, 0 }
        };

        public TableMask()
        {

        }

        public byte this[int x, int y]
        {
            get
            {
                return level[x, y];
            }
        }
    }
}
