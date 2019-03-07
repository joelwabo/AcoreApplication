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

    }
}
