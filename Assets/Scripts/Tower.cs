using UnityEngine;
using UnityEngine.Rendering;

public class Tower : MonoBehaviour
{

    public float range = 5f;

    public float damage = 1f;
    public float fireRate = 2f;

    public Transform currentTarget;

    public LineRenderer lineRenderer;
    public Transform firePoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SelectTarget", 0f, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SelectTarget()
    {
        float shortestDistance = Mathf.Infinity;
        Transform nearestTarget = null;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

        foreach (var hitCollider in hitColliders)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, hitCollider.transform.position);
            if (distanceToEnemy <= range && distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestTarget = hitCollider.transform;
            }
        }

        if (nearestTarget != null) 
        {
            currentTarget = nearestTarget;
            Shoot();
            Debug.Log("Nearest target is " + nearestTarget.name + " at " + shortestDistance);
        }
    }

    void Shoot()
    {
        if (currentTarget == null)
        {
            return;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, currentTarget.position);

        EnemyMovement enemy = currentTarget.GetComponent<EnemyMovement>();
        if (enemy != null)
        {
            enemy.Hit(damage);
            Debug.Log("Laser dealt" + damage + " to " + enemy.name);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
