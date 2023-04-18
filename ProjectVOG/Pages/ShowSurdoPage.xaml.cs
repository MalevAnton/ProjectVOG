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
    /// Логика взаимодействия для ShowSurdoPage.xaml
    /// </summary>
    public partial class ShowSurdoPage : Page
    {
        List<PartialClass> partialClasses = new List<PartialClass>();

        User user;

        public ShowSurdoPage()
        {
            InitializeComponent();

            CreateFile();
        }

        public ShowSurdoPage(User user)
        {
            InitializeComponent();

            this.user = user;

            CreateFile();

            textBlockFIO.Text = "" + user.Surname + " " + user.Name + " " + user.Fatherland;
        }

        public void CreateFile()
        {
            listViewProduct.ItemsSource = BaseClass.projectVOG.Surdoperevodchik.ToList();

            comboBoxSorting.SelectedIndex = 0;

            textBlockCountProduct.Text = "" + BaseClass.projectVOG.Surdoperevodchik.ToList().Count() + " из " + BaseClass.projectVOG.Surdoperevodchik.ToList().Count();
        }

        public void Filtration()
        {
            List<Surdoperevodchik> events = BaseClass.projectVOG.Surdoperevodchik.ToList();

            if (textBoxSearch.Text.Length > 0)
            {
                events = events.Where(x => x.SurnameSurdo.ToLower().Contains(textBoxSearch.Text.ToLower())).ToList();
            }

            if (comboBoxSorting.SelectedIndex > 0)
            {
                switch (comboBoxSorting.SelectedIndex)
                {
                    case 1:

                        events = events.OrderBy(x => x.SurnameSurdo).ToList();

                        break;

                    case 2:

                        events = events.OrderBy(x => x.SurnameSurdo).ToList();

                        break;
                }
            }

            listViewProduct.ItemsSource = events;

            if (events.Count == 0)
            {
                MessageBox.Show("Данные не найдены");
            }

            textBlockCountProduct.Text = "" + events.Count() + " из " + BaseClass.projectVOG.Event.ToList().Count();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            int index = Convert.ToInt32(btn.Uid);

            Surdoperevodchik surdoperevodchik = BaseClass.projectVOG.Surdoperevodchik.FirstOrDefault(x => x.idSurdo == index);

            BaseClass.projectVOG.Surdoperevodchik.Remove(surdoperevodchik);

            BaseClass.projectVOG.SaveChanges();

            FrameClass.frame.Navigate(new ShowEventPage());
        }

        private void buttonDelete_Loaded(object sender, RoutedEventArgs e)
        {
            if (user == null)
            {
                return;
            }

            Button buttonDelete = sender as Button;

            if (user.Role.Role1 == "Менеджер" || user.Role.Role1 == "Администратор")
            {
                buttonDelete.Visibility = Visibility.Visible;
            }

            else
            {
                buttonDelete.Visibility = Visibility.Collapsed;
            }
        }

        private void buttonEvent_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.frame.Navigate(new ShowEventPage(user));
        }

        private void buttonTicket_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.frame.Navigate(new ShowTicketPage(user));
        }

        private void buttonSurdo_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.frame.Navigate(new ShowSurdoPage(user));
        }

        private void buttonContact_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.frame.Navigate(new ContactPage());
        }

        private void buttonProfile_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.frame.Navigate(new ProfilePage());
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.frame.Navigate(new AutorizationPage());
        }

        private void textBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtration();
        }

        private void comboBoxSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtration();
        }
    }
}
