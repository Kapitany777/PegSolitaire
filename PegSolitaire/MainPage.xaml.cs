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
    public partial class MainPage : UserControl
    {
        TableMask level = new TableMask();

        PegControl[,] pegs = new PegControl[7, 7];

        List<PegControl> peglist = new List<PegControl>();

        PegControl clickedPeg = null;

        public MainPage()
        {
            InitializeComponent();
        }

        private void Init()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    pegs[i, j] = null;
                }
            }
        }

        private void CreateTable()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (level[i, j] == 1)
                    {
                        TableControl rec = new TableControl();

                        rec.XK = i;
                        rec.YK = j;

                        rec.MouseLeftButtonDown += new MouseButtonEventHandler(rec_MouseLeftButtonDown);

                        rec.SetValue(Grid.RowProperty, i);
                        rec.SetValue(Grid.ColumnProperty, j);

                        GridBoard.Children.Add(rec);
                    }
                }
            }
        }

        private void CreatePegs()
        {
            for (int i = 0; i < 32; i++)
            {
                PegControl peg = new PegControl();

                peglist.Add(peg);
                peg.MouseLeftButtonDown += new MouseButtonEventHandler(p_MouseLeftButtonDown);

                GridBoard.Children.Add(peg);
            }
        }

        private void InstallPegs()
        {
            int pos = 0;

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (level[i, j] == 1)
                    {
                        if (i != 3 || j != 3)
                        {
                            PegControl peg = peglist[pos];

                            peg.XK = i;
                            peg.YK = j;

                            peg.Visibility = Visibility.Visible;
                            peg.ChangeColor(false);

                            peg.SetValue(Grid.RowProperty, i);
                            peg.SetValue(Grid.ColumnProperty, j);


                            pegs[i, j] = peg;

                            pos++;
                        }
                    }
                }
            }
        }

        private int NumMoves(bool debug = false)
        {
            int l = 0;

            try
            {
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        if (pegs[i, j] != null)
                        {
                            if (debug) pegs[i, j].ChangeColor(false);

                            if ((i > 1) && (level[i - 1, j] == 1) && (level[i - 2, j] == 1) && (pegs[i - 1, j] != null) && (pegs[i - 2, j] == null))
                            {
                                l++;
                                if (debug) pegs[i, j].ChangeColor(true);
                            }

                            if ((i < 5) && (level[i + 1, j] == 1) && (level[i + 2, j] == 1) && (pegs[i + 1, j] != null) && (pegs[i + 2, j] == null))
                            {
                                l++;
                                if (debug) pegs[i, j].ChangeColor(true);
                            }

                            if ((j > 1) && (level[i, j - 1] == 1) && (level[i, j - 2] == 1) && (pegs[i, j - 1] != null) && (pegs[i, j - 2] == null))
                            {
                                l++;
                                if (debug) pegs[i, j].ChangeColor(true);
                            }

                            if ((j < 5) && (level[i, j + 1] == 1) && (level[i, j + 2] == 1) && (pegs[i, j + 1] != null) && (pegs[i, j + 2] == null))
                            {
                                l++;
                                if (debug) pegs[i, j].ChangeColor(true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return l;
        }

        private int NumPegs()
        {
            int sum = 0;

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (pegs[i, j] != null) sum++;
                }
            }

            return sum;
        }

        private void StartGame()
        {
            Init();

            InstallPegs();
            clickedPeg = null;

            TextBlockMessage.Text = "";
        }

        private void CheckGame()
        {
            if (NumPegs() == 1)
            {
                TextBlockMessage.Text = "Congratulations! You win!";
            }
            else if (NumMoves() == 0)
            {
                TextBlockMessage.Text = "Sorry, no more moves left...";
            }
        }

        void rec_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CheckGame();

            if (clickedPeg != null)
            {
                TableControl rec = sender as TableControl;

                if (pegs[rec.XK, rec.YK] == null)
                {
                    if ((clickedPeg.XK == rec.XK && Math.Abs(clickedPeg.YK - rec.YK) == 2) ||
                        (clickedPeg.YK == rec.YK && Math.Abs(clickedPeg.XK - rec.XK) == 2))
                    {
                        int kx = (clickedPeg.XK + rec.XK) / 2;
                        int ky = (clickedPeg.YK + rec.YK) / 2;

                        if (pegs[kx, ky] != null)
                        {
                            pegs[clickedPeg.XK, clickedPeg.YK] = null;

                            pegs[kx, ky].Visibility = Visibility.Collapsed;
                            pegs[kx, ky] = null;

                            clickedPeg.XK = rec.XK;
                            clickedPeg.YK = rec.YK;

                            pegs[rec.XK, rec.YK] = clickedPeg;

                            clickedPeg.SetValue(Grid.RowProperty, rec.XK);
                            clickedPeg.SetValue(Grid.ColumnProperty, rec.YK);

                            clickedPeg.ChangeColor(false);

                            clickedPeg = null;
                        }
                    }
                }
            }

            CheckGame();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CreateTable();
            CreatePegs();

            StartGame();
        }

        void p_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PegControl peg = sender as PegControl;

            if (clickedPeg == null)
            {
                peg.ChangeColor(true);

                clickedPeg = peg;
            }
            else
            {
                if (clickedPeg == peg)
                {
                    peg.ChangeColor(false);

                    clickedPeg = null;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }
    }
}
