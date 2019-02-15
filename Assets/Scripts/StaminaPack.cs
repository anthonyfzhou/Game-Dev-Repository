using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaPack : MonoBehaviour
{
    #region staminaPack_variables
    [SerializeField]
    [Tooltip("Assign the stamina value of the stamina pack")]
    private float staminaValue;
    #endregion

    #region functions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerController>().increaseStamina(staminaValue);
            Destroy(this.gameObject);
        }
    }
    #endregion
}
