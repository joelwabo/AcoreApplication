using AcoreApplication.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Dapper;
using System.Windows.Media.Animation;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using System.Threading;
using System.Globalization;

namespace AcoreApplication.Views
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.ProcessComboBox.SelectedIndex = 0;

            Messenger.Default.Register<DataService.Process>(this, AfficherListRecette);
            Messenger.Default.Register<ObservableCollection<Segment>>(this, CreateSegmentChart);
            Messenger.Default.Register<DataService.Historique>(this, CreateHistoriqueChart);
            Messenger.Default.Register<ObservableCollection<DataService.Registre>>(this, AfficherListRegistre);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentCulture;

            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
            AcoreApplication.Resources.Lang.Wrapper.ChangeCulture(Thread.CurrentThread.CurrentUICulture);

            hideColumns();

        }

        private void hideColumns()
        {

            MachineDataGrid.Columns[12].Visibility = Visibility.Collapsed; //TONms

            MachineDataGrid.Columns[13].Visibility = Visibility.Collapsed; //TOFFms

            MachineDataGrid.Columns[15].Visibility = Visibility.Collapsed; //Duree tempo

            MachineDataGrid.Columns[16].Visibility = Visibility.Collapsed; //Duree restante
            MachineDataGrid.Columns[18].Visibility = Visibility.Collapsed; //Duree rampe
        }

        private void Spanish_click(object sender, RoutedEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");
            AcoreApplication.Resources.Lang.Wrapper.ChangeCulture(Thread.CurrentThread.CurrentUICulture);
        }
        private void English_click(object sender, RoutedEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            AcoreApplication.Resources.Lang.Wrapper.ChangeCulture(Thread.CurrentThread.CurrentUICulture);
        }
        private void French_click(object sender, RoutedEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
            AcoreApplication.Resources.Lang.Wrapper.ChangeCulture(Thread.CurrentThread.CurrentUICulture);
        }
        /*
        private void Machine1_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard sb = this.Machine1.FindResource("Storyboard1") as Storyboard;
            sb.Begin();
        }*/

        private void AfficherListRecette(DataService.Process process)
        {
            this.ListRecetteDataGrid.ItemsSource = process.Recettes;

        }

        private void AfficherListRegistre(ObservableCollection<DataService.Registre> registre)
        {
            this.ListRecetteDataGrid.ItemsSource = registre;

        }

        private void CreateSegmentChart(ObservableCollection<Segment> segments)
        {
            SeriesCollection seriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "V",
                    Values = new ChartValues<ObservablePoint>(),
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15,
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "A",
                    Values = new ChartValues<ObservablePoint>(),
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15,
                    LineSmoothness = 0
                }
            };
            int i = 0;
            foreach(Segment segment in segments)
            {
                seriesCollection[0].Values.Add(new ObservablePoint(i, segment.ConsigneDepartV));
                seriesCollection[0].Values.Add(new ObservablePoint(i+1, segment.ConsigneArriveeV));
                seriesCollection[1].Values.Add(new ObservablePoint(i, segment.ConsigneDepartA));
                seriesCollection[1].Values.Add(new ObservablePoint(i+1, segment.ConsigneArriveeA));
                i++;
            }
            this.SegmentChart.Series = seriesCollection;
        }

        private void CreateHistoriqueChart(DataService.Historique historique)
        {
            SeriesCollection seriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "V",
                    Values = new ChartValues<ObservablePoint>(),
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 1
                },
                new LineSeries
                {
                    Title = "A",
                    Values = new ChartValues<ObservablePoint>(),
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 1
                },
                new LineSeries
                {
                    Title = "U",
                    Values = new ChartValues<ObservablePoint>(),
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 1
                },
                new LineSeries
                {
                    Title = "I",
                    Values = new ChartValues<ObservablePoint>(),
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 1
                }
            };
            int i = 0;
            if((historique.HistoriqueData != null)&& (historique.HistoriqueData.Count>0))
                foreach (DataService.HistoriqueData data in historique.HistoriqueData)
                {
                    seriesCollection[0].Values.Add(new ObservablePoint(i, (double)data.ConsigneV));
                    seriesCollection[1].Values.Add(new ObservablePoint(i, (double)data.ConsigneA));
                    i++;
                }
            i = 0;
            if ((historique.Recette2.Segments != null)&& (historique.Recette2.Segments.Count>0))
                foreach (Segment segment in historique.Recette2.Segments)
                {
                    seriesCollection[2].Values.Add(new ObservablePoint(i, segment.ConsigneDepartV));
                    seriesCollection[2].Values.Add(new ObservablePoint(i + 1, segment.ConsigneArriveeV));
                    seriesCollection[3].Values.Add(new ObservablePoint(i, segment.ConsigneDepartA));
                    seriesCollection[3].Values.Add(new ObservablePoint(i + 1, segment.ConsigneArriveeA));
                    i++;
                }
            this.HistoriqueChart.Series = seriesCollection;
        }



        private void DataGrid_MouseRightButtonUp(object sender,
                                        MouseButtonEventArgs e)
        {
            //TODO: Left show down the item
            //TODO: Right show dropdown
            //listBox.Items.Add( new TextBox());
            int i = 1;
        }

        private void Ton_click(object sender, RoutedEventArgs e)
        {
            string headerText = (string)TonContextMenu.Header;
            if (headerText == "Show Tonms")
            {

                MachineDataGrid.Columns[12].Visibility = Visibility.Visible;
                TonContextMenu.Header="Hide Tonms";
            }
            else if (headerText == "Hide Tonms")
            {
                MachineDataGrid.Columns[12].Visibility = Visibility.Hidden;
                TonContextMenu.Header = "Show Tonms";
            }

        }

        private void Toff_click(object sender, RoutedEventArgs e)
        {

            string headerText = (string)ToffContextMenu.Header;
            if (headerText == "Show Toffms")
            {

                MachineDataGrid.Columns[13].Visibility = Visibility.Visible;
                ToffContextMenu.Header = "Hide Toffms";
            }
            else if (headerText == "Hide Toffms")
            {
                MachineDataGrid.Columns[13].Visibility = Visibility.Hidden;
                ToffContextMenu.Header = "Show Toffms";
            }
        }

        private void DureeTempo_click(object sender, RoutedEventArgs e)
        {

            string headerText = (string)DureeTempoContextMenu.Header;
            if (headerText == "Show Durée tempo")
            {

                MachineDataGrid.Columns[15].Visibility = Visibility.Visible;
                DureeTempoContextMenu.Header = "Hide Durée tempo";
            }
            else if (headerText == "Hide Durée tempo")
            {
                MachineDataGrid.Columns[15].Visibility = Visibility.Hidden;
                DureeTempoContextMenu.Header = "Show Durée tempo";
            }
        }

        private void DureeRestante_click(object sender, RoutedEventArgs e)
        {

            string headerText = (string)DureeRestanteContextMenu.Header;
            if (headerText == "Show Durée restante")
            {

                MachineDataGrid.Columns[16].Visibility = Visibility.Visible;
                DureeRestanteContextMenu.Header = "Hide Durée restante";
            }
            else if (headerText == "Hide Durée restante")
            {
                MachineDataGrid.Columns[16].Visibility = Visibility.Hidden;
                DureeRestanteContextMenu.Header = "Show Durée restante";
            }
        }

        private void DureeRampe_click(object sender, RoutedEventArgs e)
        {

            string headerText = (string)DureeRampeContextMenu.Header;
            if (headerText == "Show Durée rampe")
            {

                MachineDataGrid.Columns[18].Visibility = Visibility.Visible;
                DureeRampeContextMenu.Header = "Hide Durée rampe";
            }
            else if (headerText == "Hide Durée rampe")
            {
                MachineDataGrid.Columns[18].Visibility = Visibility.Hidden;
                DureeRampeContextMenu.Header = "Show Durée rampe";
            }
        }

    }
}
