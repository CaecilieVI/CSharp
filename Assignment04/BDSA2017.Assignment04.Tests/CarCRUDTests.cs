using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace BDSA2017.Assignment04.Tests
{
    public class CarCRUDTests
    {
        Slot_Car_TournamentContext context = new Slot_Car_TournamentContext();

        [Fact(DisplayName = "Create_given_car_returns_id")]
        public void Create_given_car_returns_id()
        {
            var cc = new CarCRUD(context);

            var car = new Car { Name = "Car One", DriverName = "Driver One" };

            int expected = car.CarId;
            int actual = cc.Create(car);

            Assert.Equal(expected, actual);
            context.Remove(car);
            context.SaveChanges();
        }
        
        [Fact(DisplayName = "FindById_given_id_returns_car")]
        public void FindById_given_id_returns_car()
        {
            var cc = new CarCRUD(context);

            var car = new Car { Name = "Car Two", DriverName = "Driver Two" };

            cc.Create(car);

            var expected = car;
            var actual = cc.FindById(car.CarId);

            Assert.Equal(expected.CarId, actual.CarId);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.DriverName, actual.DriverName);
            context.Remove(car);
            context.SaveChanges();
        }

        [Fact(DisplayName = "All_returns_collection_of_cars")]
        public void All_returns_collection_of_cars()
        {
            var cc = new CarCRUD(context);

            ICollection<Car> expected = context.Car.ToArray();
            ICollection<Car> actual = cc.All();

            Assert.Equal(expected, actual);
        }

        [Fact(DisplayName = "Update_given_car_updates_car")]
        public void Update_given_car_updates_car()
        {
            var cc = new CarCRUD(context);

            var car = new Car { Name = "Car Three", DriverName = "Driver Three" };

            cc.Create(car);
            
            car.Name = "Super Awesome Car";
            cc.Update(car);

            var expected = "Super Awesome Car";
            var actual = car.Name;

            Assert.Equal(expected, actual);
            context.Remove(car);
            context.SaveChanges();
        }

        [Fact(DisplayName = "Delete_given_carid_deletes_car")] 
        public void Delete_given_carid_deletes_car()
        {
            var cc = new CarCRUD(context);

            var car = new Car { Name = "Car Four", DriverName = "Driver Four" };

            cc.Create(car);

            var carToDelete = new Car { Name = "Car Four", DriverName = "Driver Four" };
            cc.Delete(carToDelete.CarId);
            bool notDeleted = context.Car.Any(c => c.CarId == carToDelete.CarId);

            Assert.False(notDeleted);
        }
    }
}