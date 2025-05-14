using Dal.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface IPrice
    {
        double GetPriceForHour(int seats, int time);
        int GetIdPrice(int seats, int time);
        double GetPriceForHourById(int id);
        int GetSeatsById(string id);
        int GetTimeById(int id);
        List<Price> GetAllPrices();
        List<int> GetAllIdPrice();



    }
}
