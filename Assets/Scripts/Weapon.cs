using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleFlash;
    public GameObject[] hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = 0f;
    public Animator animator;

    private Recoil Recoil_Script;



    bool canShoot = true;
    
    private void OnEnable() 
    {
        canShoot = true;
    }
    void Start()
    {
        Recoil_Script = GameObject.Find("CameraRotation/CameraRecoil").GetComponent<Recoil>();
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canShoot )
        {
            ShootAnim();
            //CameraShaker.Invoke(); DOtween ile camera titremesi
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if(ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            Recoil_Script.RecoilFire();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;

    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();

            if (target == null) { return; }
            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
            var random = new System.Random();
            int start2 = random.Next(0, hitEffect.Length);
        GameObject impact = Instantiate(hitEffect[start2],hit.point,Quaternion.LookRotation(hit.normal));
        Destroy( impact , .5f );
    }

    void ShootAnim()
    {
        animator.SetTrigger("Shoot");
            
    }
}
