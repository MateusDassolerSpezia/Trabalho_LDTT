using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 4f);
    }
}
