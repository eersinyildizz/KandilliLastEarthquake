using DAL.Content.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace DAL.Repository
{
    public class EarthquakeRepository : IGenericRepository<Earthquake>
    {
        
        public Earthquake Add(Earthquake entity)
        {
            using (DataContext db = new DataContext())
            {
                var Addeditem = db.Earthquakes.Add(entity);
                db.SaveChanges();
                return Addeditem;
            }
        }

        public void Delete(Earthquake entity)
        {
            throw new NotImplementedException();
        }
        public Earthquake[] Get(Expression<Func<Earthquake, bool>> predicate = null, int count = 0)
        {
            using (DataContext db = new DataContext())
            {
                if (count == 0)
                {
                    return db.Earthquakes.Where(predicate).ToArray();
                }
                if (predicate == null)
                {
                    return db.Earthquakes.OrderByDescending(p=>p.Key).Take(count).ToArray();
                }
                return db.Earthquakes.Where(predicate).Take(count).ToArray();
            }
        }

        public Earthquake[] GetAll()
        {
            using (DataContext db = new DataContext())
            {
                Earthquake[] result = db.Earthquakes.ToArray();
                return result;
            }
        }



        public Earthquake GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Earthquake entity)
        {
            throw new NotImplementedException();
        }


    }
}
