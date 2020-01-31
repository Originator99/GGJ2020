using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathstar : MonoBehaviour
{
    [SerializeField]
    float health;
    [SerializeField]
    float charge;

    public delegate void AttackTarget();
    public static event AttackTarget OnAttackTarget;

    private void Start()
    {
        StartCoroutine(StartCharging());
    }

    IEnumerator StartCharging()
    {
        while (charge<=100)
        { 
            yield return new WaitForSeconds(1f);
            FindObjectOfType<UIManager>().chargeImage.fillAmount = charge/100;
            charge += 0.5f;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && charge>=100)
        {
            //Fire
            charge = 0;
            StartCoroutine(StartCharging());
            OnAttackTarget();
        }
    }
}
