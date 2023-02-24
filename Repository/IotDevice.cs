using System;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;

namespace IOTDeviceCRUD.Repository
{
    public class IotDevice
    {
        public static RegistryManager registryManager;
        private static string connectionString = "HostName=shital-iot-hub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=xlo3mPK3X5BnSMB22EXV7EoVPhLxh0Iw2PgnTcTQmZk=";
        //static Device  device;

        public static async Task AddDeviceAsync(string deviceName)
        {
            Device device;
            if (string.IsNullOrEmpty(deviceName))
            {
                throw new ArgumentNullException("Device Name please");
            }
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            device = await registryManager.AddDeviceAsync(new Device(deviceName));
            return;
        }

        public static async Task<Device> GetDeviceAsync(string deviceId)
        {
            Device device;
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            device = await registryManager.GetDeviceAsync(deviceId);
            return device;
        }
        public static async Task DeleteDeviceAsync(string deviceId)
        {           
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            await registryManager.RemoveDeviceAsync((deviceId));          
        }
        public static async Task<Device> UpdateDeviceAsync(string deviceId)
        {
            if(string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentNullException("Device id please");
            }
            Device device= new Device(deviceId);
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            device = await registryManager.GetDeviceAsync(deviceId);
            device.StatusReason = "Updated";
            device = await registryManager.UpdateDeviceAsync(device);
            return device;
        }
    }
}
