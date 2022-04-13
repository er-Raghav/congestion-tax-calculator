using System;
using System.Collections.Generic;
using congestion.calculator;
using congestion_tax_api.Models;

public class CongestionTaxCalculator : ICongestionTaxCalculator
{
    /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total congestion tax for that day
         */

    public int GetTax(string vehicle, DateTime[] dates)
    {
        var _rates = TaxRates.lstTaxRates;
        int totalFee = 0;
        DateTime intervalStart = dates[0];
        foreach (DateTime date in dates)
        {            
            int nextFee = GetTollFee(date, vehicle, _rates);
            int tempFee = GetTollFee(intervalStart, vehicle, _rates);

            double diffInMinutes = date.Subtract(intervalStart).TotalMinutes;

            if (diffInMinutes <= 60)
            {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                totalFee += tempFee;
            }
            else
            {

                totalFee += nextFee;
            }
            intervalStart = date;
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    private bool IsTollFreeVehicle(string vehicle)
    {
        if (vehicle == null) return false;
        return vehicle.Equals(TollFreeVehicles.Motorcycle.ToString()) ||
               vehicle.Equals(TollFreeVehicles.Tractor.ToString()) ||
               vehicle.Equals(TollFreeVehicles.Emergency.ToString()) ||
               vehicle.Equals(TollFreeVehicles.Diplomat.ToString()) ||
               vehicle.Equals(TollFreeVehicles.Foreign.ToString()) ||
               vehicle.Equals(TollFreeVehicles.Military.ToString());
    }

    public int GetTollFee(DateTime date, string vehicle, List<TaxRateCard> rates)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;
        foreach (var rate in rates)
        {
            if (IsEligibleforTollFee(date, rate))
            {
                return rate.amount;
            };
        }
        return 0;
        
    }

    private bool IsEligibleforTollFee(DateTime date, TaxRateCard taxRateCard)
    {

        TimeSpan start = new TimeSpan(taxRateCard.startHour, taxRateCard.startMinute, 0);
        TimeSpan end = new TimeSpan(taxRateCard.endHour, taxRateCard.endMinute, 0);
        TimeSpan current = date.TimeOfDay;
        if (current >= start && current <= end)
        {
            return true;
        }
        return false;
    }

    private Boolean IsTollFreeDate(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        if (year == 2013)
        {
            if (month == 1 && day == 1 ||
                month == 3 && (day == 28 || day == 29) ||
                month == 4 && (day == 1 || day == 30) ||
                month == 5 && (day == 1 || day == 8 || day == 9) ||
                month == 6 && (day == 5 || day == 6 || day == 21) ||
                month == 7 ||
                month == 11 && day == 1 ||
                month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
            {
                return true;
            }
        }
        return false;
    }

    private enum TollFreeVehicles
    {
        Motorcycle = 0,
        Tractor = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5
    }
}