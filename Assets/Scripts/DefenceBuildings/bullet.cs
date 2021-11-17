using UnityEngine;

public class bullet : MonoBehaviour
{
    ObjectPool objectPool;
    Enemy enemy;
    float attackPower;
    private Transform target;

    public float speed = 70f;

    private void OnEnable()
    {
        target = null;
    }

    private void Awake()
    {
        objectPool = FindObjectOfType<ObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            objectPool.ReturnGameObject(gameObject);
            return;
        }
        Vector2 direction = target.position - transform.position;
        float distanceFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceFrame, Space.World);
    }

    public void CarryTarget(Transform _target, float _attackPower)
    {
        attackPower = _attackPower;
        target = _target;
        enemy = _target.GetComponent<Enemy>();
    }

    void HitTarget()
    {
        objectPool.ReturnGameObject(gameObject);
        enemy.Health -= attackPower;
    }
}
