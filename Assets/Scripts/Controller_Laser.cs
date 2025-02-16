using UnityEngine;

public class Controller_Laser : Projectile
{
    public float maxGrowth = 5f, laserSpeed = 10f;
    public bool relase;
    private Vector3 initialScale;
    private Rigidbody rb;
    public GameObject parent;
    public float relaseCounter = 0.2f;
    private SphereCollider sphereCollider;

    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        initialScale = transform.localScale;
        maxGrowth = initialScale.x + maxGrowth;
    }

    void FixedUpdate()
    {
        relaseCounter -= Time.deltaTime;
        if (relaseCounter <= 0 && !relase)
        {
            relase = true;
            sphereCollider.enabled = true;
            transform.SetParent(null);
        }

        if (!relase && parent != null)
        {
            transform.position = parent.transform.position;
            GrowLaser();
        }

        if (relase)
        {
            GetComponent<Rigidbody>().velocity = Vector3.right * laserSpeed;
        }
    }

    private void GrowLaser()
    {
        if (transform.localScale.x < maxGrowth)
            transform.localScale += Vector3.one * 0.1f;
        else relase = true;
    }

    public void Initialize(GameObject parentObject)
    {
        // Configurar posición inicial y referencia al padre
        transform.position = parentObject.transform.position;
        transform.SetParent(parentObject.transform);
        relase = false;
        sphereCollider.enabled = false;
        relaseCounter = 0.2f;
        transform.localScale = initialScale;

    }
}
