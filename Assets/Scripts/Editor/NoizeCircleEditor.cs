using UnityEngine;
using UnityEditor;


    [CustomEditor(typeof(AiAgentNest))]
    public class ExampleEditor : Editor
    {
    void OnSceneGUI()
    {
        AiAgentNest structure = (AiAgentNest)target;

        Handles.color = Color.red;
        Handles.DrawWireDisc(structure.transform.position, new Vector3(0, 0, 1), structure.nestAgroRange);
    }
}
