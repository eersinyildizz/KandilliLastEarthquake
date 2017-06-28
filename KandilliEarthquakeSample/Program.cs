using DAL.Content.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HtmlParseSample
{
    class Program
    {
       
        static void Main(string[] args)
        {
            
            KandilliDeprem deprem = new KandilliDeprem();

            string[] lines = deprem.GetEarthquakes();
            var result = deprem.getDetail(lines);
            deprem.Add(result.Item1,result.Item2);


            for (int x = 0; x < result.Item2; x++)
            {
                Console.WriteLine("Last Earthquake :");
                Console.WriteLine($"Address : {result.Item1[x].Address}  Magnitude : {result.Item1[x].Magnitude} Time :{result.Item1[x].Time.ToShortTimeString()}");
            }



            Console.ReadLine();
        }
    }
}
