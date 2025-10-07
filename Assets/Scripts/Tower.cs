using UnityEngine;

public class Tower : MonoBehaviour
{

    public float range = 5f;
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
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

        foreach (var hitCollider in hitColliders)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, hitCollider.transform.position);
            if (distanceToEnemy <= range && distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
            }
            Debug.Log(hitCollider.name + " was hit");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
