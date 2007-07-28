using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatchIt
{
    class Program
    {
        static void Main(string[] args)
        {
            var res1 = from mi in typeof(System.Linq.Enumerable).GetMethods() orderby mi.Name group mi by mi.Name into g select new { Name = g.Key, Overloads = g.Count() };
            foreach (var mi in res1)
                Console.WriteLine("{0} has {1} overloads", mi.Name, mi.Overloads);

            var res2 = from mi in typeof(BdsSoft.Linq.Enumerable).GetMethods() orderby mi.Name group mi by mi.Name into g select new { Name = g.Key, Overloads = g.Count() };
            foreach (var mi in res2)
                Console.WriteLine("{0} has {1} overloads", mi.Name, mi.Overloads);

            //Console.WriteLine(res1.SequenceEqual(res2));

            var mismatches = from m1 in res1
                             join m2 in res2 on m1.Name equals m2.Name
                             where m1.Overloads != m2.Overloads
                             select new { m1.Name, Theirs = m1.Overloads, Ours = m2.Overloads };

            foreach (var m in mismatches)
                Console.WriteLine("{0} has {1} overloads instead of {2}", m.Name, m.Ours, m.Theirs);
        }
    }
}
