using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public struct ActionParameter
{
    public Action action;
    public bool actionValue;
}

[System.Serializable]
public struct StateParameters
{
    public ActionParameter[] actionParameters;
    public State nextState;
    [Tooltip("True = Or (si se cumple una solo cambia a la siguiente), False = And")]
    public bool or;
}

public abstract class State : ScriptableObject
{
    public StateParameters[] parameters;
    public NavMeshAgent navMeshAgent;
    protected GameObject target, cannon;
    public Animator animator;
    public virtual State Run(GameObject owner)
    {
        foreach (StateParameters par in parameters)
        {
            bool and = true;
            for (int i = 0; i < par.actionParameters.Length; i++)
            {
                bool currentAction = par.actionParameters[i].action.Check(owner) == par.actionParameters[i].actionValue;

                and &= currentAction;
                if (par.or && currentAction)
                {
                    Debug.Log($"Changing state to {par.nextState.name} due to OR condition.");
                    return par.nextState;
                }
            }

            if (and)
            {
                Debug.Log($"Changing state to {par.nextState.name} due to AND condition.");
                return par.nextState;
            }
        }

        return null;
    }


    public virtual void StartState(GameObject owner)
    {
        navMeshAgent = owner.GetComponent<NavMeshAgent>();
        target = FindObjectOfType<SkoController>().gameObject;
        animator = owner.GetComponent<Animator>();


        //empieza los starts de sus acciones
        foreach (StateParameters par in parameters)
        {
            foreach (ActionParameter act in par.actionParameters)
            {
                act.action.set_target = target;
                act.action.StartAction();
            }
        }
    }

    public virtual void DrawStateGizmo(GameObject owner)
    {

    }

    public void DrawActionsGizmo(GameObject owner)
    {
        foreach (StateParameters par in parameters)
        {
            for (int i = 0; i < par.actionParameters.Length; i++)
            {
                par.actionParameters[i].action.DrawGizmo(owner);
            }
        }
    }

    protected void ChangeTextureQuestionPlane(GameObject owner, Texture2D newTexture)
    {
        GameObject plane = owner.transform.Find("questionIcon").gameObject;

        //cambia el albedo y el emission
        plane.GetComponent<Renderer>().material.SetTexture("_MainTex", newTexture);
        plane.GetComponent<Renderer>().material.SetTexture("_EmissionMap", newTexture);

    }
}
