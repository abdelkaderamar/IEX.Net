using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace IEXStatsGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private BackgroundWorker _worker;

        public MainWindow()
        {
            InitializeComponent();

            OrderBook = new List<OrderBookLine>();
            OrderBook.Add(new OrderBookLine());
            OrderBook.Add(new OrderBookLine());
            OrderBook.Add(new OrderBookLine());
            OrderBook.Add(new OrderBookLine());
            OrderBook.Add(new OrderBookLine());

            DataContext = this;

            _worker = new BackgroundWorker();
            _worker.DoWork += worker_DoUpdate;
            _worker.RunWorkerAsync();
        }

        protected void worker_DoUpdate(object sender, DoWorkEventArgs e)
        {
            Random rand = new Random();
            while (true)
            {
                Thread.Sleep(10);
                int row = rand.Next(0, OrderBook.Count);
                int t = rand.Next(0, 6);

                switch (t)
                {
                    case 0:
                        OrderBook[row].BidSize = rand.Next(1, 10000);
                        break;
                    case 1:
                        OrderBook[row].AskSize = rand.Next(1, 10000);
                        break;
                    case 2:
                        OrderBook[row].Bid = Math.Min(rand.Next(1, 100), OrderBook[0].Ask - 0.1m);
                        break;
                    case 3:
                        OrderBook[row].Ask = Math.Max(OrderBook[0].Bid + 0.1m, rand.Next(1, 100));
                        break;
                    case 4:
                        OrderBook[row].BidOrderCount = rand.Next(1, 25);
                        break;
                    case 5:
                        OrderBook[row].AskOrderCount = rand.Next(1, 25);
                        break;
                }
            }
        }

        public IList<OrderBookLine> OrderBook { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public class OrderBookLine : INotifyPropertyChanged
        {
            long _bidOrderCount, _bidSize, _askOrderCount, _askSize;
            decimal _bid, _ask;

            public long BidOrderCount
            {
                get { return _bidOrderCount; }
                set { _bidOrderCount = value; OnPropertyChanged("BidOrderCount"); }
            }

            public long BidSize
            {
                get { return _bidSize; }
                set { _bidSize = value; OnPropertyChanged("BidSize"); }
            }
            public decimal Bid
            {
                get { return _bid; }
                set { _bid = value; OnPropertyChanged("Bid"); }
            }

            public decimal Ask
            {
                get { return _ask; }
                set { _ask = value; OnPropertyChanged("Ask"); }
            }

            public long AskSize
            {
                get { return _askSize; }
                set { _askSize = value; OnPropertyChanged("AskSize"); }
            }

            public long AskOrderCount
            {
                get { return _askOrderCount; }
                set { _askOrderCount = value; OnPropertyChanged("AskOrderCount"); }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged(string name)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
                }
            }
        }
    }
}
