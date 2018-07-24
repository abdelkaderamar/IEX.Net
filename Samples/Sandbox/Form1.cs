// Copyright (c) Abdelkader Amar. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.


﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sandbox
{
    class OrderBookLine
    {
        public long BidSize { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
        public long AskSize { get; set; }
    }

    public partial class Form1 : Form
    {
        List<OrderBookLine> _orderBook;

        public Form1()
        {
            InitializeComponent();

            _orderBook = new List<OrderBookLine>() {
    new OrderBookLine { BidSize = 10, Bid=9.9m, Ask=10.1m, AskSize = 20 },
    new OrderBookLine { BidSize = 10, Bid=9.9m, Ask=10.1m, AskSize = 20 , },
};
            var bindingList = new BindingList<OrderBookLine>(_orderBook);
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;
        }
    }
}
