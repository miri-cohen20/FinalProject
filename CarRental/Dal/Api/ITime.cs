using Dal.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    internal interface ITime
    {
        int GetIdOfDuration(string duration);
        string GetIdDuration(int id);
        List<string> GetAllDuration();
        List<Time> GetAllTime();


    }
}   
