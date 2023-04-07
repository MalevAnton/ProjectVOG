using ProjectVOG.Classes;
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

namespace ProjectVOG.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();

            cbRole.ItemsSource = BaseClass.projectVOG.Role.ToList();

            cbRole.SelectedValue = "idRole";

            cbRole.DisplayMemberPath = "Role";

            cbRole.SelectedIndex = 1;
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            int g = 0;

            if (rbMen.IsChecked == true) g = 1;

            if (rbWomen.IsChecked == true) g = 2;

            User user = new User()
            {
                Surname = textBoxSurname.Text,

                Name = textBoxName.Text,

                Fatherland = textBoxFatherland.Text,

                Birthday = Convert.ToDateTime(dpBirthday.SelectedDate),

                Login = textBoxLogin.Text,

                Password = passwordBoxPassword.Password,

                idGender = g,

                idRole = cbRole.SelectedIndex + 1
            };

            BaseClass.projectVOG.User.Add(user);

            BaseClass.projectVOG.SaveChanges();

            MessageBox.Show("Пользователь добавлен");

            FrameClass.frame.Navigate(new AutorizationPage());
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.frame.Navigate(new AutorizationPage());
        }
    }
}
