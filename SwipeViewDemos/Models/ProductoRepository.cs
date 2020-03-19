using SwipeViewDemos.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeViewDemos.Models
{
    public class ProductoRepository
    {
        public IList<ProductoModel> Producto { get; set; }

        public ProductoRepository()
        {
            //Hydrator<Friend> _friendHydrator
            //    = new Hydrator<Friend>();
            //Friends = _friendHydrator.GetList(50);
            Task.Run(async () =>
                Producto = await App.pDatabase.GetItemsAsync()).Wait();
        }

        public IList<ProductoModel> GetAll()
        {
            return Producto;
        }

        public IList<ProductoModel> GetAllByFirstLetter(string letter)
        {
            var query = from q in Producto
                        where q.Nombre_Producto.StartsWith(letter)
                        select q;
            return query.ToList();
        }

        public
            ObservableCollection
            <Grouping<string, ProductoModel>>
            GetAllGrouped()
        {
            IEnumerable<Grouping<string, ProductoModel>> sorted = new Grouping<string, ProductoModel>[0];
            if (Producto != null)
            {
                sorted =
                    from f in Producto
                    orderby f.Nombre_Producto
                    group f by f.Nombre_Producto[0].ToString()
                    into theGroup
                    select
                    new Grouping<string, ProductoModel>
                        (theGroup.Key, theGroup);
            }


            return new
                ObservableCollection
                <Grouping<string, ProductoModel>>(sorted);
        }

    }
}
