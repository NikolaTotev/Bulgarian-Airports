using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace CSV_Generator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StringBuilder currentCSV = new StringBuilder();
        private bool creatingNewCSV = true;
        private int numberOfRows = 0;
        private int numberOfColumns = 0;
        private List<string> columnNames = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_Save_OnClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dia_SelectSaveLocation = new SaveFileDialog();
            dia_SelectSaveLocation.ShowDialog();

            string savePath = dia_SelectSaveLocation.FileName;

            StringBuilder finalCSV = new StringBuilder();

            if (creatingNewCSV)
            {

                finalCSV.Append(
                "Airport Code, Location, Nearest City, Area, Runway Numbers, Runway Length(s), Runway Width(s), Type of pavement, Flights per year\n");
                finalCSV.Append(currentCSV.ToString());
            }
            else
            {
                finalCSV = currentCSV;
            }


            if (string.IsNullOrWhiteSpace(savePath))
            {
                MessageBox.Show($"File has not been saved.", "Save Cancelled", MessageBoxButton.OK, icon: MessageBoxImage.Information);
                return;
            }

            try
            {
                using (StreamWriter outputFile = new StreamWriter(savePath))
                {
                    outputFile.Write(finalCSV);
                }
            }
            catch (IOException exception)
            {
                MessageBox.Show($"Error: Could not write to ${savePath}.\n Please make sure the file isn't open somewhere else", "File Error", MessageBoxButton.OK, icon: MessageBoxImage.Exclamation);
            }

        }

        private void Btn_Add_OnClick(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            bool emptyTextboxDetected = false;
            //Row 1
            if (!string.IsNullOrWhiteSpace(Tb_AirportCode.Text))
            {
                Tb_AirportCode.BorderBrush = Brushes.Transparent;
                sb.Append(Tb_AirportCode.Text);
                sb.Append(",");

            }
            else
            {
                emptyTextboxDetected = true;
                Tb_AirportCode.BorderBrush = (Brush)Application.Current.Resources["WarningRed"];
            }

            if (!string.IsNullOrWhiteSpace(Tb_Location.Text))
            {
                Tb_Location.BorderBrush = Brushes.Transparent;
                sb.Append(Tb_Location.Text);
                sb.Append(",");
            }
            else
            {
                emptyTextboxDetected = true;
                Tb_Location.BorderBrush = (Brush)Application.Current.Resources["WarningRed"];
            }

            if (!string.IsNullOrWhiteSpace(Tb_NearestCity.Text))
            {
                Tb_NearestCity.BorderBrush = Brushes.Transparent;
                sb.Append(Tb_NearestCity.Text);
                sb.Append(",");
            }
            else
            {
                emptyTextboxDetected = true;
                Tb_NearestCity.BorderBrush = (Brush)Application.Current.Resources["WarningRed"];
            }


            //Row 2
            if (!string.IsNullOrWhiteSpace(Tb_Area.Text))
            {
                Tb_Area.BorderBrush = Brushes.Transparent;
                sb.Append(Tb_Area.Text);
                sb.Append(",");
            }
            else
            {
                emptyTextboxDetected = true;
                Tb_Area.BorderBrush = (Brush)Application.Current.Resources["WarningRed"];
            }

            if (!string.IsNullOrWhiteSpace(Tb_RunwayNum.Text))
            {
                Tb_RunwayNum.BorderBrush = Brushes.Transparent;
                sb.Append(Tb_RunwayNum.Text);
                sb.Append(",");
            }
            else
            {
                emptyTextboxDetected = true;
                Tb_RunwayNum.BorderBrush = (Brush)Application.Current.Resources["WarningRed"];
            }

            if (!string.IsNullOrWhiteSpace(Tb_RunwayLen.Text))
            {
                Tb_RunwayLen.BorderBrush = Brushes.Transparent;
                sb.Append(Tb_RunwayLen.Text);
                sb.Append(",");
            }
            else
            {
                emptyTextboxDetected = true;
                Tb_RunwayLen.BorderBrush = (Brush)Application.Current.Resources["WarningRed"];
            }


            //Row 3
            if (!string.IsNullOrWhiteSpace(Tb_RunwayWidth.Text))
            {
                Tb_RunwayWidth.BorderBrush = Brushes.Transparent;
                sb.Append(Tb_RunwayWidth.Text);
                sb.Append(",");
            }
            else
            {
                emptyTextboxDetected = true;
                Tb_RunwayWidth.BorderBrush = (Brush)Application.Current.Resources["WarningRed"];
            }

            if (!string.IsNullOrWhiteSpace(Tb_PavementType.Text))
            {
                Tb_PavementType.BorderBrush = Brushes.Transparent;
                sb.Append(Tb_PavementType.Text);
                sb.Append(",");
            }
            else
            {
                emptyTextboxDetected = true;
                Tb_PavementType.BorderBrush = (Brush)Application.Current.Resources["WarningRed"];
            }

            if (!string.IsNullOrWhiteSpace(Tb_FlightsPerYear.Text))
            {
                Tb_FlightsPerYear.BorderBrush = Brushes.Transparent;
                sb.Append(Tb_FlightsPerYear.Text);
                sb.Append("\n");
            }
            else
            {
                emptyTextboxDetected = true;
                Tb_FlightsPerYear.BorderBrush = (Brush)Application.Current.Resources["WarningRed"];
            }

            if (!emptyTextboxDetected)
            {
                currentCSV.Append(sb.ToString());
                numberOfRows++;
                Lb_RowCountValue.Content = numberOfRows.ToString();
                Tb_AirportCode.Clear();
                Tb_Area.Clear();
                Tb_FlightsPerYear.Clear();
                Tb_Location.Clear();
                Tb_NearestCity.Clear();
                Tb_PavementType.Clear();
                Tb_RunwayLen.Clear();
                Tb_RunwayNum.Clear();
                Tb_RunwayWidth.Clear();
            }
            else
            {
                MessageBox.Show($"Airport not added! \n Missing information", "Missing information", MessageBoxButton.OK, icon: MessageBoxImage.Error);
            }
        }

        private void Tb_AirportCodeChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Tb_AirportCode.Text))
            {
                Tb_AirportCode.BorderBrush = (Brush)Application.Current.Resources["WarningRed"];
            }
            else
            {
                Tb_AirportCode.BorderBrush = Brushes.Transparent;
            }
        }

        private void Tb_NearestCityChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Tb_NearestCity.Text))
            {
                Tb_NearestCity.BorderBrush = (Brush)Application.Current.Resources["WarningRed"];
            }
            else
            {
                Tb_NearestCity.BorderBrush = Brushes.Transparent;
            }
        }

        private void Tb_LocationChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Tb_Location.Text))
            {
                Tb_Location.BorderBrush = (Brush)Application.Current.Resources["WarningRed"];
            }
            else
            {
                Tb_Location.BorderBrush = Brushes.Transparent;
            }
        }

        private void Tb_AreaChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Tb_Area.Text))
            {
                Tb_Area.BorderBrush = (Brush)Application.Current.Resources["WarningRed"];
            }
            else
            {
                Tb_Area.BorderBrush = Brushes.Transparent;
            }
        }

        private void Tb_RunwayNumChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Tb_RunwayNum.Text))
            {
                Tb_RunwayNum.BorderBrush = (Brush)Application.Current.Resources["WarningRed"];
            }
            else
            {
                Tb_RunwayNum.BorderBrush = Brushes.Transparent;
            }
        }

        private void Tb_RunwayLenChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Tb_RunwayLen.Text))
            {
                Tb_RunwayLen.BorderBrush = (Brush)Application.Current.Resources["WarningRed"];
            }
            else
            {
                Tb_RunwayLen.BorderBrush = Brushes.Transparent;
            }
        }

        private void Tb_RunwayWidthsChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Tb_RunwayWidth.Text))
            {
                Tb_RunwayWidth.BorderBrush = (Brush)Application.Current.Resources["WarningRed"];
            }
            else
            {
                Tb_RunwayWidth.BorderBrush = Brushes.Transparent;
            }
        }

        private void Tb_PavementTypeChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Tb_PavementType.Text))
            {
                Tb_PavementType.BorderBrush = (Brush)Application.Current.Resources["WarningRed"];
            }
            else
            {
                Tb_PavementType.BorderBrush = Brushes.Transparent;
            }
        }

        private void Tb_FlightsPerYearChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Tb_FlightsPerYear.Text))
            {
                Tb_FlightsPerYear.BorderBrush = (Brush)Application.Current.Resources["WarningRed"];
            }
            else
            {
                Tb_FlightsPerYear.BorderBrush = Brushes.Transparent;
            }
        }

        private void Btn_Open_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dia_OpenFile = new OpenFileDialog();
            dia_OpenFile.ShowDialog();
            string fileToOpen = dia_OpenFile.FileName;
            creatingNewCSV = false;
            columnNames = File.ReadAllLines(fileToOpen).ToList();

            foreach (var column in columnNames[0].Split(',').ToList())
            {
                Lst_ColNames.Items.Add(column);
            }
            
            numberOfColumns = Lst_ColNames.Items.Count;
            numberOfRows = columnNames.Count();

            Lb_ColumnCountValue.Content = numberOfColumns.ToString();
            Lb_RowCountValue.Content = numberOfRows.ToString();
        }
    }
}
