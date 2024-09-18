using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRecoil : MonoBehaviour
{
    public float recoilIntensity = 1f; // Tepme şiddeti
    public float recoilSpeed = 2f; // Tepme hızı

    private Vector3 originalRotation; // Orijinal dönme

    void Start()
    {
        originalRotation = transform.localEulerAngles;
    }

    void Update()
    {
        // Silahın tepmesi için rastgele bir dönme miktarı oluştur
        float recoilAmount = Random.Range(-recoilIntensity, recoilIntensity);
        float recoilRotation = Random.Range(-recoilIntensity, recoilIntensity);

        // Silahın dönmesini güncelle
        Vector3 recoilVector = new Vector3(recoilAmount, recoilRotation, 0);
        transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, originalRotation + recoilVector, Time.deltaTime * recoilSpeed);
    }
}
