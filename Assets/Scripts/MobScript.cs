using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public sealed class MobScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _agressivnessDistance;
    private Transform _cached;

    private void Start()
    {
        _cached = transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

	private void OnValidate()
	{
        _speed = Mathf.Clamp(_speed, 0, float.MaxValue);
	}

	private void FixedUpdate()
    {
        float distToPlayer = (_player.position - _cached.position).sqrMagnitude;
        if (distToPlayer < _agressivnessDistance * _agressivnessDistance)
        {
            StartHunting();
        }
        else
        {
            StopHunting();
        }

        void StartHunting()
        {
            if (_cached.position.x < _player.position.x)
            {
                _rigidbody2D.velocity = new Vector2(_speed, 0);
            }
            else if (_cached.position.x > _player.position.x)
            {
                _rigidbody2D.velocity = new Vector2(-_speed, 0);
            }
        }

        void StopHunting()
        {
            _rigidbody2D.velocity = new Vector2(0, 0);
        }
    }

}
