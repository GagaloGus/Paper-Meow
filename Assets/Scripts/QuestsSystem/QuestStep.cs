using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    [SerializeField] bool isFinished = false;

    protected void FinishQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;

            //(aca estara todo para avanzar el quest)


            Destroy(gameObject);
        }
    }
}
