using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDCApp.Entities;

namespace WDCApp
{
    public class OrderManager
    {
        private Model1 context;

        public OrderManager()
        {
            context = new Model1();
        }

        public List<Contract> GetContractsIncludingClient()
        {
            return context.Contract.Include("Client").ToList();

        }
        public void CompleteOrder(Contract contract)
        {
              
            var existingContract = App.Context.Contract.FirstOrDefault(c => c.Id == contract.Id);

            if (existingContract != null)
            {
                // Обновить свойство IsCompleted на true
                existingContract.Status = true;

                // Сохранить изменения в базе данных
                App.Context.SaveChanges();

            }

        }
    }
}
