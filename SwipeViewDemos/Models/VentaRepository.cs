using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeViewDemos.Models
{
    public class VentaRepository
    {
        public IList<VentaModel> Venta { get; set; }

        public VentaRepository()
        {
            //Hydrator<Friend> _friendHydrator
            //    = new Hydrator<Friend>();
            //Friends = _friendHydrator.GetList(50);
            Task.Run(async () =>
                Venta = await App.vDatabase.GetItemsAsync()).Wait();
        }

        public IList<VentaModel> GetAll()
        {
            return Venta;
        }

        
    }
}
