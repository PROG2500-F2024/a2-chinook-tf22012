using Microsoft.EntityFrameworkCore;
using PROG2500_A2_Chinook.Data;
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
using Windows.UI;

namespace PROG2500_A2_Chinook.Pages
{
    /// <summary>
    /// Interaction logic for AlbumsPage.xaml
    /// </summary>
    public partial class AlbumsPage : Page
    {
        ChinookContext _context = new ChinookContext();
        CollectionViewSource albumsViewSource = new CollectionViewSource();

        public AlbumsPage()
        {
            InitializeComponent();

            //Tie the markup viewsource object to the C# code viewsource object
            albumsViewSource = (CollectionViewSource)FindResource(nameof(albumsViewSource));

            //Use the dbContext to tell EF to load the data we want to use on this page.
            _context.Albums.Load();

            //Set the viewsource data source to use the album data collection (dbset)
            albumsViewSource.Source = _context.Albums.Local.ToObservableCollection();
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            var albumQuery =
               from album in _context.Albums
               where album.Title.Contains(albumSearchBox.Text)
               orderby album.AlbumId
               select album;

            albumsViewSource.Source = albumQuery.ToList();
        }
    }
}
