using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerID;

    [SerializeField]
    private AudioClip _clip;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y < -7) 
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collided with: " + other.name);

        if (other.tag == "Player")
        {
            // Acess the player
            Player player = other.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);

            if (player != null)
            {
                if (powerID == 0)
                {
                    // Enable triple shot
                    player.TripleShotPowerupOn();
                }
                else if (powerID == 1)
                {
                    // Enable speed boost 
                    player.SpeedBoostPowerupOn();
                }
                else if (powerID == 2)
                {
                    // Enable shields
                    player.ShieldPowerupOn();
                }
            }

            // Destroy our selves
            Destroy(this.gameObject);
        }
    }
}
