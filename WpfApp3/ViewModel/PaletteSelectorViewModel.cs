﻿using System;
using System.Collections.Generic;
using System.Windows.Input;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;

namespace WpfApp3.ViewModel;

public class PaletteSelectorViewModel : ViewModelBase {
  public PaletteSelectorViewModel() {
    Swatches = new SwatchesProvider().Swatches;
  }

  public IEnumerable<Swatch> Swatches { get; }

  public ICommand ApplyPrimaryCommand { get; } = new AnotherCommandImplementation(o => ApplyPrimary((Swatch) o!));

  public ICommand ApplyAccentCommand { get; } = new AnotherCommandImplementation(o => ApplyAccent((Swatch) o!));


  private static void ApplyPrimary(Swatch swatch) {
    //ModifyTheme(theme => theme.SetPrimaryColor(swatch.ExemplarHue.Color));
  }

  private static void ApplyAccent(Swatch swatch) {
    /*if (swatch is {AccentExemplarHue: not null})
      ModifyTheme(theme => theme.SetSecondaryColor(swatch.AccentExemplarHue.Color));*/
  }

  /*private static void ModifyTheme(Action<ITheme> modificationAction) {
    var paletteHelper = new PaletteHelper();
    var theme = paletteHelper.GetTheme();

    //modificationAction?.Invoke(theme);

    paletteHelper.SetTheme(theme);
  }*/
}