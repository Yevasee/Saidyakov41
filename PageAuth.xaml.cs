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
    /// Логика взаимодействия для PageAuth.xaml
    /// </summary>
    public partial class PageAuth : Page
    {
        public int CountAuthFails = 0;
        public string CapchaChars = "";
        public PageAuth()
        {
            InitializeComponent();
        }

        private void BtnInGuest_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageProduct(null));
            TextBoxLogin.Text = "";
            TextBoxPassword.Text = "";
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = TextBoxLogin.Text;
            string password = TextBoxPassword.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Есть пустые поля");
            }

            User user = Saidyakov41Entities.GetContext().User.ToList().Find(p => p.UserLogin == login && p.UserPassword == password);
            if (user != null && (CountAuthFails == 0 || CapchaChars == TextBoxCapcha.Text))
            {
                Manager.MainFrame.Navigate(new PageProduct(user));
                TextBoxLogin.Text = "";
                TextBoxPassword.Text = "";
                TextBlockCapchaOneWord.Text = "";
                TextBlockCapchaTwoWord.Text = "";
                TextBlockCapchaThreeWord.Text = "";
                TextBlockCapchaFourWord.Text = "";
                StackPanelCapcha.Visibility = Visibility.Hidden;
                TextBoxCapcha.Visibility = Visibility.Hidden;
            } else
            {
                if (CountAuthFails > 0)
                    MessageBox.Show("Логин, пароль или капча введены неверно");
                else
                    MessageBox.Show("Логин или пароль введены неверно");

                StackPanelCapcha.Visibility = Visibility.Visible;
                TextBoxCapcha.Visibility = Visibility.Visible;
                Random random = new Random();

                string symbols = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890!@#$%&";
                CapchaChars = (symbols[random.Next(symbols.Length)].ToString() + symbols[random.Next(symbols.Length)].ToString()
                    + symbols[random.Next(symbols.Length)].ToString() + symbols[random.Next(symbols.Length)].ToString());
                TextBlockCapchaOneWord.Text = CapchaChars[0].ToString();
                TextBlockCapchaTwoWord.Text = CapchaChars[1].ToString();
                TextBlockCapchaThreeWord.Text = CapchaChars[2].ToString();
                TextBlockCapchaFourWord.Text = CapchaChars[3].ToString();



                TextBlockCapchaOneWord.Margin = new Thickness(random.Next(0, 20), random.Next(0, 20), 0, 0);
                TextBlockCapchaTwoWord.Margin = new Thickness(random.Next(0, 20), random.Next(0, 20), 0, 0);
                TextBlockCapchaThreeWord.Margin = new Thickness(random.Next(0, 20), random.Next(0, 20), 0, 0);
                TextBlockCapchaFourWord.Margin = new Thickness(random.Next(0, 20), random.Next(0, 20), 0, 0);

                CountAuthFails++;



                if (CountAuthFails >= 2)
                {
                    MessageBox.Show("Введены неверные данные");
                    BtnLogin.IsEnabled = false;
                    await Task.Delay(10000);
                    BtnLogin.IsEnabled = true;
                }
            }
        }
    }
}
