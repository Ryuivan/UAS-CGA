using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class FruitsController : MonoBehaviour
{
    private Vector3 mousePressDownPosition;
    private Vector3 mouseReleasePosition;
    private Rigidbody rb;
    private bool isShoot;
    
    [SerializeField]
    private float forceMultiplier = 3;

    void Start() 
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown() 
    {
        mousePressDownPosition = Input.mousePosition;
    }

    private void OnMouseDrag() 
    {
        Vector3 forceInit = (Input.mousePosition - mousePressDownPosition);
        Vector3 forceV = (new Vector3(forceInit.x, forceInit.y, forceInit.y) * forceMultiplier);

        if (!isShoot)
            Trajectory.instance.UpdateTrajectory(forceV, rb, transform.position);
    }

    private void OnMouseUp() 
    {
        Trajectory.instance.HideTrajectory();
        mouseReleasePosition = Input.mousePosition;
        Shoot(mouseReleasePosition - mousePressDownPosition);
    }

    void Shoot(Vector3 Force) 
    {
        if (isShoot)
            return;

        rb.AddForce(new Vector3(Force.x, Force.y, Force.y) * forceMultiplier);
        isShoot = true;
        
        Spawner.instance.StartSpawning();
    }

    public bool IsShot()
    {
        return isShoot;
    }
}