using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BilgeAdam.Data.Expressions
{
    class Person
    {
        private static int id;
        private readonly Random r = StaticRandom.Instance;
        public Person(string name)
        {
            Id = ++id;
            BirthDate = new DateTime(DateTime.Now.Year - r.Next(10, 30),
                                     r.Next(1,12),
                                     r.Next(1,28));
            FullName = name;
        }
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public static class StaticRandom
    {
        private static int seed;

        private static ThreadLocal<Random> threadLocal = new ThreadLocal<Random>
            (() => new Random(Interlocked.Increment(ref seed)));

        static StaticRandom()
        {
            seed = Environment.TickCount;
        }

        public static Random Instance { get { return threadLocal.Value; } }
    }
}
