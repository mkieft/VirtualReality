using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Rigidbody projectile;
    public GameObject spawnPoint; 
    //public Transform paintGun; 
    public float shootForce = 50f; 


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1")) 
        {
            FindObjectOfType<AudioManager>().Play("Fire");
            Debug.Log("Pressed trigger...shooting");
            Rigidbody instantiatedProjectile = Instantiate(projectile, spawnPoint.transform.position, spawnPoint.transform.rotation);

           instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, shootForce)); 

            //instantiatedProjectile.AddForce(transform.forward * shootForce);
            //instantiatedProjectile.velocity = transform.TransformDirection(transform.forward * shootForce); 
            //instantiatedProjectile.AddForce(instantiatedProjectile.transform.forward * shootForce);

        }
    }
}
