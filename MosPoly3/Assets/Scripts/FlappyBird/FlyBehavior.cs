using UnityEngine;

public class FlyBehavior : MonoBehaviour
{
    [SerializeField] private float _velocity = 1.5f;
    [SerializeField] private float _rotationSpeed = 10f;
    private Rigidbody2D _rb;
    private Quaternion _initialRotation;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.velocity = Vector2.up * _velocity;
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = _initialRotation * Quaternion.Euler(0, 0, _rb.velocity.y * _rotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FindObjectOfType<GameManager>().EndGame();
    }

    public void ResetPosition(Vector3 startPos, Quaternion startRot)
    {
        transform.position = startPos;
        transform.rotation = startRot;
    }
}
