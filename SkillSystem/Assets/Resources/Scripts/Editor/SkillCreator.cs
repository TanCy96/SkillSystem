using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SkillCreator : EditorWindow
{
    private const string skillPrefabPath = "Assets/Resources/Prefabs/Skills";
    private const string skillTemplatePath = "Assets/Resources/Prefabs/SkillTemplate.prefab";
    private const string skillScriptsPath = "Scripts/Skills";

    private int currentTab = 0;
    private int selectedSkillIndex = 0;
    private List<string> skillNames = new List<string>();
    private MonoScript[] skillScripts;

    [MenuItem("Project/SkillCreator", false)]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        SkillCreator window = (SkillCreator)EditorWindow.GetWindow(typeof(SkillCreator));
        window.Show();
        window.Awake();
    }

    private void Awake()
    {
        skillNames.Clear();
        skillScripts = Resources.LoadAll<MonoScript>(skillScriptsPath);
        for (int i = 0; i < skillScripts.Length; i++)
        {
            skillNames.Add(skillScripts[i].GetClass().ToString());
        }
    }

    private void OnGUI()
    {
        EditorGUILayout.Space();

        currentTab = GUILayout.Toolbar(currentTab, new string[] { "Skill", "Talent" });
        if (currentTab == 0)
        {
            EditorGUILayout.LabelField("Create a Skill object");
            EditorGUILayout.Space(20f);
            EditorGUILayout.BeginVertical();

            Rect dropdownRect = EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Skill script");
            string[] test = new string[] { "DamageSkill", "MobilitySkill", "StatSkill", "SupportSkill" };
            selectedSkillIndex = EditorGUI.Popup(new Rect(dropdownRect.width/2, dropdownRect.y, 180, 80), selectedSkillIndex, skillNames.ToArray());
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Create Skill", GUILayout.Height(60f)))
            {
                GameObject template = PrefabUtility.LoadPrefabContents(skillTemplatePath);
                if (template != null)
                {
                    GameObject go = Instantiate(template);
                    go.AddComponent(skillScripts[selectedSkillIndex].GetClass());

                    string path = skillPrefabPath + "/Test.prefab";
                    path = AssetDatabase.GenerateUniqueAssetPath(path);

                    bool result;
                    PrefabUtility.SaveAsPrefabAssetAndConnect(go, path, InteractionMode.UserAction, out result);
                    Debug.Log($"RESULT {result}");
                    if (result)
                        DestroyImmediate(go);
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
    }
}
