using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using TACSV.Config;
using Timer = System.Timers.Timer;

namespace TACSV;

public class InfluxDbManager
{
    private static InfluxDBClient InfluxClient = new("https://influx.biggarf.com", ConfigManager.Options.InfluxDBToken);
    private static Queue<Action> uploadQueue = new Queue<Action>();
    private static System.Timers.Timer timer;
    private static object queueLock = new();

    public static void WritePoint(string componentName, string componentReadingType, float value, DateTime timeStamp)
    {
        Trace.WriteLine($"Name: {componentName}, Type: {componentReadingType}, Value: {value}, Time {timeStamp}");
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
        DateTime time = DateTime.Now;
        var messageComponents = message.Split(":");
        if (messageComponents.Length == 1)
        {
            if (message == "INIT;")
            {
                Program.LastInitTime = DateTime.Now;
                TACSVConsole.Log("INIT Recieved!");
            }

            return;
        }

        messageComponents[1] = messageComponents[1].Replace(";", "");
        var componentPrefix = messageComponents[0];
        var data = float.Parse(messageComponents[1]);
        if (ConfigManager.Options.MillisPrefix == componentPrefix)
        {
            Program.LastMillis = data;
        }

        if (Program.LastInitTime != null)
        {
            time = (DateTime)Program.LastInitTime?.AddMilliseconds(Program.LastMillis);
        }

        if (!ConfigManager.Options.DataPrefixes.TryGetValue(componentPrefix, out var prefix)) return;
        var prefixParts = prefix.Split(" ");
        lock (queueLock)
        {
            uploadQueue.Enqueue(() => WritePoint(prefixParts[0], prefix, data, time));
        }
    }

    public static void StartThread()
    {
        timer = new Timer(125);
        timer.Elapsed += ExecuteFirstFunction;
        timer.AutoReset = true;
        timer.Enabled = true;
    }

    private static void ExecuteFirstFunction(object? sender, ElapsedEventArgs elapsedEventArgs)
    {
        lock (queueLock)
        {
            if (uploadQueue.Count > 0)
            {
                var function = uploadQueue.Dequeue();
                try
                {
                    ThreadPool.QueueUserWorkItem(_ => function());
                }
                catch (Exception e)
                {
                    uploadQueue.Enqueue(function);
                }


                //TACSVConsole.Log("Doing the thing father");
            }
        }
    }
}