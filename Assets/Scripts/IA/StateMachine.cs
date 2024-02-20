using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StateMachine : MonoBehaviour
{
    public State initialState;
    [SerializeField]
    State _currentState;
    private ParticleSystem part;

    private Color GamingGizmoCol;
    private Color MonoGizmoCol;

    // Start is called before the first frame update
    void Start()
    {

        _currentState = initialState;
        _currentState.StartState(gameObject);

        StartCoroutine(RainbowCol());
        StartCoroutine(MonoCol());
        part = FindAnyObjectByType<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        State nextState = _currentState.Run(gameObject);
        if (nextState)
        {
            ChangeState(nextState);
        }
    }

    void ChangeState(State nextState)
    {
        _currentState = nextState;
        _currentState.StartState(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (_currentState)
        {
            Gizmos.color = GamingGizmoCol;
            _currentState.DrawActionsGizmo(gameObject);

            Gizmos.color = MonoGizmoCol;
            _currentState.DrawStateGizmo(gameObject);
        }
    }

    IEnumerator RainbowCol()
    {
        float count = 0;
        while (count < 1)
        {
            Color rainbow = Color.HSVToRGB(count, 1, 1);

            GamingGizmoCol = rainbow;
            yield return new WaitForSeconds(1 / 360);

            count += 1 / 360f;
        }
        StartCoroutine(RainbowCol());
    }

    IEnumerator MonoCol()
    {
        List<Color> monoColors = new()
            { Color.white, Color.black };

        foreach (Color color in monoColors)
        {
            MonoGizmoCol = color;
            yield return new WaitForSeconds(1);
        }

        StartCoroutine(MonoCol());
    }

    public void PlayerDied()
    {
        GetComponent<Animator>().SetTrigger("playerDied");
    }

    //public void Damage(int damage)
    //{
    //     Collider collision = GetComponent<Collider>();
    //    // Verificar si la colisi�n es con el jugador
    //    if (collision.gameObject.GetComponent<SkoController>())
    //    {
    //        // Llamar al m�todo Damage del GameManager solo si la colisi�n es con el jugador
    //        GameManager.instance.Damage(damage);

    //    }
    //}

    public void PartStart()
    {
        part.Play();
    }

    public void PartStop()
    {
        part.Stop();
        part.Clear();

    }

    public State get_currentState { get { return _currentState; } }
}