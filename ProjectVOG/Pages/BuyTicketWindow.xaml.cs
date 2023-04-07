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
using System.Windows.Shapes;

namespace ProjectVOG.Pages
{
    /// <summary>
    /// Логика взаимодействия для BuyTicketWindow.xaml
    /// </summary>
    public partial class BuyTicketWindow : Window
    {
        User user;

        List<PartialClass> partialClasses;

        double summa;

        public BuyTicketWindow(List<PartialClass> partialClasses, User user)
        {
            InitializeComponent();

            this.user = user;

            this.partialClasses = partialClasses;

            listViewProduct.ItemsSource = partialClasses;

            if (user != null)
            {
                textBlockFIO.Text = "" + user.Surname + " " + user.Name + " " + user.Fatherland;
            }

            Calculation();
        }

        private void Calculation()
        {
            summa = 0;

            foreach (PartialClass partialClass in partialClasses)
            {
                summa += partialClass.count;
            }

            textBlockSumma.Text = "Сумма заказа: " + summa.ToString("0.00") + " руб.";
        }

        private void textBoxCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            int index = Convert.ToInt32(textBox.Uid);

            PartialClass partialClass = partialClasses.FirstOrDefault(x => x.ticket.idTicket == index);

            if (textBox.Text.Replace(" ", "") == "")
            {
                partialClass.count = 0;
            }

            else
            {
                partialClass.count = Convert.ToInt32(textBox.Text);
            }

            if (partialClass.count == 0)
            {
                partialClasses.Remove(partialClass);
            }

            if (partialClasses.Count == 0)
            {
                this.Close();
            }

            listViewProduct.Items.Refresh();

            Calculation();
        }

        private void textBoxCount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0)))
            {
                e.Handled = true;
            }
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            int index = Convert.ToInt32(btn.Uid);

            PartialClass partialBask = partialClasses.FirstOrDefault(x => x.ticket.idTicket == index);

            partialClasses.Remove(partialBask);

            if (partialClasses.Count == 0)
            {
                this.Close();
            }

            listViewProduct.Items.Refresh();

            Calculation();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonAddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Order order = new Order();

                List<Order> orderLast = BaseClass.projectVOG.Order.OrderBy(x => x.idOrder).ToList();

                order.idOrder = orderLast[orderLast.Count - 1].idOrder + 1;

                order.OrderDate = DateTime.Now;

                if (user != null)
                {
                    order.OrderClient = user.idUser;
                }

                Random rand = new Random();

                string textCode = "";

                for (int i = 0; i < 3; i++)
                {
                    textCode = textCode + rand.Next(10).ToString();
                }

                order.OrderCode = Convert.ToInt32(textCode);

                BaseClass.projectVOG.Order.Add(order);

                BaseClass.projectVOG.SaveChanges();

                foreach (PartialClass partialBask in partialClasses)
                {
                    OrderTicket orderTicket = new OrderTicket();

                    orderTicket.idOrder = order.idOrder;

                    orderTicket.idTicket = partialBask.ticket.idTicket;

                    orderTicket.TicketCount = partialBask.count;

                    BaseClass.projectVOG.OrderTicket.Add(orderTicket);
                }

                BaseClass.projectVOG.SaveChanges();

                MessageBox.Show("Заказ успешно создан");

                TicketWindow ticketWindow = new TicketWindow(order, partialClasses, summa);

                ticketWindow.ShowDialog();

                partialClasses.Clear();

                this.Close();
            }

            catch
            {
                MessageBox.Show("При создание заказа возникла ошибка!");
            }
        }
    }
}
