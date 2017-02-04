using OrdersAndisheh.DBL;
using System;
using System.Collections.Generic;
namespace BL
{
    public interface ISefareshService
    {
        void AcceptSefaresh(string tarikhsefaresh);
        List<Customer> LoadDestinations();
        List<Driver> LoadDrivers();
        List<Product> LoadGoods();
        Sefaresh LoadSefaresh(string tarikh);
        void SaveSefaresh(Sefaresh sefaresh);
        void UnAcceptSefaresh(Sefaresh sefaresh);
        void UpdateSefaresh(Sefaresh sefaresh);
        List<Customer> LoadOracleCustomer();

        void SaveOracleRelation(List<OrdersAndisheh.DBL.Customer> OCustomers);

        List<ItemSefaresh> LoadNoDriverSefareshItems(string sefareshTarikh);

        void AddDriver(OrdersAndisheh.DBL.Driver p);

        //void DelNoUsedTempDrivers();

        void Save();
        void DeleteSefaresh(string SelectedTarikh);
        void DelNoUsedTempDrivers(List<Driver> TempDriverForDelete);

        List<string> LoadAllNOAcceptedSefareshTarikh();

        bool HasOracle(Product product, Customer customer);
    }
}
