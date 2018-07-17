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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Data;
using System.Windows.Shapes;
using System.Windows.Input;

namespace IEXStatsGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker _worker;

        public delegate void CellChangeEventHandler(object sender, CellChangeEventArgs e);

        public event CellChangeEventHandler CellChanged;

        public MainWindow()
        {
            InitializeComponent();

            CellChanged += new CellChangeEventHandler(OnCellChanged);

            OrderBook = new List<OrderBookLine>();
            int index = 0;
            OrderBook.Add(new OrderBookLine(index++, CellChanged));
            OrderBook.Add(new OrderBookLine(index++, CellChanged));
            OrderBook.Add(new OrderBookLine(index++, CellChanged));
            OrderBook.Add(new OrderBookLine(index++, CellChanged));
            OrderBook.Add(new OrderBookLine(index++, CellChanged));

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
                Thread.Sleep(1000);
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

        // Storyboard sb = new Storyboard();
        private static DoubleAnimation blink = new DoubleAnimation()
        {
            From = 0,
            To = 1,
            Duration = TimeSpan.FromSeconds(0.2),
            FillBehavior = FillBehavior.HoldEnd
        };

        private static ColorAnimation greenColorAnimation = new ColorAnimation(Colors.Transparent, Colors.Green, 
            TimeSpan.FromMilliseconds(1500), FillBehavior.Stop);

        private static ColorAnimation redColorAnimation = new ColorAnimation(Colors.Transparent, Colors.Red,
            TimeSpan.FromMilliseconds(1250), FillBehavior.Stop);

        protected virtual void OnCellChanged(object sender, CellChangeEventArgs e)
        {
            
            Dispatcher.BeginInvoke(new Action(() =>
            {
                var sb = new Storyboard();
                sb.Stop();
                sb.Children.Clear();
                //sb.Children.Add(blink);

                DataGridCell cell = GetCell(e.Row, e.Column, dataGrid);

                //Storyboard.SetTarget(blink, cell.Content as TextBlock);
                //Storyboard.SetTargetProperty(blink, new PropertyPath(Button.OpacityProperty));

                if (e.Up)
                {
                    sb.Children.Add(greenColorAnimation);
                    Storyboard.SetTarget(greenColorAnimation, cell);
                    Storyboard.SetTargetName(greenColorAnimation, "MySolidColorBrush");

                    Storyboard.SetTargetProperty(greenColorAnimation,
                        new PropertyPath("(DataGridRow.Background).(SolidColorBrush.Color)"));
                }
                else
                {
                    sb.Children.Add(redColorAnimation);
                    Storyboard.SetTarget(redColorAnimation, cell);
                    Storyboard.SetTargetName(redColorAnimation, "MySolidColorBrush");

                    Storyboard.SetTargetProperty(redColorAnimation,
                        new PropertyPath("(DataGridRow.Background).(SolidColorBrush.Color)"));

                }

                // dataGrid.BeginStoryboard(sb);
                sb.Begin();
            }));
        }

        public IList<OrderBookLine> OrderBook { get; set; }


        public DataGridCell GetCell(int rowIndex, int columnIndex, DataGrid dg)
        {
            DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
            DataGridCellsPresenter p = GetVisualChild<DataGridCellsPresenter>(row);
            DataGridCell cell = p.ItemContainerGenerator.ContainerFromIndex(columnIndex) as DataGridCell;
            return cell;
        }

        static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
        public class OrderBookLine : INotifyPropertyChanged
        {
            long _bidOrderCount, _bidSize, _askOrderCount, _askSize;
            decimal _bid, _ask;
            CellChangeEventHandler _handler;

            public static readonly int COL_BID_ORDER_COUNT = 0;
            public static readonly int COL_BID_SIZE = 1;
            public static readonly int COL_BID = 2;
            public static readonly int COL_ASK_ORDER_COUNT = 3;
            public static readonly int COL_ASK_SIZE = 4;
            public static readonly int COL_ASK = 5;

            public OrderBookLine(int row, CellChangeEventHandler handler)
            {
                Row = row;
                _handler = handler;
            }

            public int Row { get; }

            public long BidOrderCount
            {
                get { return _bidOrderCount; }
                set { bool up = value > _bidOrderCount; _bidOrderCount = value; OnPropertyChanged("BidOrderCount", Row, COL_BID_ORDER_COUNT, up); }
            }

            public long BidSize
            {
                get { return _bidSize; }
                set { bool up = value > _bidSize; _bidSize = value; OnPropertyChanged("BidSize", Row, COL_BID_SIZE, up); }
            }
            public decimal Bid
            {
                get { return _bid; }
                set { bool up = value > _bid; _bid = value; OnPropertyChanged("Bid", Row, COL_BID, up); }
            }

            public decimal Ask
            {
                get { return _ask; }
                set { bool up = value > _ask; _ask = value; OnPropertyChanged("Ask", Row, COL_ASK, up); }
            }

            public long AskSize
            {
                get { return _askSize; }
                set { bool up = value > _askSize; _askSize = value; OnPropertyChanged("AskSize", Row, COL_ASK_SIZE, up); }
            }

            public long AskOrderCount
            {
                get { return _askOrderCount; }
                set { bool up = value > _askOrderCount; _askOrderCount = value; OnPropertyChanged("AskOrderCount", Row, COL_ASK_ORDER_COUNT, up); }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged(string name, int row, int col, bool up)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
                }
                _handler(this, new CellChangeEventArgs(row, col, up));
            }
        }

        private void dataGrid_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            Console.WriteLine("Cell changed");

        }
    }

    public class CellChangeEventArgs : EventArgs
    {
        public CellChangeEventArgs(int row, int col, bool up)
        {
            Row = row;
            Column = col;
            Up = up;
        }

        public int Row { get; }

        public int Column { get; }

        public bool Up { get; }
    }
}