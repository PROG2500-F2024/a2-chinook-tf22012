﻿using Microsoft.EntityFrameworkCore;
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

namespace PROG2500_A2_Chinook.Pages
{
    /// <summary>
    /// Interaction logic for TracksPage.xaml
    /// </summary>
    public partial class TracksPage : Page
    {
        ChinookContext _context = new ChinookContext();
        CollectionViewSource tracksViewSource = new CollectionViewSource();
        public TracksPage()
        {
            InitializeComponent();

            //Tie the markup viewsource object to the C# code viewsource object
            tracksViewSource = (CollectionViewSource)FindResource(nameof(tracksViewSource));

            //Use the dbContext to tell EF to load the data we want to use on this page.
            _context.Tracks.Load();

            //Set the viewsource data source to use the artists data collection (dbset)
            tracksViewSource.Source = _context.Tracks.Local.ToObservableCollection();
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            var trackQuery =
               from track in _context.Tracks
               where track.Name.Contains(trackSearchBox.Text)
               orderby track.TrackId
               select track;

            tracksViewSource.Source = trackQuery.ToList();
        }
    }
}
