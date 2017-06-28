using DAL.Content.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlParseSample
{
    public interface IKandilliDeprem<T> where T : Earthquake
    {
        string[] GetEarthquakes();

        Tuple<List<T>, int> getDetail(string[] strLines);

        void Add(List<T> items, int startIndex);


    }
}
