using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public UIManager UIM;

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 7)
        {
            Utils.PlaySFX("Finish");
            collision.gameObject.GetComponent<PlayerMovement>().SwitchPlayerState(PlayerStates.NonGameplay);
            UIM.GameOver(collision.gameObject.GetComponent<PlayerMovement>().FinishTime);
        }
    }
}
