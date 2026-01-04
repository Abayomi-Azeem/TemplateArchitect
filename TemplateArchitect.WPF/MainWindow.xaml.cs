using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TemplateArchitect.WPF.ViewModels;

namespace TemplateArchitect.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainPageViewModel();

        }

        //private async void Run_Click(object sender, RoutedEventArgs e)
        //{
        //    OutputTextBox.Clear();
        //    RunButton.IsEnabled = false;
        //    try
        //    {
        //        var workingDir = WorkingDirTextBox.Text;
        //        var command = CommandTextBox.Text;

        //        var psi = new ProcessStartInfo
        //        {
        //            FileName = "TemplateArchitect.CLI.exe",
        //            Arguments = $"\"{workingDir}\" \"{command}\"",
        //            UseShellExecute = false,
        //            RedirectStandardError = true,
        //            RedirectStandardOutput = true,
        //            CreateNoWindow = true
        //        };

        //        var process = new Process { StartInfo = psi };

        //        process.OutputDataReceived += (sender, args) =>
        //        {
        //            if (args.Data != null)
        //            {
        //                Dispatcher.Invoke(() =>
        //                            OutputTextBox.AppendText(args.Data + "\n"));
        //            }
        //        };
        //        process.ErrorDataReceived += (sender, args) =>
        //        {
        //            if (args.Data != null)
        //            {
        //                Dispatcher.Invoke(() =>
        //                            OutputTextBox.AppendText("[ERR] " + args.Data + "\n"));
        //            }
        //        };

        //        process.Start();
        //        process.BeginOutputReadLine();
        //        process.BeginErrorReadLine();

        //        await process.WaitForExitAsync();
        //    }
        //    finally
        //    {
        //        RunButton.IsEnabled = true;
        //    }

        //}

        //private async void Browse_Click(object sender, RoutedEventArgs e)
        //{
        //    using var dialog = new FolderBrowserDialog
        //    {
        //        Description = "Select Project Folder",
        //        UseDescriptionForTitle  = true,
        //        ShowHiddenFiles = true,
        //    };

        //    if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        WorkingDirTextBox.Text = dialog.SelectedPath;
        //    }
        //}
    }
}