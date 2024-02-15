using System;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using TACSV.Config;

namespace TACSV;

public class InfluxDbManager
{
    private static InfluxDBClient InfluxClient = new("https://influx.biggarf.com", ConfigManager.Options.InfluxDBToken);

    public static void WritePoint(string componentName, string componentReadingType, float value, DateTime timeStamp)
    {
        var point = PointData
            .Measurement(componentName)
            .Field(componentReadingType, value)
            .Timestamp(timeStamp, WritePrecision.Ns);
        InfluxClient.GetWriteApi().WritePoint(point, ConfigManager.Options.Bucket, ConfigManager.Options.Organisation);
    }

    public static void OnMessage(object? sender, string message)
    {
        //Assume message contains data
        //2 components of message
        var messageComponents = message.Split(":");
        messageComponents[1] = messageComponents[1].Replace(";", "");
        
        var componentPrefix = messageComponents[0];
        var data = float.Parse(messageComponents[1]);

        if (ConfigManager.Options.DataPrefixes.TryGetValue(componentPrefix, out var prefix))
        { 
            var prefixParts = prefix.Split(" ");
            WritePoint(prefixParts[0], prefix, data, DateTime.Now);
        }
    }

}