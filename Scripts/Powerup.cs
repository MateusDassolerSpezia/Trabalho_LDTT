using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _powerupSpeed = 3.0f;
    [SerializeField] private EPowerupType _powerupType;

    [SerializeField] private AudioClip _SFX_PowerupPickupClip;

    void Update()
    {
        PowerupMovement();
        CheckPowerupBounds();
    }

    private void PowerupMovement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _powerupSpeed);       
    }

    private void CheckPowerupBounds()
    {
        if (transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                switch (_powerupType)
                {
                    case EPowerupType.tripleShot:
                        player.TripleShotPowerupOn();
                        break;
                    case EPowerupType.speedBoost:
                        player.SpeedBoostPowerupOn();
                        break;
                    case EPowerupType.Shield:
                        player.ShieldPowerupOn();
                        break;
                }
            }

            AudioSource.PlayClipAtPoint(_SFX_PowerupPickupClip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
        }
    }
}
