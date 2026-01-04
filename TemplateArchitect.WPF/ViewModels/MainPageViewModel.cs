using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TemplateArchitect.WPF.Core;
using TemplateArchitect.WPF.Core.CommandManagement;
using TemplateArchitect.WPF.Core.Dtos;
using TemplateArchitect.WPF.Core.Enums;
using TemplateArchitect.WPF.Core.Extensions;
namespace TemplateArchitect.WPF.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<DropDownItemDto<DotNetVersions>> AvailableDotNetVersions { get; }
        public ObservableCollection<DropDownItemDto<ArchitectureEnums>> AvailableArchitectureTypes { get; }
        
        public MainPageCommandManager MainPageCommand { get; }
        public MainPageCommandManager BrowseCommand {  get; }
        public MainPageCommandManager SeparateContractsProjectCommand { get; }

        private DotNetVersions? _selectedVersion;
        private ArchitectureEnums? _selectedArchitecture;
        private string? _workingDirectory;
        private string? _applicationName;
        private string _outputText = "";
        private bool _isRunning;
        private StringBuilder _outputBuilder = new();
        private bool _shouldCreateSeparateContractsProject;
        private string? _separateContractsProjectName;
        private string? _separateContractsProjectNameBoxVisibility;

        public MainPageViewModel()
        {
            AvailableDotNetVersions = new ObservableCollection<DropDownItemDto<DotNetVersions>>(Utils.ToDisplayList<DotNetVersions>());
            AvailableArchitectureTypes = new ObservableCollection<DropDownItemDto<ArchitectureEnums>>(Utils.ToDisplayList<ArchitectureEnums>());
            MainPageCommand = new MainPageCommandManager(Run, CanRun);
            BrowseCommand = new MainPageCommandManager(OpenFolderDialogBox, () => true);
        }

        private async Task Run()
        {
            IsRunning = true;

            var workingDir = WorkingDirectory;
            var applicationName = ApplicationName!.Replace(" ","");
            var architecture = SelectedArchitecture;
            var dotnetVersion = SelectedVersion!.GetDisplayName();
            var separateContractsProjectName = ShouldCreateSeparateContractsProject ? SeparateContractsProjectName : null;

            var command = $"{architecture!.Value.ToString()} {workingDir} {applicationName} {dotnetVersion}";
            if (ShouldCreateSeparateContractsProject)
            {
                command += $" -S {separateContractsProjectName}";
            }

            var psi = new ProcessStartInfo
            {
                FileName = "TemplateArchitect.CLI.exe",
                Arguments = $"{command}",
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            var process = new Process { StartInfo = psi };

            process.OutputDataReceived += (sender, args) =>
            {
                if (args.Data != null)
                {
                    AppendOutput(args.Data + "\n");
                }
            };
            process.ErrorDataReceived += (sender, args) =>
            {
                if (args.Data != null)
                {
                    AppendOutput("[ERR] " + args.Data + "\n");
                }
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            await process.WaitForExitAsync();
            IsRunning = false;
        }
        private async Task OpenFolderDialogBox()
        {
            using var dialog = new FolderBrowserDialog
            {
                Description = "Select Project Folder",
                UseDescriptionForTitle = true,
                ShowHiddenFiles = true,
            };

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                WorkingDirectory = dialog.SelectedPath;
            }
        }

        private bool CanRun() => SelectedArchitecture.HasValue && SelectedVersion.HasValue && !IsRunning;

        private void SeparateContractsProjectNameVisibilityChanged()
        {
            if (ShouldCreateSeparateContractsProject)
            {
                SeparateContractsProjectNameBoxVisibility = "Visible";
                SeparateContractsProjectName = "Enter Contracts Project Name";
            }
            else
            {
                SeparateContractsProjectNameBoxVisibility = "Hidden";
                SeparateContractsProjectName = "";
            }
        }
        public DotNetVersions? SelectedVersion
        {
            get { return _selectedVersion; }
            set 
            { 
                _selectedVersion = value;
                OnPropertyChanged();
                MainPageCommand.RaiseCanExecuteChanged();
            }
        }

        public ArchitectureEnums? SelectedArchitecture
        {
            get { return _selectedArchitecture; }
            set
            {
                _selectedArchitecture = value;
                OnPropertyChanged();
                MainPageCommand.RaiseCanExecuteChanged();
            }
        }

        public string? WorkingDirectory
        {
            get { return _workingDirectory; }
            set 
            { 
                _workingDirectory = value;
                OnPropertyChanged(); 
                MainPageCommand.RaiseCanExecuteChanged();
            }
        }

        public string? ApplicationName
        {
            get { return _applicationName; }
            set
            {
                _applicationName = value;
                OnPropertyChanged();
                MainPageCommand.RaiseCanExecuteChanged();
            }
        }

        public string OutputText
        {
            get { return _outputText; }
            set
            {
                _outputText = value;
                OnPropertyChanged();
            }
        }

        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                OnPropertyChanged();
                MainPageCommand.RaiseCanExecuteChanged();
            }
        }

        public bool ShouldCreateSeparateContractsProject
        {
            get { return _shouldCreateSeparateContractsProject; }
            set
            {
                _shouldCreateSeparateContractsProject = value;
                OnPropertyChanged();
                SeparateContractsProjectNameVisibilityChanged();
            }
        }

        public string? SeparateContractsProjectName
        {
            get { return _separateContractsProjectName; }
            set
            {
                _separateContractsProjectName = value;
                OnPropertyChanged();
                MainPageCommand.RaiseCanExecuteChanged();
            }
        }

        public string? SeparateContractsProjectNameBoxVisibility
        {
            get { return _separateContractsProjectNameBoxVisibility; }
            set
            {
                _separateContractsProjectNameBoxVisibility = value;
                OnPropertyChanged();
                MainPageCommand.RaiseCanExecuteChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }     
        
        private void AppendOutput (string outputText)
        {
            System.Windows.Application.Current.Dispatcher.BeginInvoke(() =>
            {
                _outputBuilder.AppendLine(outputText);
                OutputText = _outputBuilder.ToString();
            });
        }
        
    }

}
