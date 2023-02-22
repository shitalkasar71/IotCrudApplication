using System;
using Microsoft.Azure.Devices.Shared;
using IOTDeviceCRUD.Repository.IotDeviceProperties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IOTDeviceCRUD.Model;

namespace IOTDeviceCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IotDevicePropertiesController : ControllerBase
    {
        [HttpPut("UpdateDeviceReportedProperties")]
        public async Task<string> UpdateDeviceReportedProperties(string deviceName, ReportedProperties properties)
        {
            await IotDeviceProperties.AddReportedPropertiesAsync(deviceName, properties);
            return null;
        }
        [HttpPut("UpdateDeviceDesiredProperties")]
        public async Task<string> UpdateDeviceDesiredProperties(string deviceName)
        {
            await IotDeviceProperties.DesiredPropertiesUpdate(deviceName);
            return null;
        }
        [HttpPut("UpdateDeviceTagProperties")]
        public async Task<string> UpdateDeviceTagProperties(string deviceName)
        {
            await IotDeviceProperties.UpdateDeviceTagProperties(deviceName);
            return null;
        }
        [HttpGet("GetIotDeviceProperties")]
        public async Task<Twin> GetIotDevice(string deviceName)
        {
           var device=await IotDeviceProperties.GetDevicePropertiesAsync(deviceName);
            return device;
        }
    }
}
