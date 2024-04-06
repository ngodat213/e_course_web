using System;
using System.Diagnostics;

public static class VideoHelper
{
    public static TimeSpan GetDuration(string videoFilePath)
    {
        try
        {
            var ffProbeProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "ffprobe",
                    Arguments = $"-v error -show_entries format=duration -of default=noprint_wrappers=1:nokey=1 \"{videoFilePath}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            ffProbeProcess.Start();
            string durationString = ffProbeProcess.StandardOutput.ReadToEnd();
            ffProbeProcess.WaitForExit();

            if (double.TryParse(durationString, out double durationInSeconds))
            {
                TimeSpan duration = TimeSpan.FromSeconds(durationInSeconds);
                return duration;
            }
            else
            {
                throw new Exception("Unable to parse video duration.");
            }
        }
        catch (Exception ex)
        {
            // Handle exception
            throw new Exception("Error getting video duration.", ex);
        }
    }

    public static int GetHour(string videoFilePath)
    {
        return GetDuration(videoFilePath).Hours;
    }

    public static int GetMinute(string videoFilePath)
    {
        return GetDuration(videoFilePath).Minutes;
    }
}
