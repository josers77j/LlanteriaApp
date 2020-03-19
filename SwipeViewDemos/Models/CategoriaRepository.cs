using SwipeViewDemos.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeViewDemos.Models
{
    public class CategoriaRepository
    {
        public IList<CategoriaModel> Category { get; set; }

        public CategoriaRepository()
        {
            //Hydrator<Friend> _friendHydrator
            //    = new Hydrator<Friend>();
            //Friends = _friendHydrator.GetList(50);
            Task.Run(async () =>
                Category = await App.Database.GetItemsAsync()).Wait();
        }

        public IList<CategoriaModel> GetAll()
        {
            return Category;
        }

        public IList<CategoriaModel> GetAllByFirstLetter(string letter)
        {
            var query = from q in Category
                        where q.Nombre_Categoria.StartsWith(letter)
                        select q;
            return query.ToList();
        }

        public
            ObservableCollection
            <Grouping<string, CategoriaModel>>
            GetAllGrouped()
        {
            IEnumerable<Grouping<string, CategoriaModel>> sorted = new Grouping<string, CategoriaModel>[0];
            if (Category != null)
            {
                sorted =
                    from f in Category
                    orderby f.Nombre_Categoria
                    group f by f.Nombre_Categoria[0].ToString()
                    into theGroup
                    select
                    new Grouping<string, CategoriaModel>
                        (theGroup.Key, theGroup);
            }


            return new
                ObservableCollection
                <Grouping<string, CategoriaModel>>(sorted);
        }

    }
}
