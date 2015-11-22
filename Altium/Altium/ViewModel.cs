using CoreServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Altium
{
    public sealed class ViewModel : BaseViewModel
    {
        private DuplicatesController model;

        private TimeSpan _time;
        public TimeSpan Time
        {
            get { return _time; }
            set { _time = value; OnPropertyChanged(nameof(Time)); }
        }

        private int _filesCount;
        public int FilesCount
        {
            get { return _filesCount; }
            set { _filesCount = value; OnPropertyChanged(nameof(FilesCount)); }
        }

        public List<IFilesComparer> AvaliableComparers
        {
            get { return DuplicatesController.AvaliableComparers; }
        }

        private IFilesComparer _selectedComparer;
        public IFilesComparer SelectedComparer
        {
            get { return _selectedComparer; }
            set { _selectedComparer = value; OnPropertyChanged(nameof(SelectedComparer)); }
        }

        private int _duplicatesCount;
        public int DuplicatesCount
        {
            get { return _duplicatesCount; }
            set { _duplicatesCount = value; OnPropertyChanged(nameof(DuplicatesCount)); }
        }

        private List<List<FileInfo>> _duplicates;
        public List<List<FileInfo>> Duplicates
        {
            get { return _duplicates; }
            set { _duplicates = value;  OnPropertyChanged(nameof(Duplicates)); }
        }

        private List<FileInfo> _blockedFiles;
        public List<FileInfo> BlockedFiles
        {
            get { return _blockedFiles; }
            set { _blockedFiles = value; OnPropertyChanged(nameof(BlockedFiles)); }
        }

        private String _selectedPath;
        public String SelectedPath
        {
            get { return _selectedPath; }
            set { _selectedPath = value; OnPropertyChanged(nameof(SelectedPath)); }
        }

        private Visibility _progressVisible = Visibility.Hidden;
        public Visibility ProgressVisible
        {
            get { return _progressVisible; }
            set { _progressVisible = value; OnPropertyChanged(nameof(ProgressVisible)); }
        }

        public ICommand SelectFolderCommand { get; set; }

        public ViewModel()
        {
            SelectFolderCommand = new RelayCommand<Object>(SelectFolderHelper);
        }

        private void SelectFolderHelper(object obj)
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //SelectedPath = @"C:\test";
                SelectedPath = dialog.SelectedPath;
                ProgressVisible = Visibility.Visible;
                model = new DuplicatesController(SelectedPath);
                FilesCount = model.Files.Count + model.LockedFiles.Count;
                BlockedFiles = model.LockedFiles;
                model.InjectComparer(SelectedComparer);

                Async(model.GetDuplicates, res =>
                {
                    Duplicates = res;
                    DuplicatesCount = Duplicates.Select(x => x.Count).Sum();
                    ProgressVisible = Visibility.Hidden;
                });
            }
        }

        private static void Async<T>(Func<T> operation, Action<T> marshalToUi)
        {
            var dispatcher = System.Windows.Application.Current.Dispatcher;

            Task.Factory.StartNew(operation)
                .ContinueWith(result => dispatcher.BeginInvoke(marshalToUi, result.Result), TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}
