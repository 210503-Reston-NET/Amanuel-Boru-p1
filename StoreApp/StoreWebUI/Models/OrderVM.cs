﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreModels;

namespace StoreWebUI.Models
{
    public class OrderVM
    {
        public OrderVM()
        {
        }

        public OrderVM(Order order)
        {
            OrderId = order.OrderId;
            UserName = order.UserName;
            LocationId = order.LocationId.ToString();
            Orderdate = order.Orderdate;
            Total = order.Total;
        }

        public int OrderId { get; set; }
        public string UserName { get; set; }
        public string LocationId { get; set; }
        public DateTime Orderdate { get; set; }
        public double Total { get; set; }

        public List<SelectListItem> LocationList { get; set; }
    }
}
