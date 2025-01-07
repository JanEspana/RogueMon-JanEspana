using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour, InputController.IWeaponActions
{
    bool canUse = true;
    float cooldown = 0.5f;
    public InputController ic;

    public Melee clobbopus;
    public Shoot clauncher;
    public Flamethrower magby;

    private int weaponIndex = 0;

    private Image clobImage;
    private Image claunchImage;
    private Image magImage;

    void Start()
    {
        clobImage = GameObject.Find("Clobbopus").GetComponent<Image>();
        claunchImage = GameObject.Find("Clauncher").GetComponent<Image>();
        magImage = GameObject.Find("Magby").GetComponent<Image>();

        clobbopus = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Melee>();
        clauncher = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Shoot>();
        magby = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Flamethrower>();

        SelectImage();
    }
    void Awake()
    {
        ic = new InputController();
        ic.Weapon.SetCallbacks(this);
    }
    void OnEnable()
    {
        ic.Weapon.Enable();
        ic.Weapon.UseWeapon.canceled += StopShooting;

    }
    void OnDisable()
    {
        ic.Weapon.Disable();
    }
    void Update()
    {
    }
    public void OnClobbopus(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            weaponIndex = 0;
            SelectImage();
        }
    }
    public void OnClauncher(InputAction.CallbackContext context)
    {
        if (context.performed && clauncher.isObtained)
        {
            weaponIndex = 1;
            SelectImage();
        }
        
    }
    public void OnMagby(InputAction.CallbackContext context)
    {
        if (context.performed && magby.isObtained)
        {
            weaponIndex = 2;
            SelectImage();
        }
    }

    public void OnUseWeapon(InputAction.CallbackContext context)
    {
        if (context.performed && canUse)
        {
            switch (weaponIndex)
            {
                case 0:
                    clobbopus.RockSmash();
                    break;
                case 1:
                    clauncher.WaterGun();
                    break;
                case 2:
                    magby.UseFlamethrower();
                    break;
            }
            StartCoroutine(Cooldown());
        }
    }
    void SelectImage()
    {
        switch (weaponIndex)
        {
            case 0:
                clobImage.color = Color.white;
                claunchImage.color = Color.grey;
                magImage.color = Color.grey;
                break;
            case 1:
                clobImage.color = Color.grey;
                claunchImage.color = Color.white;
                magImage.color = Color.grey;
                break;
            case 2:
                clobImage.color = Color.grey;
                claunchImage.color = Color.grey;
                magImage.color = Color.white;
                break;
            case 3:
                clobImage.color = Color.grey;
                claunchImage.color = Color.grey;
                magImage.color = Color.grey;
                break;
        }
    }
    IEnumerator Cooldown()
    {
        canUse = false;
        yield return new WaitForSeconds(cooldown);
        canUse = true;
    }
    void StopShooting(InputAction.CallbackContext context)
    {
        if (weaponIndex == 2 && magby.activeParticleSystem != null)
        {
            Destroy(magby.activeParticleSystem);
        }
    }
}
