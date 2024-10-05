using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;

namespace ExodusGame.Scripts.Utils;

public static class Logger
{
    public enum LogLevel { Info, Warning, Error, Debug}

    private static string _logFilePath = "user://log.txt";

    public static void Log(string message, LogLevel level = LogLevel.Info, 
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0)
    {
        string logMessage;

        if (level == LogLevel.Info)
        {
            logMessage = $"{DateTime.Now}: [{level}] {message}";
        }
        else
        {
            logMessage = $"{DateTime.Now}: [{level}] {message} " +
                         $"(Caller: {memberName}, {filePath}, line {lineNumber}";
        }
            

        switch (level)
        {
            case LogLevel.Error:
                GD.PrintErr(logMessage);
                break;
            case LogLevel.Warning:
                GD.PushWarning(logMessage);
                break;
            case LogLevel.Info:
            case LogLevel.Debug:
            default:
                GD.Print(logMessage);
                break;
        }

        WriteToFile(logMessage);


    }

    private static async void WriteToFile(string message)
    {
        await Task.Run(() =>
        {
            try
            {
                using var logFile = FileAccess.Open(_logFilePath, FileAccess.ModeFlags.WriteRead);
                logFile.Seek(logFile.GetLength()); // Move to end for appending
                logFile.StoreLine(message);
            }
            catch (Exception e)
            {
                GD.PrintErr($"Failed to write to log file: {e.Message}");
            }
        });

    }
}