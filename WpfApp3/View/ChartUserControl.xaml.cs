using System;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveCharts;
using LiveCharts.Wpf;
using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using Axis = LiveChartsCore.SkiaSharpView.Axis;

namespace WpfApp3.View;

public partial class ChartUserControl {
  private static readonly SKColor s_blue = new(25, 118, 210);
  private static readonly SKColor s_red = new(229, 57, 53);
  private static readonly SKColor s_yellow = new(198, 167, 0);
  public SeriesCollection SeriesCollection { get; set; }
  public string[] Labels { get; set; }
  public Func<double, string> YFormatter { get; set; }

  public ChartUserControl() {
    InitializeComponent();
    SeriesCollection = new SeriesCollection {
      new LineSeries {
        Title = "Series 1",
        Values = new ChartValues<double> {4, 6, 5, 2, 4}
      },
      new LineSeries {
        Title = "Series 2",
        Values = new ChartValues<double> {6, 7, 3, 4, 6},
        PointGeometry = null
      },
      new LineSeries {
        Title = "Series 3",
        Values = new ChartValues<double> {4, 2, 7, 2, 7},
        PointGeometry = DefaultGeometries.Square,
        PointGeometrySize = 15
      }
    };

    Labels = new[] {"Jan", "Feb", "Mar", "Apr", "May"};
    YFormatter = value => value.ToString("C");

    //modifying the series collection will animate and update the chart
    SeriesCollection.Add(new LineSeries {
      Title = "Series 4",
      Values = new ChartValues<double> {5, 3, 2, 4},
      LineSmoothness = 0, //0: straight lines, 1: really smooth lines
      PointGeometry = Geometry.Parse("m 25 70.36218 20 -28 -20 22 -8 -6 z"),
      PointGeometrySize = 50,
      PointForeground = Brushes.Gray
    });

    //modifying any series values will also animate and update the chart
    SeriesCollection[3].Values.Add(5d);

    DataContext = this;
  }

  public ISeries[] TodosLosGraficos { get; set; } = {
    new LineSeries<double> {
      LineSmoothness = 1,
      Name = "Entradas (posts)",
      Values = new double[] {20, 17, 7, 5, 17, 3, 5, 1, 7, 4, 1, 2, 3, 5, 4, 6, 1, 1, 2, 2, 2},
      Stroke = new SolidColorPaint(s_blue, 2),
      GeometrySize = 10,
      GeometryStroke = new SolidColorPaint(s_blue, 2),
      Fill = null,
      ScalesYAt = 0 // it will be scaled at the Axis[0] instance 
    },
    new LineSeries<double> {
      Name = "Tens 2",
      Values = new double[] {11, 12, 13, 10, 13},
      Stroke = new SolidColorPaint(s_blue, 2),
      GeometrySize = 10,
      GeometryStroke = new SolidColorPaint(s_blue, 2),
      Fill = null,
      ScalesYAt = 0 // it will be scaled at the Axis[0] instance 
    },
    new LineSeries<double> {
      Name = "Hundreds",
      Values = new double[] {533, 586, 425, 579, 518},
      Stroke = new SolidColorPaint(s_red, 2),
      GeometrySize = 10,
      GeometryStroke = new SolidColorPaint(s_red, 2),
      Fill = null,
      ScalesYAt = 1 // it will be scaled at the YAxes[1] instance 
    },
    new LineSeries<double> {
      Name = "Thousands",
      Values = new double[] {5493, 7843, 4368, 9018, 3902},
      Stroke = new SolidColorPaint(s_yellow, 2),
      GeometrySize = 10,
      GeometryStroke = new SolidColorPaint(s_yellow, 2),
      Fill = null,
      ScalesYAt = 2 // it will be scaled at the YAxes[2] instance 
    }
  };

  public Axis[] XAxes { get; set; } = {
    new Axis {
      Name = "Meses",
      NamePaint = new SolidColorPaint {Color = SKColors.Black},
      // Use the labels property for named or static labels 
      Labels = new string[] {
        "Ene-14", "Feb-14", "Mar-14", "Abr-14", "May-14", "Jun-14", "Jul-14", "Ago-14", "Sep-14", "Oct-14", "Nov-14",
        "Dic-14", "Ene-15", "Feb-15", "Mar-15", "Abr-15", "May-15", "Jun-15", "Jul-15", "Ago-15", "Sep-15", "Oct-15",
        "Nov-15", "Dic-15", "Ene-16", "Feb-16", "Mar-16", "Abr-16", "May-16", "Jun-16", "Jul-16", "Ago-16", "Sep-16",
        "Oct-16", "Nov-16", "Dic-16", "Ene-17", "Feb-17", "Mar-17", "Abr-17", "May-17", "Jun-17", "Jul-17", "Ago-17",
        "Sep-17", "Oct-17", "Nov-17", "Dic-17", "Ene-18", "Feb-18", "Mar-18", "Abr-18", "May-18", "Jun-18", "Jul-18",
        "Ago-18", "Sep-18", "Oct-18", "Nov-18", "Dic-18"
      },
      LabelsRotation = 15
    }
  };

  public ICartesianAxis[] YAxes { get; set; } = {
    new Axis // the "units" and "tens" series will be scaled on this axis
    {
      Name = "Entradas (posts)",
      NameTextSize = 14,
      NamePaint = new SolidColorPaint(s_blue),
      TextSize = 12,
      LabelsPaint = new SolidColorPaint(s_blue),
    },
    new Axis // the "hundreds" series will be scaled on this axis
    {
      Name = "Hundreds",
      NameTextSize = 14,
      NamePaint = new SolidColorPaint(s_red),
      TextSize = 12,
      LabelsPaint = new SolidColorPaint(s_red),
      ShowSeparatorLines = false,
      Position = LiveChartsCore.Measure.AxisPosition.End
    },
    new Axis // the "thousands" series will be scaled on this axis
    {
      Name = "Thousands",
      NameTextSize = 14,
      NamePaint = new SolidColorPaint(s_yellow),
      TextSize = 12,
      LabelsPaint = new SolidColorPaint(s_yellow),
      ShowSeparatorLines = false,
      Position = LiveChartsCore.Measure.AxisPosition.End
    }
  };
}