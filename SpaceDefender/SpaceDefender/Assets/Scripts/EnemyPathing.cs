using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> pathPoints;

    int pointIndex = 0;

    void Start()
    {
        pathPoints = waveConfig.GetWayPoints();
        transform.position = pathPoints[pointIndex].transform.position;
    }


    void Update()
    {
        
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        if (pointIndex <= pathPoints.Count - 1)
        {
            var targetPosition = pathPoints[pointIndex].transform.position;
            var movementPerFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementPerFrame);

            if (transform.position == targetPosition)
            {
                pointIndex++;
                
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
