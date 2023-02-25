using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_3
{
    static class CorrectInfo
    {

        public static string Name;
        public static string Surname;
        public static string Parameter;
        public static string DateOfBirth;
        public static string Role;
        public static string Gender;
        public static string ID;

        public static K FindTheKey<K, V>(this Dictionary<K, V> dict, V val)
        {
            return dict.FirstOrDefault(entry =>
                EqualityComparer<V>.Default.Equals(entry.Value, val)).Key;
        }
    }

}
