using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private int powerupID;

    [SerializeField]
    private AudioClip _clip;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);

            Player player = other.transform.GetComponent<Player>();
            if (player != null) 
            {
                if (powerupID == 0)
                {
                    player.TripleShotActive();
                }

                else if (powerupID == 1)
                {
                    player.SpeedBoostActive();
                }

                else if(powerupID == 2) 
                {
                    player.ShieldsActive();
                }

                else
                {
                    Debug.Log("Default Case");
                }

                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldsActive();
                        break;
                    default:
                        Debug.Log("Default Case");
                        break;
                }
            }

            Destroy(this.gameObject);
        }
    }


}
