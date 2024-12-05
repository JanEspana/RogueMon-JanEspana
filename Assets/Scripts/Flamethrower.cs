using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    public GameObject activeParticleSystem;
    public GameObject flames;
    public Transform spawnPoint;
    public bool isObtained;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UseFlamethrower()
    {
        activeParticleSystem = Instantiate(flames, spawnPoint.position, spawnPoint.rotation);
        activeParticleSystem.transform.parent = spawnPoint;
        activeParticleSystem.transform.position = spawnPoint.position;
    }
}
