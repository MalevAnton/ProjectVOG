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
    /// Логика взаимодействия для ShowTicketPage.xaml
    /// </summary>
    public partial class ShowTicketPage : Page
    {
        List<PartialClass> partialClasses = new List<PartialClass>();

        User user;

        public ShowTicketPage()
        {
            InitializeComponent();

            CreateFile();
        }

        public ShowTicketPage(User user)
        {
            InitializeComponent();

            this.user = user;

            CreateFile();

            textBlockFIO.Text = "" + user.Surname + " " + user.Name + " " + user.Fatherland;
        }

        public void CreateFile()
        {
            listViewProduct.ItemsSource = BaseClass.projectVOG.Event.ToList();

            comboBoxSorting.SelectedIndex = 0;

            textBlockCountProduct.Text = "" + BaseClass.projectVOG.Event.ToList().Count() + " из " + BaseClass.projectVOG.Event.ToList().Count();
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

        private void comboBoxFiltration_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtration();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы хотите удалить элемент?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Button button = (Button)sender;

                    int index = Convert.ToInt32(button.Uid);

                    Ticket ticket = BaseClass.projectVOG.Ticket.FirstOrDefault(x => x.idTicket == index);

                    List<OrderTicket> orderProducts = BaseClass.projectVOG.OrderTicket.Where(x => x.idTicket == index).ToList();

                    if (orderProducts.Count == 0)
                    {
                        foreach (OrderTicket orderProduct in orderProducts)
                        {
                            BaseClass.projectVOG.OrderTicket.Remove(orderProduct);
                        }

                        BaseClass.projectVOG.Ticket.Remove(ticket);

                        BaseClass.projectVOG.SaveChanges();

                        FrameClass.frame.Navigate(new ShowTicketPage(user));
                    }

                    else
                    {
                        MessageBox.Show("Нельзя удалить товар, он указан в заказе.");
                    }
                }
            }

            catch
            {
                MessageBox.Show("Oшибка");
            }
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

        public void Filtration()
        {
            List<Ticket> tickets = BaseClass.projectVOG.Ticket.ToList();

            if (textBoxSearch.Text.Length > 0)
            {
                tickets = tickets.Where(x => x.TitleMovie.ToLower().Contains(textBoxSearch.Text.ToLower())).ToList();
            }

            if (comboBoxSorting.SelectedIndex > 0)
            {
                switch (comboBoxSorting.SelectedIndex)
                {
                    case 1:

                        tickets = tickets.OrderBy(x => x.TitleMovie).ToList();

                        break;

                    case 2:

                        tickets = tickets.OrderByDescending(x => x.TitleMovie).ToList();

                        break;
                }
            }

            listViewProduct.ItemsSource = tickets;

            if (tickets.Count == 0)
            {
                MessageBox.Show("Данные не найдены");
            }

            textBlockCountProduct.Text = "" + tickets.Count() + " из " + BaseClass.projectVOG.Event.ToList().Count();
        }

        private void buttonBuyTicket_Click(object sender, RoutedEventArgs e)
        {
            Ticket ticket = (Ticket)listViewProduct.SelectedItem;
            bool s = false;

            foreach (PartialClass partialClass in partialClasses)
            {
                if (partialClass.ticket == ticket)
                {
                    partialClass.count = partialClass.count += 1;

                    s = true;
                }
            }

            if (!s)
            {
                PartialClass partialClasse = new PartialClass();

                partialClasse.ticket = ticket;

                //partialClasse.order = ticket.idTicket;

                partialClasse.count = 1;

                partialClasses.Add(partialClasse);
            }

            buttonShow.Visibility = Visibility.Visible;
        }

        private void buttonBuy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonShow_Click(object sender, RoutedEventArgs e)
        {
            BuyTicketWindow buyTicketWindow = new BuyTicketWindow(partialClasses, user);

            buyTicketWindow.ShowDialog();

            if (partialClasses.Count == 0)
            {
                buttonShow.Visibility = Visibility.Collapsed;
            }
        }
    }
}
