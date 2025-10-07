using System.Linq;
using TMPro;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    public float moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            GameObject group = GameObject.Find("Waypoints");
            if (group != null)
            {
                waypoints = group.GetComponentsInChildren<Transform>()
                                 .Where(t => t != group.transform)
                                 .ToArray();
            }
            else
            {
                Debug.LogError("WaypointGroup not found in scene.");
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        Vector3 direction = (targetPosition - transform.position).normalized;

        if (currentWaypointIndex < waypoints.Length)
        {
            Transform target = waypoints[currentWaypointIndex];
            transform.position = Vector3.MoveTowards(
                transform.position,
                target.position,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                currentWaypointIndex++;

            }
        }

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }
    }
}
