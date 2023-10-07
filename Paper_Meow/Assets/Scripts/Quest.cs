using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest/Base Quest")]
public class Quest : ScriptableObject
{
    public string questName;
    [TextArea(3, 5)]
    public string questDescription;
    public bool isCompleted;
}