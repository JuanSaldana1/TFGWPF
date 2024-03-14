using System;
using System.Collections.Generic;
using System.Linq;
using MaterialDesignThemes.Wpf;

namespace WpfApp3.ViewModel;

public class ThemeSettingsViewModel : ViewModelBase {
  private ColorSelection _colorSelectionValue;

  private Contrast _contrastValue;

  private float _desiredContrastRatio = 4.5f;

  private bool _isColorAdjusted;

  private bool _isDarkTheme;

  public ThemeSettingsViewModel() {
    var paletteHelper = new PaletteHelper();
    var theme = paletteHelper.GetTheme();

    IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark;

    if (theme is Theme internalTheme) {
      _isColorAdjusted = internalTheme.ColorAdjustment is not null;

      var colorAdjustment = internalTheme.ColorAdjustment ?? new ColorAdjustment();
      _desiredContrastRatio = colorAdjustment.DesiredContrastRatio;
      _contrastValue = colorAdjustment.Contrast;
      _colorSelectionValue = colorAdjustment.Colors;
    }

    if (paletteHelper.GetThemeManager() is { } themeManager)
      themeManager.ThemeChanged += (_, e) => IsDarkTheme = e.NewTheme?.GetBaseTheme() == BaseTheme.Dark;
  }

  public bool IsDarkTheme {
    get => _isDarkTheme;
    set {
      if (SetProperty(ref _isDarkTheme, value))
        ModifyTheme(theme => theme.SetBaseTheme(BaseTheme.Dark));
    }
  }

  public bool IsColorAdjusted {
    get => _isColorAdjusted;
    set {
      if (SetProperty(ref _isColorAdjusted, value))
        ModifyTheme(theme => {
          if (theme is Theme internalTheme)
            internalTheme.ColorAdjustment = value
              ? new ColorAdjustment {
                DesiredContrastRatio = DesiredContrastRatio,
                Contrast = ContrastValue,
                Colors = ColorSelectionValue
              }
              : null;
        });
    }
  }

  public float DesiredContrastRatio {
    get => _desiredContrastRatio;
    set {
      if (SetProperty(ref _desiredContrastRatio, value))
        ModifyTheme(theme => {
          if (theme is Theme internalTheme && internalTheme.ColorAdjustment != null)
            internalTheme.ColorAdjustment.DesiredContrastRatio = value;
        });
    }
  }

  public IEnumerable<Contrast> ContrastValues => Enum.GetValues(typeof(Contrast)).Cast<Contrast>();

  public Contrast ContrastValue {
    get => _contrastValue;
    set {
      if (SetProperty(ref _contrastValue, value))
        ModifyTheme(theme => {
          if (theme is Theme internalTheme && internalTheme.ColorAdjustment != null)
            internalTheme.ColorAdjustment.Contrast = value;
        });
    }
  }

  public IEnumerable<ColorSelection> ColorSelectionValues =>
    Enum.GetValues(typeof(ColorSelection)).Cast<ColorSelection>();

  public ColorSelection ColorSelectionValue {
    get => _colorSelectionValue;
    set {
      if (SetProperty(ref _colorSelectionValue, value))
        ModifyTheme(theme => {
          if (theme is Theme internalTheme && internalTheme.ColorAdjustment != null)
            internalTheme.ColorAdjustment.Colors = value;
        });
    }
  }

  private static void ModifyTheme(Action<Theme> modificationAction) {
    var paletteHelper = new PaletteHelper();
    var theme = paletteHelper.GetTheme();

    modificationAction.Invoke(theme);

    paletteHelper.SetTheme(theme);
  }
}