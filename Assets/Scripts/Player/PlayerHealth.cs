using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [Tooltip("The max health the player can have")]
    public int maxHealth = 100;

    [Tooltip("The current health the player has")]
    public int currentHealth;

    [Tooltip("If you're using segmented health, this is gameObject that holds your health icons as its children")]
    public GameObject HealthIcons;

    [Tooltip("This is the Bar of health that you use if you're doing non-segmented health")]
    public GameObject HealthBar;

    private List<GameObject> Hearts = new List<GameObject>();

    private List<GameObject> TempHearts = new List<GameObject>();

    [Tooltip("If you actually want to use a healthbar or not")]
    public bool useHealthBar = false;

    private PlayerMovement playerMovement;


    [HideInInspector] public int index = 0;

    void Start()
    {
       SetUpHealth();
       playerMovement = GetComponent<PlayerMovement>();
    }

    public void SetUpHealth()
    {
        if (!useHealthBar)
        {
            Hearts.Clear();
            TempHearts.Clear();
            foreach (Transform child in HealthIcons.transform)
            {
                child.gameObject.GetComponent<Image>().color = Color.white;
                Hearts.Add(child.gameObject);
                TempHearts.Add(child.gameObject);
            }
            for (int i = 0; i < maxHealth - currentHealth; i++)
            {
                TempHearts[maxHealth - i - 1].GetComponent<Image>().color = Color.black;
                TempHearts.RemoveAt(TempHearts.Count - 1);
            }
        }
        else
        {
            if (HealthBar)
            {
                HealthBar.GetComponent<Image>().type = Image.Type.Filled;
                HealthBar.GetComponent<Image>().fillMethod = (int)Image.FillMethod.Horizontal;
                HealthBar.GetComponent<Image>().fillOrigin = (int)Image.OriginHorizontal.Left;
                currentHealth = maxHealth;
                UpdateHealthBar();
            }
        }
    }

    public void DecreaseHealth(int value)
    {
        if (!useHealthBar)
        {
            SegmentedHealthDecrease(value);
            return;
        }
        currentHealth -= value;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        UpdateHealthBar();
    }

    public void IncreaseHealth(int value)
    {
        if (!useHealthBar)
        {
            SegmentedHealthIncrease(value);
            return;
        }
        currentHealth += value;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthBar();
    }

    private void SegmentedHealthDecrease(int value)//Helper function
    {
        if (value > TempHearts.Count)
        {
            value = TempHearts.Count;
        }
        for (int i = 0; i < value; i++)
        {
            TempHearts[currentHealth - 1].GetComponent<Image>().color = Color.black;
            TempHearts.RemoveAt(TempHearts.Count - 1);
            currentHealth--;
        }

        if (TempHearts.Count == 0)
        {
            currentHealth = 0;
        }
    }

    private void SegmentedHealthIncrease(int value)//Helper function
    {
        if (value + TempHearts.Count > Hearts.Count)
        {
            value = Hearts.Count - TempHearts.Count;
        }

        for (int i = 0; i < value; i++)
        {
            var temp = Hearts[currentHealth];
            temp.GetComponent<Image>().color = Color.white;
            TempHearts.Add(temp);
            currentHealth++;
        }
    }

    public void ResetHealth()//Resets health back to normal
    {
        if (!useHealthBar)
        {
            for (int i = 0; i < Hearts.Count; i++)
            {
                Hearts[i].GetComponent<Image>().color = Color.white;
            }

            TempHearts.Clear();

            foreach (var VARIABLE in Hearts)
            {
                TempHearts.Add(VARIABLE);
            }
            currentHealth = TempHearts.Count;
        }
        else
        {
            currentHealth = maxHealth;
            UpdateHealthBar();
        }
    }

    void UpdateHealthBar()//Updates the health bar according to the new health amounts
    {
        if (useHealthBar)
        {
            float fillAmount = (float)currentHealth / maxHealth;
            if (fillAmount > 1)
            {
                fillAmount = 1.0f;
            }

            HealthBar.GetComponent<Image>().fillAmount = fillAmount;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D thisCollision = GetComponent<Collider2D>();
        if (collision.otherCollider == thisCollision)
        {
            if (collision.gameObject.TryGetComponent(out Weapon weapon))
            {
                if (weapon.alignmnent == Weapon.Alignment.Enemy ||
                    weapon.alignmnent == Weapon.Alignment.Environment)
                {
                    DecreaseHealth(weapon.damageValue);
                    if (currentHealth == 0)
                    {
                        playerMovement.TimeToDie();
                    }
                }
            }
            if (collision.gameObject.TryGetComponent(out HealingItem healingValue))
            {
                IncreaseHealth(healingValue.HealAmount);
                if (healingValue.DestroyOnContact)
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        {
            if (collision.gameObject.TryGetComponent(out Weapon weapon))
            {
                if (weapon.alignmnent == Weapon.Alignment.Enemy ||
                    weapon.alignmnent == Weapon.Alignment.Environment)
                {
                    DecreaseHealth(weapon.damageValue);
                    if (currentHealth == 0)
                    {
                        playerMovement.TimeToDie();
                    }
                }
            }
            if (collision.gameObject.TryGetComponent(out HealingItem healingValue))
            {
                IncreaseHealth(healingValue.HealAmount);
                if (healingValue.DestroyOnContact)
                {
                    Destroy(collision.gameObject);
                }
            }
        }

    }
}
