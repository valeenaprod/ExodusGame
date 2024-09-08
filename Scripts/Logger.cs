using System.Runtime.CompilerServices;
using Godot;

namespace Bunkify.Scripts;

public static class Logger
{
    public static void Log(string message,
        string className,
        [CallerMemberName] string methodName = "",
        [CallerLineNumber] int line = 0)
    {
        GD.Print($"[{className}/{methodName}:{line}] : {message}");
    }

    public static void LogError(string message,
        string className,
        [CallerMemberName] string methodName = "",
        [CallerLineNumber] int line = 0)
    {
        GD.PrintErr($"[{className}/{methodName}:{line}] : {message}");
    }

    public static void LogWarning(string message, string className, [CallerMemberName] string methodName = "")
    {
        GD.PushWarning($"[{className}/{methodName}] : {message}");
    }

    public static void GameLog(string message)
    {
        GD.PrintRich($"[b]Update:[/b] {message}");
    }
}