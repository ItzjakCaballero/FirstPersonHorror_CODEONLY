using UnityEngine;

[CreateAssetMenu(fileName = "TaskInfo", menuName = "Scriptable Objects/TaskInfo")]
public class TaskInfoSO : ScriptableObject
{
    [field:SerializeField] public string id { get; private set; }
    public string taskName;
    public string taskObjective;
    public GameObject[] taskStepPrefabs;

#if UNITY_EDITOR
    private void OnValidate()
    {
        id = name;
        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif
}
