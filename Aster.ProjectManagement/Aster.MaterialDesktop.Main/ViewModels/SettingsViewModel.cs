using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aster.MaterialDesktop.Main.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        public IEnumerable<Swatch> Swatches { get; }

        /// <summary>
        /// 电灯开关开关
        /// </summary>
        public DelegateCommand<bool?> DarkLightCommand { get; private set; }


        /// <summary>
        /// 主色
        /// </summary>
        public DelegateCommand<Swatch> ApplyPrimaryCommand { get; private set; }

        /// <summary>
        /// 强调色
        /// </summary>
        public DelegateCommand<Swatch> ApplyAccentCommand { get; private set; }

        public SettingsViewModel()
        {
            DarkLightCommand = new DelegateCommand<bool?>(DoDarkLightCommand);
            ApplyPrimaryCommand = new DelegateCommand<Swatch>(ApplyPrimary);
            ApplyAccentCommand = new DelegateCommand<Swatch>(ApplyAccent);
            Swatches = new SwatchesProvider().Swatches;

        }

        private void DoDarkLightCommand(bool? isDark)
        {
            if (isDark.HasValue)
            {
                new PaletteHelper().SetLightDark(isDark.Value);
            }
        }

        private static void ApplyPrimary(Swatch swatch)
        {
            new PaletteHelper().ReplacePrimaryColor(swatch);
        }

        private static void ApplyAccent(Swatch swatch)
        {
            new PaletteHelper().ReplaceAccentColor(swatch);
        }
    }
}
