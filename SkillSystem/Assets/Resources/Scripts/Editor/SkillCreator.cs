using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SkillCreator : EditorWindow
{
    private const string skillPrefabPath = "Assets/Resources/Prefabs/Skills";
    private const string skillSettingPath = "Assets/Resources/Prefabs/SkillSettings";
    private const string skillTemplatePath = "Assets/Resources/Prefabs/SkillTemplate.prefab";
    private const string skillSettingTemplatePath = "Assets/Resources/Prefabs/SkillSettingTemplate.asset";
    private const string skillScriptsPath = "Scripts/Skills";
    private const string talentScriptsPath = "Scripts/Talents";
    private const string talentPrefabPath = "Assets/Resources/Prefabs/Talents";

    private int currentTab = 0;
    private int selectedSkillIndex = 0;
    private List<string> skillNames = new List<string>();
    private MonoScript[] skillScripts;
    private SkillSetting baseSkillSetting;
    private float amount = 0;
    private Skill.StatsType statsType = Skill.StatsType.None;

    private int selectedTalentIndex = 0;
    private List<string> talentNames = new List<string>();
    private MonoScript[] talentScripts;
    private float talentAmount = 0;
    private string talentName;

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

        talentNames.Clear();
        talentScripts = Resources.LoadAll<MonoScript>(talentScriptsPath);
        for (int i = 0; i < talentScripts.Length; i++)
        {
            talentNames.Add(talentScripts[i].GetClass().ToString());
        }

        if (baseSkillSetting == null)
            baseSkillSetting = AssetDatabase.LoadAssetAtPath<SkillSetting>(skillSettingTemplatePath);
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

            // dropdown to select skill scripts
            selectedSkillIndex = EditorGUILayout.Popup("Skill script:", selectedSkillIndex, skillNames.ToArray());

            // input fields for skill settings
            if (baseSkillSetting != null)
            {
                baseSkillSetting.skillName = EditorGUILayout.TextField("Skill Name:", baseSkillSetting.skillName);
                baseSkillSetting.desc = EditorGUILayout.TextField("Skill Descriptions:", baseSkillSetting.desc);
                baseSkillSetting.mana = float.Parse(EditorGUILayout.TextField("Mana Cost:", baseSkillSetting.mana.ToString()));
                baseSkillSetting.cooldown = float.Parse(EditorGUILayout.TextField("Cooldown:", baseSkillSetting.cooldown.ToString()));
            }

            // other skill details
            amount = float.Parse(EditorGUILayout.TextField("Extra amount on skill:", amount.ToString()));
            statsType = (Skill.StatsType)EditorGUILayout.EnumPopup("Skill Stats Type for Talent:", statsType);
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space(50f);
            // create skill button with logic
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Create Skill", GUILayout.Height(60f)))
            {
                GameObject template = PrefabUtility.LoadPrefabContents(skillTemplatePath);
                if (template != null)
                {
                    GameObject go = Instantiate(template);
                    Skill skill = go.AddComponent(skillScripts[selectedSkillIndex].GetClass()) as Skill;

                    string path = skillPrefabPath + $"/{baseSkillSetting.skillName}.prefab";
                    path = AssetDatabase.GenerateUniqueAssetPath(path);

                    string settingPath = skillSettingPath + $"/{baseSkillSetting.skillName}.asset";
                    settingPath = AssetDatabase.GenerateUniqueAssetPath(settingPath);
                    AssetDatabase.CopyAsset(skillSettingTemplatePath, settingPath);
                    skill.AddSkillSetting(AssetDatabase.LoadAssetAtPath<SkillSetting>(settingPath));
                    skill.SetAmounts(amount);
                    skill.statType = statsType;

                    bool result;
                    PrefabUtility.SaveAsPrefabAssetAndConnect(go, path, InteractionMode.UserAction, out result);
                    if (result)
                        DestroyImmediate(go);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        else if (currentTab == 1)
        {
            EditorGUILayout.LabelField("Create a Talent object to attach on Skill in game");
            EditorGUILayout.Space(20f);
            EditorGUILayout.BeginVertical();

            // dropdown to select talent scripts
            selectedTalentIndex = EditorGUILayout.Popup("Talent script:", selectedTalentIndex, talentNames.ToArray());

            // other talent details
            talentName = EditorGUILayout.TextField("Talent Name:", talentName);
            talentAmount = float.Parse(EditorGUILayout.TextField("Modifier amount on talent:", talentAmount.ToString()));
            statsType = (Skill.StatsType)EditorGUILayout.EnumPopup("Stats Type for Talent:", statsType);
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space(50f);
            // create skill button with logic
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Create Talent", GUILayout.Height(60f)))
            {
                GameObject template = PrefabUtility.LoadPrefabContents(skillTemplatePath);
                if (template != null)
                {
                    GameObject go = Instantiate(template);
                    Talent tal = go.AddComponent(talentScripts[selectedTalentIndex].GetClass()) as Talent;

                    string path = talentPrefabPath + $"/{talentName}.prefab";
                    path = AssetDatabase.GenerateUniqueAssetPath(path);
                    tal.statType = statsType;
                    tal.modifierAmount = talentAmount;

                    bool result;
                    PrefabUtility.SaveAsPrefabAssetAndConnect(go, path, InteractionMode.UserAction, out result);
                    if (result)
                        DestroyImmediate(go);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
