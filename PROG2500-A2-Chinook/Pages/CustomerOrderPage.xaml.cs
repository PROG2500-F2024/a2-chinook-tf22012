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
    /// Interaction logic for CustomerOrderPage.xaml
    /// </summary>
    public partial class CustomerOrderPage : Page
    {
        private readonly ChinookContext _context = new ChinookContext();
        private CollectionViewSource customerOrderViewSource;
        public CustomerOrderPage()
        {
            InitializeComponent();

            customerOrderViewSource = (CollectionViewSource)FindResource(nameof(customerOrderViewSource));

            _context.Customers.Load();
            _context.Invoices.Load();
            _context.InvoiceLines.Load();

            customerOrderViewSource.Source = _context.Customers.Local.ToObservableCollection();

            LoadCustomerOrderDetails();
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadCustomerOrderDetails(customerOrdersSearchBox.Text);
        }

        private void LoadCustomerOrderDetails(string searchText = "")
        {
            // Query customers and include their invoices
            var query =
                from customer in _context.Customers
                where string.IsNullOrEmpty(searchText) || customer.LastName.Contains(searchText)
                select new
                {
                    FullName = $"{customer.FirstName} {customer.LastName}",
                    City = customer.City,
                    Country = customer.Country,
                    Email = customer.Email,
                    Invoices = customer.Invoices.Select(invoice => new
                    {
                        InvoiceDate = $"Order Date: {invoice.InvoiceDate}",
                        Total = $"Total: ${invoice.Total}",
                        Quantity = $"Quantity: {invoice.InvoiceLines.Count()}"
                    }).ToList() // Materialize invoices
                };

            // Execute the query against the database and assign it as the data source for the list view
            customerOrdersListView.ItemsSource = query.ToList();
        }

    }
}
