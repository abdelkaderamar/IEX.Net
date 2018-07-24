// Copyright (c) Abdelkader Amar. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.


﻿using System;

namespace IEX.Api.Data
{
    public class IexData
    {
        public IexData(string symbol)
        {
            Symbol = symbol;
        }

        public String Symbol { get; }
    }
}
