using Microsoft.Maui.Devices;

namespace Lesson38MauiDemoApp.Services
{
    public class DeviceService
    {
        public string GetModel() => DeviceInfo.Model;
        public string GetManufacturer() => DeviceInfo.Manufacturer;
        public string GetOsVersion() => DeviceInfo.VersionString;

        public int? GetBatteryLevel()
        {
            try
            {
                var charge = Battery.ChargeLevel;
                if (charge < 0)
                    return null;
                return (int)(charge * 100);
            }
            catch
            {
                return null;
            }
        }

        public string GetPowerSource()
        {
            try
            {
                return Battery.PowerSource.ToString();
            }
            catch
            {
                return "Unknown";

            }
        }
    }
}
