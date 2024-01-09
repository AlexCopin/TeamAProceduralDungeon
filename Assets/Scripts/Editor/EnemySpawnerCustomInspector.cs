using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawner_Inspector : Editor
{
    SerializedProperty lookAtPoint;
    int indexSpawnPoint = 0;
    void OnEnable()
    {
        lookAtPoint = serializedObject.FindProperty("lookAtPoint");
    }

    public override void OnInspectorGUI()
    {
        EnemySpawner myTarget = (EnemySpawner)target;
        
        myTarget._difficulty = EditorGUILayout.Slider("Difficulty", myTarget._difficulty, 0, 1);
        if (GUILayout.Button("Generate a spawn point"))
        {
            GameObject go = new GameObject();
            go.transform.SetParent(myTarget.transform);
            go.transform.name = "SpawnPoint" + indexSpawnPoint;
            go.transform.localPosition = Vector3.zero;
            myTarget._spawnPoints.Add(go.transform.transform);
            indexSpawnPoint++;
            
        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("_spawnPoints"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_datas"));

        serializedObject.Update();
        serializedObject.ApplyModifiedProperties();
    }
}