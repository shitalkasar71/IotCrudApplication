using System;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using IOTDeviceCRUD.Model;
using System.Text;
using Newtonsoft.Json;

namespace IOTDeviceCRUD.Repository.SendTelemetryMessages
{
    public class SendTelemetryMessages
    {
        private static string connectionString = "HostName=shital-iot-hub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=AU0MPRpmg/iqEKgBjxzOmslQo/UgJJ/ygztMD//A0mk=";
        public static RegistryManager registryManager;
        public static DeviceClient client = null;
        public static string myDeviceConnection = "HostName=shital-iot-hub.azure-devices.net;DeviceId=Test;SharedAccessKey=NGgych2tPGAK0yEE+nrXbcq0v07rEGZEv5TV1CDBMTI=";
        public static async Task SendMessage(string deviceName)
        {
            try
            {            
              registryManager = RegistryManager.CreateFromConnectionString(connectionString);
              var device = await registryManager.GetTwinAsync(deviceName);
              ReportedProperties properties = new ReportedProperties();
              TwinCollection reportedProp;
              reportedProp = device.Properties.Reported;
              client = DeviceClient.CreateFromConnectionString(myDeviceConnection, Microsoft.Azure.Devices.Client.TransportType.Mqtt);
              while(true)
              {
                var telemetry = new
                {
                    temperature = reportedProp["temperature"],
                    pressure = reportedProp["pressure"],
                  //  drift = reportedProp["drift"],
                    accuracy = reportedProp["accuracy"],
                 //   supplyvoltagelevel = reportedProp["supplyvoltagelevel"],
                  //  fillScale = reportedProp["fillScale"],
                    frequency = reportedProp["frequency"]
                   // resolution = reportedProp["resolution"]
                   // sensorType = reportedProp["sensorType"]
                };
                var telemetryString = JsonConvert.SerializeObject(telemetry);
                var message = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(telemetryString));
                await client.SendEventAsync(message);
                Console.WriteLine("{0}>sending message:{1}", DateTime.Now, telemetryString);
                await Task.Delay(1000);                   
              }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}
