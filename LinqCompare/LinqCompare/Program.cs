using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqCompare;

namespace ConsoleApplication8
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //List<int> list1 = new List<int>();
                //list1.Add(1);
                //list1.Add(2);
                //list1.Add(3);
                //List<int> list2 = new List<int>();
                //list2.Add(3);
                //list2.Add(4);
                //list2.Add(5);
                ////得到的结果是4,5 即减去了相同的元素。
                //List<int> list3 = list2.Except(list1).ToList();

                List<wage> list1 = new List<wage>()
                {
                    new wage () { id = 1, card = "card1", name = "name1", price = 1 },
                    new wage () { id =2,  name = "name2", price = 2 },
                    new wage () { id = 3, card = "card3", name = "name3", price = 3 }
                };
                List<wage> list2 = new List<wage>()
                {
                    new wage () { id = 3, card = "card3", name = "name3", price = 33 },
                    new wage () { id = 4, card = "card4", name = "name4", price = 44 }
                };
                // 求合集，并且去重复
                //var result = list1.Union(list2, new MyComparer()).Except(list1.Intersect(list2, new MyComparer()), new MyComparer());
                //// 求交集
                //var result1 = list1.Intersect(list2, new MyComparer<wage>("id"));
                //foreach (var item in result1)
                //{
                //    Console.WriteLine("id = {0}, card = {1}, name = {2}, price = {3}", item.id, item.card, item.name, item.price);
                //}
                // 求差集
                var result2 = list1.Except(list2, new MyComparer<wage>());
                foreach (var item in result2)
                {
                    Console.WriteLine("id = {0}, card = {1}, name = {2}, price = {3}", item.id, item.card, item.name, item.price);
                }
                Console.Read();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class wage
    {
        public int id { set; get; }
        public string card { set; get; }
        public string name { set; get; }
        public decimal price { set; get; }
    }

    class MyComparer1 : IEqualityComparer<wage>
    {
        public bool Equals(wage x, wage y)
        {
            return x.card == y.card && x.name == y.name && x.price == y.price;
            //return x.card == y.card && x.name == y.name;
        }

        public int GetHashCode(wage obj)
        {
            return obj.card.GetHashCode() ^ obj.name.GetHashCode() ^ obj.price.GetHashCode();
            //return obj.card.GetHashCode() ^ obj.name.GetHashCode();
        }
    }
}