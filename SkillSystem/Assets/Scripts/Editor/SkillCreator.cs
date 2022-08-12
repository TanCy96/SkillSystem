using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SkillCreator : EditorWindow
{
    [MenuItem("Project/SkillCreatorr", false)]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        SkillCreator window = (SkillCreator)EditorWindow.GetWindow(typeof(SkillCreator));
        window.Show();
    }

    private void OnGUI()
    {
        
    }
}
