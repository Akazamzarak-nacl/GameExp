using UnityEngine;

public class ObstacleController : MonoBehaviour {
    public Vector3 Direction;
    public float Distance;
    public float Speed;
    public float IdleTime;

    private enum State {
        Idle,
        Moving
    }

    private State _currentState = State.Idle;
    private float _timer;

    private void Start() {
        _timer = IdleTime + 1;
        Direction.Normalize();
        Direction = -Direction;
    }
    private Vector3 _startPosition;
    private void Update() {
        if (_currentState == State.Idle) {
            if (_timer < IdleTime) {
                _timer += Time.deltaTime;
            } else {
                _currentState = State.Moving;
                _timer = 0;
                Direction = -Direction;
                _startPosition = transform.position;
            }
        }

        if (_currentState == State.Moving) {
            if ((transform.position - _startPosition).magnitude < Distance) {
                transform.position += Direction * Speed * Time.deltaTime;
            } else {
                _currentState = State.Idle;
                _timer = 0;
            }
        }
    }
}
