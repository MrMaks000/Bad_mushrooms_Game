using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float arrowSpeed = 10f;
    private Enemy target;
    private int damage;

    public void SetTarget(Enemy newTarget, int _damage)
    {
        target = newTarget;
        damage = _damage;
        RotateArrow(); // Вызываем функцию поворота стрелы при установке новой цели
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = target.transform.position - transform.position;
        float distanceThisFrame = arrowSpeed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        Vector3 newPosition = Vector3.Lerp(transform.position, target.transform.position, distanceThisFrame / direction.magnitude);
        transform.position = newPosition;

        RotateArrow(); // Вызываем функцию поворота стрелы каждый кадр для коррекции направления
    }

    private void RotateArrow()
    {
        Vector3 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void HitTarget()
    {
        if (target != null)
        {
            target.GetDamage(damage);
        }

        // Здесь можно добавить логику повреждения врага и дополнительные действия при попадании
        Destroy(gameObject);
    }
}