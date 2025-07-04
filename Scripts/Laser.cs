using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _LaserSpeed = 10.0f;

    void Update()
    {
        LaserMovement();
        LaserCheckBounds();
    }

    private void LaserCheckBounds()
    {
        if (transform.position.y >= 6)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    private void LaserMovement()
    {
        transform.Translate(Vector3.up * _LaserSpeed * Time.deltaTime);
    }
}
