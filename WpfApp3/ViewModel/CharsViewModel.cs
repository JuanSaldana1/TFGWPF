using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace WpfApp3.ViewModel;

[ObservableObject]
public partial class CharsViewModel {
  public ISeries[] Series { get; set; } = {
    new LineSeries<double> {
      Values = new double[] {2, 1, 3, 5, 3, 4, 6},
      Fill = null
    }
  };
}