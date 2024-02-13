using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR_WIN
[CustomEditor(typeof(MeshRotator), true, isFallback = true)]
public class MeshRotatorInspector : Editor
{
    private string exportTo = "Assets/rotatedMesh";
    public override void OnInspectorGUI()
    {
        if (target.GetType() == typeof(MeshRotator))
        {
            MeshRotator mr = (MeshRotator)target;
            if (GUILayout.Button("Apply Rotation To Vertices")) mr.ApplyRotationToVertices();
            GUILayout.BeginHorizontal();
            exportTo = GUILayout.TextField(exportTo);
            if (GUILayout.Button("Export Mesh"))
            {
                string path = exportTo + ".asset";
                Mesh mesh = AssetDatabase.LoadAssetAtPath<Mesh>(path);
                if (mesh != null)
                {
                    Debug.Log("file already exists");
                    return;
                }
                mesh = Instantiate<Mesh>(mr.meshFilter.sharedMesh);
                AssetDatabase.CreateAsset(mesh, exportTo + ".asset");
                mr.meshFilter.sharedMesh = mesh;
                EditorGUIUtility.PingObject(mr.meshFilter.sharedMesh);
                EditorUtility.SetDirty(mr.meshFilter);
            }
            GUILayout.EndHorizontal();
        }
        base.OnInspectorGUI();
    }
}
#endif