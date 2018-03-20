using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BDSA2017.Assignment04
{
    public class CarCRUD : IDisposable
    {
        private readonly Slot_Car_TournamentContext _context;

        public CarCRUD(Slot_Car_TournamentContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="car"></param>
        /// <returns>The id of the newly created car</returns>
        public int Create(Car car)
        {
            _context.Add(car);
            _context.SaveChanges();
            var id = from c in _context.Car
                     where c.Equals(car)
                     select c.CarId;

            return id.FirstOrDefault();
        }

        public Car FindById(int id)
        {
            return _context.Car.Find(id);
        }

        public ICollection<Car> All()
        {
            return _context.Car.ToArray();
        }

        public void Update(Car car)
        {
            _context.Update(car);
            _context.SaveChanges();
        }

        public void Delete(int carId)
        {
            var car = FindById(carId);
            _context.Remove(car);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}