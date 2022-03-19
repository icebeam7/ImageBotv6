global using System;
global using System.IO;
global using System.Text;
global using System.Linq;
global using System.Net.Http;
global using System.Threading.Tasks;
global using System.Collections.Generic;

global using ImageBot.Models;
global using ImageBot.Helpers;
global using ImageBot.Services;

global using Twilio.TwiML;
global using Newtonsoft.Json;

global using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
global using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

global using Microsoft.Azure.WebJobs;
global using Microsoft.Azure.WebJobs.Extensions.Http;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.Logging;
