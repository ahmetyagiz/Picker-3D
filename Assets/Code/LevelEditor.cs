using UnityEngine;
using UnityEditor;

public class LevelEditor : EditorWindow
{
    private GameObject prefabObject;
    private GameObject instantiatedPrefab;

    [MenuItem("Custom/Edit Level")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(LevelEditor));
    }

    private void OnGUI()
    {
        GUILayout.Label("Select a level to edit:", EditorStyles.boldLabel);
        prefabObject = EditorGUILayout.ObjectField("Prefab:", prefabObject, typeof(GameObject), false) as GameObject;

        if (GUILayout.Button("Create Selected Level") && prefabObject != null)
        {
            OpenPrefabEditor();
        }

        if (instantiatedPrefab != null && GUILayout.Button("Save Changes"))
        {
            SaveChangesToPrefab();
        }
    }

    private void OpenPrefabEditor()
    {
        instantiatedPrefab = PrefabUtility.InstantiatePrefab(prefabObject) as GameObject;
    }

    private void SaveChangesToPrefab()
    {
        PrefabUtility.SaveAsPrefabAsset(instantiatedPrefab, AssetDatabase.GetAssetPath(prefabObject));
        DestroyImmediate(instantiatedPrefab);
        Debug.Log("Changes to the level have been saved!");
    }
}
