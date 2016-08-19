﻿using System;
namespace BL
{
    public interface ISefareshService
    {
        void AcceptSefaresh(BL.Sefaresh sefaresh);
        System.Collections.Generic.List<OrdersAndisheh.DBL.Customer> LoadDestinations();
        System.Collections.Generic.List<OrdersAndisheh.DBL.Driver> LoadDrivers();
        System.Collections.Generic.List<OrdersAndisheh.DBL.Product> LoadGoods();
        BL.Sefaresh LoadSefaresh(string tarikh);
        void SaveSefaresh(BL.Sefaresh sefaresh);
        void UnAcceptSefaresh(BL.Sefaresh sefaresh);
        void UpdateSefaresh(BL.Sefaresh sefaresh);
    }
}