using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _lineRenderer;

    [SerializeField]
    [Range(3, 30)]
    private int _lineSegmentCount = 20;
    
    [SerializeField]
    [Range(10, 100)]
    private int _showPercentage = 50;

    [SerializeField]
    private int _linePointCount;

    private List<Vector3> _linePoints = new List<Vector3>();

    #region Singleton

    public static Trajectory instance;

    private void Awake() 
    {
        instance = this;
    }
    
    #endregion

    private void Start() 
    {
        _linePointCount = (int)(_lineSegmentCount * (_showPercentage / 100f));
    }


    public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidbody, Vector3 startingPoint) 
    {
        Vector3 velocity = (forceVector / rigidbody.mass) * Time.fixedDeltaTime;

        float FlightDuration = (2 * velocity.y) / Physics.gravity.y;
        
        float stepTime = FlightDuration / _lineSegmentCount;

        _linePoints.Clear();
        _linePoints.Add(startingPoint);

        for (int i = 1; i < _linePointCount; i++) {
            float stepTimePassed = stepTime * i;

            Vector3 MovementVector = new Vector3(
                velocity.x * stepTimePassed,
                velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
                velocity.z * stepTimePassed
            );

            Vector3 NewPointOnLine = -MovementVector + startingPoint;

            _linePoints.Add(-MovementVector + startingPoint);
        }

        _lineRenderer.positionCount = _linePoints.Count;
        _lineRenderer.SetPositions(_linePoints.ToArray());
    }

    public void HideTrajectory() 
    {
        _lineRenderer.positionCount = 0;
    }
}