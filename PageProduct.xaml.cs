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

namespace Saidyakov41
{
    /// <summary>
    /// Логика взаимодействия для PageProduct.xaml
    /// </summary>
    public partial class PageProduct : Page
    {
        int CountRecords;
        int CountPage;
        int CurrentPage = 0;
        List<Product> CurrentPageList = new List<Product>();
        List<Product> TableList;
        public PageProduct(User user=null)
        {
            InitializeComponent();

            if (user == null)
            {
                TextBlockFIO.Text = "гость";
                TextBlockRole.Text = "Гость";
            }
            else
            {
                TextBlockFIO.Text = user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
                TextBlockRole.Text = user.Role.RoleName;
            }


            var currentProducts = Saidyakov41Entities.GetContext().Product.ToList();
            ComboTypeFilter.SelectedIndex = 0;
            ComboTypeSort.SelectedIndex = 0;
            ListViewProduct.ItemsSource = currentProducts;
            TBAllRecords.Text = " из " + currentProducts.Count.ToString();
        }
        private void UpdateProducts()
        {
            var currentProducts = Saidyakov41Entities.GetContext().Product.ToList();

            currentProducts = currentProducts.Where(p => (p.ProductName.ToLower().Contains(TBoxSearch.Text.ToLower()))).ToList();


            switch (ComboTypeFilter.SelectedIndex)
            {
                case 1: currentProducts = currentProducts.Where(p => (p.ProductDiscountAmount > 0 && p.ProductDiscountAmount < 10)).ToList(); break;
                case 2: currentProducts = currentProducts.Where(p => (p.ProductDiscountAmount >= 10 && p.ProductDiscountAmount < 15)).ToList(); break;
                case 3: currentProducts = currentProducts.Where(p => (p.ProductDiscountAmount >= 15)).ToList(); break;
            }

            switch (ComboTypeSort.SelectedIndex)
            {
                //case 0: currentProducts = currentProducts.ToList(); break;
                case 1: currentProducts = currentProducts.OrderBy(p => p.ProductCost).ToList(); break;
                case 2: currentProducts = currentProducts.OrderByDescending(p => p.ProductCost).ToList(); break;
            }

            ListViewProduct.ItemsSource = currentProducts;

            TableList = currentProducts;
            TBCount.Text = "кол-во " + currentProducts.Count.ToString();
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageAddEdit());
        }

        private void ComboTypeSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProducts();
        }

        private void ComboTypeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProducts();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateProducts();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProducts();
        }
    }
}
