using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    #region gameObject_variables
    [SerializeField]
    [Tooltip("healthpack")]
    private GameObject healthPack;

    [SerializeField]
    [Tooltip("Stamina Pack")]
    private GameObject staminaPack;
    #endregion

    #region helper_functions
    IEnumerator DeleteChest()
    {
        yield return new WaitForSeconds(0.3f);
        float result = Random.Range(0.0f, 1.0f);
        if (result < 0.7f) {
            Instantiate(healthPack, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(staminaPack, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    public void Interact()
    {
        StartCoroutine(DeleteChest());
    }
    #endregion
}
