using System;
using System.Collections.Generic;
using System.Linq;
namespace LINQ18.Shared
{
    internal class Program
    {
        public static void Main(String[] args)
        {
            ToDectionary();
            Console.ReadKey();
        }
        public static void AsEnumerable()
        {
            ShippingList<Shipping> shippnings = ShippingRepository.AllAsShippingList;
            var todayShipping = shippnings.Where(x => x.ShippingDate == DateTime.Today);
            todayShipping.Process("Today's Shipping");

            var todayShipping02 = shippnings.AsEnumerable().Where(x => x.ShippingDate == DateTime.Today);
            todayShipping02.Process("Today's shipping using IEnumerable<T> where");
        }
        public static void AsQueryrable()
        {
            ShippingList<Shipping> shippnings = ShippingRepository.AllAsShippingList;
            var todayShipping = shippnings.Where(x => x.ShippingDate == DateTime.Today);
            todayShipping.Process("Today's Shipping using ShippingLis<T> Where");


            IQueryable<Shipping> todayShipping02 = shippnings.AsQueryable().Where(x => x.ShippingDate == DateTime.Today);
            todayShipping02.Process("Today's shipping using IEnumerable<T> where");
        }
        public static void Cast()
        {
            IEnumerable<Shipping> shippnings = ShippingRepository.AllAsList;

            var groundShippings = shippnings.Where(x => x.GetType() == typeof(GroundShipping)).Cast<GroundShipping>();

            groundShippings.Process("Ground Shippings");
        }
        public static void OfType()
        {
            IEnumerable<Shipping> shippnings = ShippingRepository.AllAsList;
            var groundShippings = shippnings.OfType<GroundShipping>();
            groundShippings.Process("Ground Shipping");
        }
        public static void ToArray()
        {
            IEnumerable<Shipping> shippnings = ShippingRepository.AllAsList;
            var shippingArray = shippnings.ToArray();
            Console.WriteLine($"Total Shippings: {shippingArray.Length}");
            Console.WriteLine("First Shipping");
            shippingArray[0].Start();
        }
        public static void ToList()
        {
            IEnumerable<Shipping> shippnings = ShippingRepository.AllAsList;
            List<Shipping> shippingArray = shippnings.ToList();
            Console.WriteLine($"Total Shippings: {shippingArray.Count}");
            Console.WriteLine("First Shipping");
            shippingArray[0].Start();
        }

        public static void ToDectionary()
        {
            IEnumerable<Shipping> shippnings = ShippingRepository.AllAsList;
            Dictionary<string, Shipping> dictionary1 = shippnings.ToDictionary(x => x.UniqueId);
            dictionary1["ABC005"].Start();

            Dictionary<DateTime, List<Shipping>> dictionary2 = shippnings.GroupBy(x => x.ShippingDate)
                .ToDictionary(s=>s.Key , s=>s.ToList());
            var date = new DateTime(2022, 3, 9, 0, 0, 0);
            dictionary2[date].Process($"Shppings on {date.ToString("dddd , MMMM  dd yyyy")}");
        }
        public static void ToLookUp()
        {
            IEnumerable<Shipping> shippings = ShippingRepository.AllAsList;

            ILookup<string, Shipping> lookup1 = shippings.ToLookup(x => x.UniqueId);
            lookup1["ABC005"].First().Start();

            ILookup<DateTime, Shipping> lookup2 = shippings.ToLookup(x => x.ShippingDate);

            var date = new DateTime(2022, 3, 9, 0, 0, 0);
            lookup2[date].Process($"Shippings on {date.ToString("dddd, MMMM dd yyyy")}");
            Console.ReadKey();
        }




    }
}