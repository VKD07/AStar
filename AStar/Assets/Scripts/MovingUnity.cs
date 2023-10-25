using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MovingUnity : MonoBehaviour
{
    [SerializeField] AStar astar;
    [SerializeField] Transform start, end;
    [SerializeField] List<Vector3> path;
    int currentPathIndex;
    void Start()
    {
        if (astar.FindPath(transform.position, end.position))
        {
            path.Clear();
            foreach (Node node in astar.path)
            {
                path.Add(node.WorldPosition);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (path.Count > 0 && currentPathIndex < path.Count)
        {
            Vector3 targetPosition = path[currentPathIndex];
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * Time.deltaTime * 3;

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentPathIndex++;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (astar.FindPath(transform.position, end.position))
            {
                currentPathIndex = 0;
                path.Clear();
                foreach (Node node in astar.path)
                {
                    path.Add(node.WorldPosition);
                }
            }
        }
    }
}