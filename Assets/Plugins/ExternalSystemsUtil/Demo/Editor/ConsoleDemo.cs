using Arcturus.ExternalSystems;
using UnityEditor;
using UnityEngine;

public static class ConsoleDemo
{
    [MenuItem("ExternalSystemsUtil/Open Demo Cmd")]
    public static void Testc()
    {
        ExternalSystemsUtil.OpenCmd(@$"{Application.streamingAssetsPath}", "ExampleBatch.bat");
    }
}
