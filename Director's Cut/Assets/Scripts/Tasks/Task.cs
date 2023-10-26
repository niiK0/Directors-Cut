using UnityEngine;

[CreateAssetMenu(fileName = "NewTaskData", menuName = "Task Data")]
public class TaskData : ScriptableObject
{
    public string taskName;
    public bool isComplete;
    public float taskRange;
    public float completePercentage;
    public Steps[] taskSteps;
}
