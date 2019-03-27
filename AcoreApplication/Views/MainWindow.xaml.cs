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

            Messenger.Default.Register<Process>(this, AfficherListRecette);
            Messenger.Default.Register<ObservableCollection<Segment>>(this, CreateSegmentChart);
            Messenger.Default.Register<DataService.Historique>(this, CreateHistoriqueChart);
            Messenger.Default.Register<ObservableCollection<Registre>>(this, AfficherListRegistre);
        }
        
        private void Machine1_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard sb = this.Machine1.FindResource("Storyboard1") as Storyboard;
            sb.Begin();
        }

        private void AfficherListRecette(Process process)
        {
            this.ListRecetteDataGrid.ItemsSource = process.Recettes;

        }

        private void AfficherListRegistre(ObservableCollection<Registre> registre)
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
                    PointGeometrySize = 15
                },
                new LineSeries
                {
                    Title = "A",
                    Values = new ChartValues<ObservablePoint>(),
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15
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
                    seriesCollection[1].Values.Add(new ObservablePoint(i, (double)data.ConsineA));
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

    }
}
