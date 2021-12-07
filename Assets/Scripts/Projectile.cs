using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector2 startingPosition;
    public float maxTravelDistance;
    public int obstacleLayerIndex;

    private void Start()
    {
        startingPosition = transform.position;
        obstacleLayerIndex = LayerMask.NameToLayer("Obstacle");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int colliderLayerIndex = collision.collider.gameObject.layer;
        Debug.Log("Index collider: " + colliderLayerIndex);
        if (colliderLayerIndex == obstacleLayerIndex)
        {
            Destroy(gameObject);
        }
    }

    private void GroundCheck()
    {
        float traveledDistance = Vector2.Distance(startingPosition, transform.position);
        Debug.Log(traveledDistance);
        if(traveledDistance >= maxTravelDistance)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        GroundCheck();
    }
}
