using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;
using Application = UnityEngine.Application;

/// <summary>
/// Keeps a GameObject on screen.
/// Note that this ONLY works for an orthographic Main Camera at [ 0, 0, 0 ].
/// </summary>
public class BoundsCheck : MonoBehaviour
{
    public enum Direction {up, down, left, right, not}
    
    [Header("Set in Inspector")]
    public float    radius = 1f;

    public bool keepOnScreen = true;

    [Header("Set Dynamically")]
    public bool     isOnScreen = true; 
    public float    camWidth;
    public float    camHeight;

    [HideInInspector] 
    public Direction offDir;
   
    
    void Awake() {
        camHeight = Camera.main.orthographicSize;                            
        camWidth = camHeight * Camera.main.aspect;                           
    }

    void LateUpdate () {                                                     
        Vector3 pos = transform.position;
        isOnScreen = true;
        offDir = Direction.not;

        if (pos.x > camWidth - radius) {
            pos.x = camWidth - radius;
            offDir = Direction.right;
        }

        if (pos.x < -camWidth + radius) {
            pos.x = -camWidth + radius;
            offDir = Direction.left;
        }

        if (pos.y > camHeight - radius) {
            pos.y = camHeight - radius;
            offDir = Direction.up;
        }
        if (pos.y < -camHeight + radius) {
            pos.y = -camHeight + radius;
            offDir = Direction.down;
        }

        isOnScreen = (offDir == Direction.not);
        if ( keepOnScreen && !isOnScreen ) { 
            transform.position = pos;
            isOnScreen = true;
            offDir = Direction.not;
        }
    }

    // Draw the bounds in the Scene pane using OnDrawGizmos()
    void OnDrawGizmos () {                                                    
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth*2, camHeight*2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }
}
