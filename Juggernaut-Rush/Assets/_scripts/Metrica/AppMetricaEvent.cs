using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AppMetricaEvent
{
    public static void LevelStart(int Level)
    {
        var tutParms = new Dictionary<string, object>();
        tutParms["Level Namber"] = Level.ToString();

        AppMetrica.Instance.ReportEvent("level_start",tutParms);
        AppMetrica.Instance.SendEventsBuffer();
    }
    public static void LevelFinish(int Level)
    {
        var tutParms = new Dictionary<string, object>();
        tutParms["Level Namber"] = Level.ToString();
        
        AppMetrica.Instance.ReportEvent("level_finish", tutParms);
        AppMetrica.Instance.SendEventsBuffer();
    }
}
