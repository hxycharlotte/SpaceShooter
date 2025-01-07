using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private AudioClip _explosionSoundClip;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3.0f);
        AudioSource.PlayClipAtPoint(_explosionSoundClip, Camera.main.transform.position, 1f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
