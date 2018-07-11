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

namespace IEXStatsGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            OrderBook = new List<OrderBookLine>();
            OrderBook.Add(new OrderBookLine() { BidSize = 1, AskSize = 2, Bid = 10, Ask = 11 });

            DataContext = this;
        }

        public IList<OrderBookLine> OrderBook { get; set; }

        public class OrderBookLine
        {
            public long BidOrderCount { get; set; }
            public long BidSize { get; set; }
            public decimal Bid { get; set; }
            public decimal Ask { get; set; }
            public long AskSize { get; set; }
            public long AskOrderCount { get; set; }
        }
    }
}
