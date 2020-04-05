using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using MultiSearch.Controller;
using MultiSearch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MultiSearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private SearchFilter searchFilter=new SearchFilter();
        private MainController  mainController= new MainController();
        private List<SearchResult> searchResults=new List<SearchResult>();

        private void BrowseForFolder(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                searchFolderTextBox.Text = dialog.FileName;
            }
        }

        private void BrowseForList(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                DefaultExt = ".txt",
                Filter = "Text Files(*.txt)|*.txt|All Files (*.*)|*.*"
            };
            bool? nullable = dialog.ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag) & nullable.HasValue)
            {
                string fileName = dialog.FileName;
                listSearchTextBox.Text = fileName;
            }
        }
        private void SearchList(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.searchFolderTextBox.Text) && !string.IsNullOrEmpty(this.listSearchTextBox.Text))
            {
                searchResults.Clear();
                ResultsDataGrid.ItemsSource = null;
                List<string> searchStrings = mainController.GetSearchStrings(listSearchTextBox.Text);
                searchResults.AddRange(mainController.SearchPdfFiles(searchFolderTextBox.Text, searchStrings, searchFilter.SearchFilters));
                searchResults.AddRange(mainController.SearchTextFiles(searchFolderTextBox.Text, searchStrings,searchFilter.SearchFilters));
                ResultsDataGrid.ItemsSource = searchResults;
            }
        }

        private void browseForOutput(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                outputFolderTextBox.Text = dialog.FileName;
            }

        }
        private void ExportToCSV(object sender, RoutedEventArgs e)
        {
           
            mainController.WriteListToCSV(searchResults,outputFolderTextBox.Text);
        }


        private void mapCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            searchFilter.SearchFilters.Add(this.mapCheckBox.Content.ToString());
        }
        private void mapCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            searchFilter.SearchFilters.Remove(this.mapCheckBox.Content.ToString());
        }
        private void pdfCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            searchFilter.SearchFilters.Remove(this.pdfCheckBox.Content.ToString());
        }
        private void pdfCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.searchFilter.SearchFilters.Add(this.pdfCheckBox.Content.ToString());
        }
        private void svgCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.searchFilter.SearchFilters.Remove(this.svgCheckbox.Content.ToString());
        }
        private void svgCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.searchFilter.SearchFilters.Add(this.svgCheckbox.Content.ToString());
        }
        private void textCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.searchFilter.SearchFilters.Add(this.textCheckBox.Content.ToString());
        }
        private void textCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.searchFilter.SearchFilters.Remove(this.textCheckBox.Content.ToString());
        }
        private void xmlCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.searchFilter.SearchFilters.Add(this.xmlCheckBox.Content.ToString());
        }
        private void xmlCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.searchFilter.SearchFilters.Remove(this.xmlCheckBox.Content.ToString());
        }


    }
}
