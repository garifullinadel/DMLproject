using UnityEngine;

namespace FPSShooterRuntime
{
    public  class WeaponControl : MonoBehaviour
    {
        [SerializeField] private int maxWeapons = 3;
        [SerializeField] private Camera camera;
        [SerializeField] private float weaponTakingRange=10.0f;
        
        [Header("Audio Settings")]
        // audio components.
        [SerializeField] private AudioClip changeSound;
        [SerializeField] private AudioClip takeOutSound;
        [SerializeField] private AudioSource audioSource;
        
        [Header("Canvas Settings")]
        // Canvas components.
        [SerializeField]
        private PlayerCanvas _playerCanvas;
        
        
        private GameObject[] _weapons;
        
        

        private GameObject weapon;
        

        private int currentWeapon = 0;

        private int _WeaponCount = 0;

        // Start is called before the first frame update
        void Start()
        {
            _weapons = new GameObject[maxWeapons];
        }

        // Update is called once per frame
        void Update()
        {
            WeaponSwitch();
            WeaponSearch();
        }

        private void WeaponSwitch()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SelectWeapon(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SelectWeapon(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SelectWeapon(2);
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (currentWeapon >= _WeaponCount - 1)
                {
                    SelectWeapon(0);
                }
                else SelectWeapon(currentWeapon + 1);
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (currentWeapon <= 0)
                {
                    SelectWeapon(_WeaponCount - 1);
                }
                else SelectWeapon(currentWeapon - 1);
            }
        }


        private void SelectWeapon(int index)
        {
            if (index >= _WeaponCount) return;
            if (currentWeapon != index || !_weapons[index].active)
            {
                _weapons[currentWeapon].SetActive(false);
                _weapons[index].SetActive(true);
                currentWeapon = index;
                audioSource.PlayOneShot(changeSound);
            }
            else
            {
                _weapons[index].SetActive(false);
                audioSource.PlayOneShot(takeOutSound);
                _playerCanvas.AmmoTextChange("-","-");
            }
        }

        private void WeaponAdd(GameObject w)
        {
            if (_WeaponCount >= maxWeapons) return;
            _weapons[_WeaponCount] = w;
            _WeaponCount++;
            SelectWeapon(_WeaponCount - 1);
        }


        private void WeaponTake(GameObject gameObject)
        {
            weapon = gameObject.GetComponent<WeaponProp>().playerWeapon;
            
            if (!weapon.GetComponent<WeaponIsTaked>().IsTaked)
            {
                WeaponAdd(weapon);
                weapon.GetComponent<WeaponIsTaked>().IsTaked = true;
            }
            weapon.GetComponent<WeaponShooting>().totalAmmo += 30;
            Destroy(gameObject);
        }

        private void WeaponSearch()
        {
            if (Input.GetKey(KeyCode.E))
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out RaycastHit hit, weaponTakingRange))
                {
                    GameObject objectHit = hit.transform.gameObject;
                    Debug.Log(objectHit.name);
                    if (objectHit.CompareTag("Weapon"))
                    {
                        WeaponTake(objectHit);
                    }
                }
            }
        }
    }
}
