using System;
using System.Globalization;
using System.Windows;

using FilterStudio.Windows;
using FilterStudio.Windows.PipelineSteps;

namespace FilterStudio.Consts {
    /// <summary>
    /// Handle fetching of language strings and translations.
    /// </summary>
    static class Language {
        /// <summary>
        /// Get the <see cref="MainWindow"/>'s translation.
        /// </summary>
        public static ResourceDictionary GetMainWindowStrings() => mainWindowCache ??= GetFor("MainWindowStrings");

        /// <summary>
        /// Get the <see cref="ConvolutionLengthDialog"/>'s translation.
        /// </summary>
        public static ResourceDictionary GetConvolutionLengthDialogStrings() =>
            convolutionLengthDialogCache ??= GetFor("ConvolutionLengthDialogStrings");

        /// <summary>
        /// Get the <see cref="CrossoverDialog"/>'s translation.
        /// </summary>
        public static ResourceDictionary GetCrossoverDialogStrings() => crossoverDialogCache ??= GetFor("CrossoverDialogStrings");

        /// <summary>
        /// Get the <see cref="RenameDialog"/>'s translation.
        /// </summary>
        public static ResourceDictionary GetRenameDialogStrings() => renameDialogCache ??= GetFor("RenameDialogStrings");

        /// <summary>
        /// Get the translation of a resource file in the user's language, or in English if a translation couldn't be found.
        /// </summary>
        static ResourceDictionary GetFor(string resource) {
            string culture = Settings.Default.language;
            if (string.IsNullOrEmpty(culture)) {
                culture = CultureInfo.CurrentUICulture.Name;
            } else if (culture == "en-US") {
                culture = string.Empty;
            }

            if (Array.BinarySearch(supported, culture) >= 0) {
                resource += '.' + culture;
            }
            else if (culture.Length > 0 && culture.Contains('-')) {
                string languagePrefix = culture.Substring(0, culture.IndexOf('-') + 3); // 获取如 "zh-CN"
                if (Array.BinarySearch(supported, languagePrefix) >= 0) {
                    resource += '.' + languagePrefix;
                }
            }
            
            return new() {
                Source = new Uri($";component/Resources/{resource}.xaml", UriKind.RelativeOrAbsolute)
    };
}

        /// <summary>
        /// Languages supported that are not the default English.
        /// </summary>
        static readonly string[] supported = ["hu-HU","zh-CN"];

        /// <summary>
        /// The loaded translation of the <see cref="MainWindow"/> for reuse.
        /// </summary>
        static ResourceDictionary mainWindowCache;

        /// <summary>
        /// The loaded translation of the <see cref="ConvolutionLengthDialog"/> for reuse.
        /// </summary>
        static ResourceDictionary convolutionLengthDialogCache;

        /// <summary>
        /// The loaded translation of the <see cref="CrossoverDialog"/> for reuse.
        /// </summary>
        static ResourceDictionary crossoverDialogCache;

        /// <summary>
        /// The loaded translation of the <see cref="RenameDialog"/> for reuse.
        /// </summary>
        static ResourceDictionary renameDialogCache;
    }
}
