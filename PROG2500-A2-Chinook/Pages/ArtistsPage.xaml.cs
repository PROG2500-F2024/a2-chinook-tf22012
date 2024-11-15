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
    /// Interaction logic for ArtistsPage.xaml
    /// </summary>
    public partial class ArtistsPage : Page
    {
        ChinookContext _context = new ChinookContext();
        CollectionViewSource artistsViewSource = new CollectionViewSource();

        public ArtistsPage()
        {
            InitializeComponent();

            //Tie the markup viewsource object to the C# code viewsource object
            artistsViewSource = (CollectionViewSource)FindResource(nameof(artistsViewSource));

            //Use the dbContext to tell EF to load the data we want to use on this page.
            _context.Artists.Load();

            //Set the viewsource data source to use the artists data collection (dbset)
            artistsViewSource.Source = _context.Artists.Local.ToObservableCollection();
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            var artistsQuery = 
               from artist in _context.Artists
               where artist.Name.Contains(artistSearchBox.Text)
               orderby artist.Name
               select artist;

            artistsViewSource.Source = artistsQuery.ToList();
        }
    }
}
