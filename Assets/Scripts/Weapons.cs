using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    public void ShootSubmachineGun()
    {
       if (bullet != null)
           Instantiate(bullet, transform.position, Quaternion.identity);
        
    }
}
