using UnityEngine;
using UnityEngine.Events;

public class Detector : MonoBehaviour
{
    [SerializeField] private UnityEvent _onInside;
    [SerializeField] private UnityEvent _onOutside;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<RobberMover>(out RobberMover robber))
            _onInside?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<RobberMover>(out RobberMover robber))
            _onOutside?.Invoke();
    }
}
