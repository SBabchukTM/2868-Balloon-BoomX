using Tools.ObjectRenamer;
using UnityEditor;


public class ObjectRenamer
{
    [MenuItem("Tools/Object Renamer")]
    public static void CreatePopup()
    {
        ObjectRenamerWindow.InitWindow();
    }
}