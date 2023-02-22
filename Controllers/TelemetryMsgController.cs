using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices;
using IOTDeviceCRUD.Repository.SendTelemetryMessages;

namespace IOTDeviceCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelemetryMsgController : ControllerBase
    {
        [HttpPost("SendTelemetryMessage")]
        public async Task<string> SendMessage(string deviceName)
        {
            await SendTelemetryMessages.SendMessage(deviceName);
            return null;
        }
    }
}
 