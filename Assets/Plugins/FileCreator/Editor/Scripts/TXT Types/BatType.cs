using System.IO;
using UnityEditor;

public class BatType : CreatorProfile
{
    [MenuItem("Assets/Create/Text Files/BatType", priority = 31)]
    public static void CreateMenu()
    {
        FileCreator.GenerateWindow<BatType>("bat");
    }

    public override void GenerateFile(string path, string scriptName)
    {
        using StreamWriter outfile = new StreamWriter(path);
        outfile.WriteLine("");
    }
}
