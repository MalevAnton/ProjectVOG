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
    /// Логика взаимодействия для TicketWindow.xaml
    /// </summary>
    public partial class TicketWindow : Window
    {
        Order order;

        List<PartialClass> partialClasses;

        double summa;

        public TicketWindow(Order order, List<PartialClass> partialClasses, double summa)
        {
            InitializeComponent();

            this.order = order;

            this.partialClasses = partialClasses;

            this.summa = summa;
        }
    }
}
