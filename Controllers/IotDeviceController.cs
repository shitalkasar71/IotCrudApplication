using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices;
using IOTDeviceCRUD.Repository;


namespace IOTDeviceCRUD.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [Route("[controller]")]
    public class IotDeviceController : ControllerBase
    {
        [HttpPost("AddIotDevice")]
        public async Task<string> AddDevice(string deviceName)
        {
            await IOTDeviceCRUD.Repository.IotDevice.AddDeviceAsync(deviceName);
            return null;
        }
        [HttpGet("GetIotDevice")]
        public async Task<Device> GetIotDevice(string deviceName)
        {
            Device device;
            device= await IOTDeviceCRUD.Repository.IotDevice.GetDeviceAsync(deviceName);
            return device;
        }
        [HttpDelete("DeleteIotDevice")]
        public async Task<string> DeleteIotDevice(string deviceName)
        {
           await IOTDeviceCRUD.Repository.IotDevice.DeleteDeviceAsync(deviceName);
            return null;
        }
        [HttpPut("UpdateIotDevice")]
        public async Task<Device> UpdateIotDevice(string deviceName)
        {
            Device device;
            device = await IOTDeviceCRUD.Repository.IotDevice.UpdateDeviceAsync(deviceName);
            return device;
        }
    }
}
