#r "Newtonsoft.Json"

using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

class Sentimiento 
{ 
    public string negative { get; set; } 
    public string positive { get; set; }
    public string neutral {get; set; }
} 

public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{

    string requestBody = String.Empty;
    using (StreamReader streamReader =  new  StreamReader(req.Body))
    {
        requestBody = await streamReader.ReadToEndAsync();
    }

    dynamic score = JsonConvert.DeserializeObject<Sentimiento>(requestBody);
    string value = "neutral";
    double positive =Double.Parse(score.positive);
    double negative =Double.Parse(score.negative);
    double neutral =Double.Parse(score.neutral);
    if(positive > negative && positive > neutral)
    {
        value = "Positive";
    }
    else if (negative > positive && negative > neutral) 
    {
        value = "Negative";
    }
    

    return requestBody != null
        ? (ActionResult)new OkObjectResult(value)
       : new BadRequestObjectResult("Pass a sentiment score in the request body.");
}