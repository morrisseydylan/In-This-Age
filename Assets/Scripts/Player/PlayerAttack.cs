using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Player Weapons")]
    [Tooltip("This is the list of all the weapons that your player uses")]
    public List<Weapon> weaponList;
    [Tooltip("This is the current weapon that the player is using")]
    public Weapon weapon;
    private Vector2 lastKnownDirection;
    [Tooltip("The coolDown before you can attack again")]
    public float coolDown = 0.4f;

    private bool canAttack = true;
    public GameObject player;

    private void Start()
    {
        if (weapon == null && weaponList.Count > 0)
        {
            weapon = weaponList[0];
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))//Hit "1" key on the keyboard to activate this weapon
        {
            if (weaponList.Count > 0)
            {
                switchWeaponAtIndex(0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (weaponList.Count > 1)
            {
                switchWeaponAtIndex(1);
            }
        }
    }

    public void Attack()
    {
        //This is where the weapon is rotated in the right direction that you are facing
        if (weapon && canAttack)
        {
            if (weapon.weaponType == Weapon.WeaponType.Melee)
            {
                weapon.WeaponStart();
            }

            else
            {
                float forceX = -1 + 2 * (Input.mousePosition.x / Screen.width);
                float forceY = -1 + 2 * (Input.mousePosition.y / Screen.height);
                float angle = Mathf.Atan2(forceY, forceX);

                GameObject projectile = Instantiate(weapon.projectile, weapon.shootPosition.position, Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg));
                projectile.GetComponent<Projectile>().SetValues(weapon.duration, weapon.alignmnent, weapon.damageValue);
                projectile.transform.localScale = new Vector3(projectile.transform.localScale.x, projectile.transform.localScale.y, projectile.transform.localScale.z);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

                rb.AddForce(new Vector2(forceX, forceY) * weapon.force);
                


            }

            StartCoroutine(CoolDown());
        }
    }

    public void StopAttack()
    {
        if (weapon)
        {
            weapon.WeaponFinished();
        }
    }

    public void switchWeaponAtIndex(int index)
    {
        //Makes sure that if the index exists, then a switch will occur
        if (index < weaponList.Count && weaponList[index])
        {
            //Deactivate current weapon
            weapon.gameObject.SetActive(false);

            //Switch weapon to index then activate
            weapon = weaponList[index];
            weapon.gameObject.SetActive(true);
        }

    }

    private IEnumerator CoolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(coolDown);
        canAttack = true;
    }
}
