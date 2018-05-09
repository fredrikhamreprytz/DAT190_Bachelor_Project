using System;
using SkiaSharp;
using PCLAppConfig;
using DAT190_Bachelor_Project.Model;

namespace DAT190_Bachelor_Project.Bakery
{
    public static class CakeUtil
    {
        public static SKColor GetColor(IEmission emission)
        {

            switch (emission.Type)
            {
                case EmissionType.Flight:
                    return SKColor.Parse(ConfigurationManager.AppSettings["FlightCakeColor"]);
                case EmissionType.Fuel:
                    return SKColor.Parse(ConfigurationManager.AppSettings["FuelCakeColor"]);
                case EmissionType.Household:
                    return SKColor.Parse(ConfigurationManager.AppSettings["HouseholdCakeColor"]);
                default:
                    return SKColors.Transparent;
            }
        }

        public static string GetTitle(IEmission emission)
        {

            switch (emission.Type)
            {
                case EmissionType.Flight:
                    return ConfigurationManager.AppSettings["FlightTitleText"];
                case EmissionType.Fuel:
                    return ConfigurationManager.AppSettings["FuelTitleText"];
                case EmissionType.Household:
                    return ConfigurationManager.AppSettings["HouseholdTitleText"];
                default:
                    return "";
            }
        }

        public static SKPath GetSVGPath(IEmission emission)
        {

            switch (emission.Type)
            {
                case EmissionType.Flight:
                    return SKPath.ParseSvgPathData(ConfigurationManager.AppSettings["FlightIconSVG"]);
                case EmissionType.Fuel:
                    return SKPath.ParseSvgPathData(ConfigurationManager.AppSettings["FuelIconSVG"]);
                case EmissionType.Household:
                    return SKPath.ParseSvgPathData(ConfigurationManager.AppSettings["HouseholdIconSVG"]);
                default:
                    return SKPath.ParseSvgPathData("");
            }
        }
    }
}
