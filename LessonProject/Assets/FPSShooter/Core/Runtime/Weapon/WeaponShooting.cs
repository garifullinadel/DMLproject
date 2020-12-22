using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShooting : MonoBehaviour
{
    [Header("Bullet Settings")]
    // Bullet components.
    [SerializeField] private Rigidbody bullet;
    [SerializeField] private float force;
    [SerializeField] private Transform spawnPoint;
    
    [Header("Particle Settings")]
    // Particle components.
    [SerializeField] private ParticleSystem[] particleSystem;
    // Light components.
    [SerializeField] private Light shootLight;
    [SerializeField] private float lightTime=0.1f;
        
    // Stored required properties.
    private float _reloadTimer = 0.0f;
    private float _shootTimer = 0.0f;
    private int _reloadAmmo;
    
    [Header("Speed Settings")]
    // Speed properties.
    [SerializeField] private float shootSpeed;
    [SerializeField] private float reloadSpeed;
    
    // Ammo properties.
    [Header("Ammo Settings")]
    [SerializeField] internal int totalAmmo;
    [SerializeField] private int maxAmmoInMagazine; 
    [SerializeField] private int ammoInMagazine; 
    

    // Sound components.
    [Header("Sound Settings")]
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private AudioClip reloadSound;
    [SerializeField] private AudioClip nullSound;
    [SerializeField] private AudioSource audioSource;

    [Header("Canvas Settings")]
    // Canvas components.
    [SerializeField]
    private PlayerCanvas _playerCanvas;
    void Update()
    {
        ShowAmmo();
        Zoom();
        Reload();
        ReloadTimerUpdate();
        ShootTimerUpdate();
        Shoot();
    }

    private void Zoom()
    {
        if (Input.GetMouseButton(1))
        {

        }
    }

    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) & ammoInMagazine != maxAmmoInMagazine & totalAmmo > 0 && _reloadTimer<=0)
        {
            _reloadTimer += reloadSpeed;
            audioSource.PlayOneShot(reloadSound);
            _reloadAmmo = maxAmmoInMagazine - ammoInMagazine;
            totalAmmo -= _reloadAmmo;
            if (totalAmmo <= 0)
            {
                _reloadAmmo += totalAmmo;
                totalAmmo = 0;
            }
            ammoInMagazine += _reloadAmmo;
            _reloadAmmo = 0;
            audioSource.PlayOneShot(reloadSound);
        }
    }
    private void ReloadTimerUpdate()
    {
        if (_reloadTimer > 0)
        {
            _reloadTimer -= Time.deltaTime;
        }
    }
    private void ShootTimerUpdate()
    {
        if (_shootTimer > 0)
        {
            _shootTimer -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        if (Input.GetMouseButton(0) & _reloadTimer <= 0 & _shootTimer <= 0 & ammoInMagazine != 0)
        {
            {
                Rigidbody BulletInstance;
                BulletInstance = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
                BulletInstance.AddForce(spawnPoint.forward * force);
                for (int i = 0; i < particleSystem.Length; i++)
                {
                    particleSystem[i].Play();
                }

                _shootTimer = shootSpeed;
                ammoInMagazine -= 1;
                audioSource.PlayOneShot(fireSound);
                ShootLight();
            }
        }
        else if (Input.GetMouseButtonDown(0) && ammoInMagazine == 0)
        {
            audioSource.PlayOneShot(nullSound);
        }
    }

    private void ShowAmmo()
    {
        _playerCanvas.AmmoTextChange(totalAmmo.ToString(),ammoInMagazine.ToString());
    }

    private void ShootLight()
    {
        shootLight.enabled = true;
        StartCoroutine (TurnOffAfter (lightTime));
        
    }
    private IEnumerator TurnOffAfter(float time) 
    {
        yield return new WaitForSeconds(time);
        shootLight.enabled = false;
    }
}