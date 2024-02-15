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
}