using System;
using System.Collections.Generic;
using System.Text;

namespace NewsApp
{
  public  class ClassCiti
    {
      static  Dictionary<string, string> countries = new Dictionary<string, string>
        {
            ["Москва"] = "moscow",
            ["Германия"] = "Берлин",
            ["Великобритания"] = "Лондон"
        };
        public static string Citi(string value)
        {
            return countries[value]; 
        }
    }
}
