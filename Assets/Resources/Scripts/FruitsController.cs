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

    [SerializeField]
    private float moveSpeed = 5f;

    private AudioSource audioSource;
    [SerializeField] 
    private AudioClip collisionSFX; // SFX untuk tabrakan

    void Start() 
    {
        rb = GetComponent<Rigidbody>();

        // Pastikan ada AudioSource di objek
        if (GetComponent<AudioSource>() == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false; // Matikan Play On Awake
        }
        else
        {
            audioSource = GetComponent<AudioSource>();
        }

        audioSource.loop = false;
        audioSource.volume = 0.5f;
    }

    private void Update() 
    {
        if (!isShoot)
        {
            HandleKeyboardMovement();
        }
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

    private void HandleKeyboardMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        
        if (horizontalInput != 0)
        {
            transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Mainkan SFX ketika tabrakan terjadi
        if (collisionSFX != null)
        {
            PlaySFX(collisionSFX);
        }
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
