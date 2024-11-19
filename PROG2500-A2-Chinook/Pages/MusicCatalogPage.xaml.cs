using Microsoft.EntityFrameworkCore;
using PROG2500_A2_Chinook.Data;
using PROG2500_A2_Chinook.Models;
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

namespace PROG2500_A2_Chinook.Pages
{
    /// <summary>
    /// Interaction logic for MusicCatalogPage.xaml
    /// </summary>
    public partial class MusicCatalogPage : Page
    {
        private readonly ChinookContext _context = new ChinookContext();
        private CollectionViewSource catalogViewSource;
        public MusicCatalogPage()
        {
            InitializeComponent();
            catalogViewSource = (CollectionViewSource)FindResource(nameof(catalogViewSource));

            _context.Artists.Load();
            _context.Albums.Load();
            _context.Tracks.Load();

            catalogViewSource.Source = _context.Artists.Local.ToObservableCollection();

            LoadCatalog();
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadCatalog(catalogSearchBox.Text);
        }

        private void LoadCatalog(string searchText = "")
        {
            // Define a grouping query to get grouped category data
            var query =
                from artist in _context.Artists
                where string.IsNullOrEmpty(searchText) || artist.Name.Contains(searchText)
                group artist by artist.Name.ToUpper().Substring(0, 1) into artGroup
                select new
                {
                    HeaderText = $"{artGroup.Key} ({artGroup.Count()}):",
                    artists = artGroup.ToList<Artist>(),
                };

            // Execute the query against the database and assign it as the data source for the list view
            catalogListView.ItemsSource = query.ToList();
        }

    }
}
