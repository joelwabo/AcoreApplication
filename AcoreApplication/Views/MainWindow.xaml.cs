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
            Messenger.Default.Register<Process>(this, AfficherListRecette);
            Messenger.Default.Register<ObservableCollection<Segment>>(this, CreateSegmentChart);
        }

        private void Machine1_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard sb = this.Machine1.FindResource("Storyboard1") as Storyboard;            
            sb.Begin();
        }

        private void AfficherListRecette(Process process)
        {
            this.ProcessMenuItem.Header = process.Nom;
            this.ListRecetteDataGrid.ItemsSource = process.Recettes;
        }

        private void CreateSegmentChart(ObservableCollection<Segment> segments)
        {
            SeriesCollection seriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "V",
                    Values = new ChartValues<int> {segments[0].ConsigneDepartV, segments[0].ConsigneArriveeV},
                    PointGeometry = null
                },
                new LineSeries
                {
                    Title = "A",
                    Values = new ChartValues<int> {segments[0].ConsigneDepartA, segments[0].ConsigneArriveeA},
                    PointGeometry = null
                }
            };
            for(int i = 1; i<segments.Count; i++)
            {
                seriesCollection[0].Values.Add(segments[i].ConsigneDepartV);
                seriesCollection[0].Values.Add(segments[i].ConsigneArriveeV);
                seriesCollection[1].Values.Add(segments[i].ConsigneDepartA);
                seriesCollection[1].Values.Add(segments[i].ConsigneArriveeA);
            }
            this.SegmentChart.Series = seriesCollection;
        }

    }
}
