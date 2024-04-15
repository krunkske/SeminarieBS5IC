using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SudokuGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        sudokuSolver solver = new sudokuSolver();
        TextBox[,] sudokuWaardes = new TextBox[9, 9];
        int[,] sudoku =
        {
            { 5, 3, 0, 0, 7, 0, 0, 0, 0 },
            { 6, 0, 0, 1, 9, 5, 0, 0, 0 },
            { 0, 9, 8, 0, 0, 0, 0, 6, 0 },
            { 8, 0, 0, 0, 6, 0, 0, 0, 3 },
            { 4, 0, 0, 8, 0, 3, 0, 0, 1 },
            { 7, 0, 0, 0, 2, 0, 0, 0, 6 },
            { 0, 6, 0, 0, 0, 0, 2, 8, 0 },
            { 0, 0, 0, 4, 1, 9, 0, 0, 5 },
            { 0, 0, 0, 0, 8, 0, 0, 7, 9 }
        };
        bool[,] prePlaced;

        public MainWindow()
        {
            InitializeComponent();


            for (int rij = 0; rij < 9; rij++)
            {
                for (int kol = 0; kol < 9; kol++)
                {
                    TextBox sudokuWaarde = new TextBox();
                    sudokuWaardes[rij, kol] = sudokuWaarde;
                    if (sudoku[rij, kol] != 0)
                    {
                        sudokuWaarde.Text = Convert.ToString(sudoku[rij, kol]);
                        sudokuWaarde.Focusable = false;
                    }
                    else
                    {
                        sudokuWaarde.Text = "";
                    }
                    
                    sudokuWaarde.HorizontalContentAlignment = HorizontalAlignment.Center;
                    sudokuWaarde.VerticalContentAlignment = VerticalAlignment.Center;
                    sudokuWaarde.BorderBrush = Brushes.Black;
                    
                    sudokuWaarde.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);

                    sudokuWaarde.LostFocus += TextBox_LostFocus;
                    sudokuWaarde.KeyDown += TextBox_KeyDown;

                    if (rij % 3 == 0)
                    {
                        sudokuWaarde.BorderThickness = new Thickness(0.5, 2, 0.5, 0.5);
                    }

                    else if (rij == 8)
                    {
                        sudokuWaarde.BorderThickness = new Thickness(0.5, 0.5, 0.5, 2);
                    }

                    if (kol % 3 == 0)
                    {
                        if (rij % 3 == 0)
                        {
                            sudokuWaarde.BorderThickness = new Thickness(2, 2, 0.5, 0.5);
                        }
                        else if (rij == 8)
                        {
                            sudokuWaarde.BorderThickness = new Thickness(2, 0.5, 0.5, 2);
                        }
                        else
                        {
                            sudokuWaarde.BorderThickness = new Thickness(2, 0.5, 0.5, 0.5);
                        }

                    }

                    else if (kol == 8)
                    {
                        if (rij % 3 == 0)
                        {
                            sudokuWaarde.BorderThickness = new Thickness(0.5, 2, 2, 0.5);
                        }
                        else if (rij == 8)
                        {
                            sudokuWaarde.BorderThickness = new Thickness(0.5, 0.5, 2, 2);
                        }
                        else
                        {
                            sudokuWaarde.BorderThickness = new Thickness(0.5, 0.5, 2, 0.5);
                        }

                    }



                    Grid.SetRow(sudokuWaarde, rij);
                    Grid.SetColumn(sudokuWaarde, kol);

                    SudokuGrid.Children.Add(sudokuWaarde);
                }
            }

            //int rijnummer = Grid.GetRow(Knop1);
            //RowDefinition rowDef = MainGrid.RowDefinitions[rijnummer];

        }

        private void vulIn(TextBox box)
        {
            if (box != null)
            {
                int rij = Grid.GetRow(box);
                int kol = Grid.GetColumn(box);
                if (box.Text.Length == 1 && box.Text[0] > '0' && box.Text[0] <= '9')
                {
                    int waarde = Convert.ToInt32(box.Text);

                    if (solver.isAllesGeldig(rij, kol, waarde, sudoku))
                    {
                        sudoku[rij, kol] = waarde;
                    }
                    else
                    {
                        if (sudoku[rij, kol] != 0)
                        {
                            box.Text = Convert.ToString(sudoku[rij, kol]);
                        }
                        else
                        {
                            box.Text = "";
                        }

                    }
                }
                else
                {
                    if (sudoku[rij, kol] != 0)
                    {
                        box.Text = Convert.ToString(sudoku[rij, kol]);
                    }
                    else
                    {
                        box.Text = "";
                    }
                }
            }
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            vulIn((TextBox)sender);
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                vulIn((TextBox)sender);
            }
        }
    }
}