using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Altium
{
    public sealed class ViewModel : BaseViewModel
    {
        private TimeSpan _time;
        public TimeSpan Time
        {
            get { return _time; }
            set { _time = value; OnPropertyChanged("Time"); }
        }

        private int _duplicatesCount;
        public int DuplicatesCount
        {
            get { return _duplicatesCount; }
            set { _duplicatesCount = value; OnPropertyChanged("DuplicatesCount"); }
        }

        private String _selectedPath;
        public String SelectedPath
        {
            get { return _selectedPath; }
            set { _selectedPath = value; OnPropertyChanged("SelectedPath"); }
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
                SelectedPath = dialog.SelectedPath;
                DuplicatesCount = 7;
                Time = TimeSpan.FromSeconds(23);

                // getdirectory info

                var test = Directory.GetFiles(SelectedPath)
                    .Select(path => Path.GetFileName(path))
                    .ToList();

                var i = 5;
            }
        }
    }
}
