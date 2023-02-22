using System;

using IOTDeviceCRUD.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;

namespace IOTDeviceCRUD.Repository.IotDeviceProperties
{
    public class IotDeviceProperties
    {
        private static string connectionString = "HostName=demoiothubshital.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=zjm0QkaD2IW4EZqfbZX07iBcc0rOBjFEHc9MGOYujlo=";
        public static RegistryManager registryManager = RegistryManager.CreateFromConnectionString(connectionString);
        public static DeviceClient client = null;
        public static string myDeviceConnection = "HostName=demoiothubshital.azure-devices.net;DeviceId=shital;SharedAccessKey=Kqf66kTabaFfVm7t9/rptdcaIk2E7luSXzurjUfgQX0=";
        //public static async Task AddReportedPropertiesAsync(string deviceName, ReportedProperties properties)
        //{
        //    if (string.IsNullOrEmpty(deviceName))
        //    {
        //        throw new ArgumentNullException("Valid Device Name please"); 
        //    }
        //    else
        //    {
        //        client = DeviceClient.CreateFromConnectionString(myDeviceConnection,Microsoft.Azure.Devices.Client.TransportType.Mqtt);
        //        TwinCollection reportedProperties, connectivity;
        //        reportedProperties = new TwinCollection();
        //        connectivity = new TwinCollection();
        //        connectivity["type"] = "cellular";
        //        reportedProperties["connectivity"] = "connectivity";
        //        reportedProperties["temperature"] = properties.temperature;
        //        reportedProperties["pressure"] = properties.pressure;
        //       // reportedProperties["drift"] = properties.drift;
        //        reportedProperties["accuracy"] = properties.accuracy;
        //        reportedProperties["supplyvoltagelevel"] = properties.supplyvoltagelevel;
        //       // reportedProperties["fillScale"] = properties.fillScale;
        //        reportedProperties["frequency"] = properties.frequency;
        //       // reportedProperties["SensorType"] = properties.sensorType;
        //        reportedProperties["dataTimeLastApplaunch"] = properties.dataTimeLastApplaunch;
        //      //  reportedProperties["resolution"] = properties.resolution;
        //        await client.UpdateReportedPropertiesAsync(reportedProperties);
        //        return;
        //    }
        //}

        public static async Task AddReportedPropertiesAsync(string deviceName, ReportedProperties properties)
        {
            if (string.IsNullOrEmpty(deviceName)) 
            { throw new ArgumentNullException("Valid device name please");
            }
            else
            {
                client = DeviceClient.CreateFromConnectionString(myDeviceConnection, Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                TwinCollection reportedProperties, connectivity;
                reportedProperties = new TwinCollection(); 
                connectivity = new TwinCollection(); 
                connectivity["type"] = "cellular"; 
                reportedProperties["connectivity"] = "connectivity";
                reportedProperties["temperature"] = properties.temperature; 
                reportedProperties["pressure"] = properties.pressure;        
                // reportedProperties["drift"]=properties.drift;         
                    reportedProperties["accuracy"]=properties.accuracy;
                 // reportedProperties["supplyVoltageLevel"] = properties.supplyvoltagelevel;            
                // reportedProperties["fullScale"]=properties.fullScale;           
                     reportedProperties["frequency"]=properties.frequency;          
                // reportedProperties["resolution"]=properties.resolution;          
                // reportedProperties["sensorType"]=properties.sensorType;             
                  reportedProperties["dateTimeLastAppLaunch"]=properties.dataTimeLastApplaunch;         
                await client.UpdateReportedPropertiesAsync(reportedProperties);           
                return;             
            }     
        }

        public static async Task DesiredPropertiesUpdate(string deviceName)
        {
            client = DeviceClient.CreateFromConnectionString(myDeviceConnection, Microsoft.Azure.Devices.Client.TransportType.Http1);
            //client = DeviceClient.CreateFromConnectionString(myDeviceConnection, Microsoft.Azure.Devices.Client.TransportType.Http1);
            var device = await registryManager.GetTwinAsync(deviceName);
            TwinCollection desiredProperties, telemetryconfig;
            desiredProperties = new TwinCollection();
            telemetryconfig = new TwinCollection();
            telemetryconfig["frequency"] = "5hz";
            desiredProperties["telemetryconfig"] = telemetryconfig;
            device.Properties.Desired["telemetryconfig"] = telemetryconfig;
            await registryManager.UpdateTwinAsync(device.DeviceId,device,device.ETag);
            return;
        }
        public static async Task<Twin>GetDevicePropertiesAsync(string deviceName)
        {
            var device = await registryManager.GetTwinAsync(deviceName);
            return device;
        }
        public static async Task UpdateDeviceTagProperties(string deviceName)
        {
            if (string.IsNullOrEmpty(deviceName))
            {
                throw new ArgumentNullException("Device device name please");
            }
            else
            {
                var twin = await registryManager.GetTwinAsync(deviceName);
                var patchData =
                    @"{
                         tags:{
                           location:{
                               region:'canada',
                               plant:'IOTPro'
                            }
                          }
                      }";
                client =DeviceClient.CreateFromConnectionString(myDeviceConnection, Microsoft.Azure.Devices.Client.TransportType.Http1);
                TwinCollection connctivity;
                connctivity = new TwinCollection();
                connctivity["type"] = "cellular";
                await registryManager.UpdateTwinAsync(twin.DeviceId, patchData, twin.ETag);
                return;
            }
        }
    }
}
 