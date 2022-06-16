using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerMovement : MonoBehaviour
{
    public PlayerStates PlayerState;
    public float ExplosionForce, PushForce;
    public CinemachineFreeLook Cam;
    public Transform PerspectiveCam;
    public UIManager UIM;
    public Rigidbody rigidbody;
    public GameObject PlayerExplosion;
    public Vector3 StartPoint;

    public float FinishTime;

    public void OnEnable()
    {
        FinishTime = 0;
        SwitchPlayerState(PlayerStates.Gameplay);
    }
    public void SwitchPlayerState(PlayerStates NewPlayerState)
    {
        PlayerState = NewPlayerState;
        if(PlayerState==PlayerStates.Gameplay)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            Cam.m_XAxis.m_MaxSpeed = 600;
            Cam.m_YAxis.m_MaxSpeed = 20;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            Cam.m_XAxis.m_MaxSpeed = 0;
            Cam.m_YAxis.m_MaxSpeed = 0;
        }
    }
    public void Update()
    {
        switch(PlayerState)
        {
            case PlayerStates.Gameplay:
                Gameplay();
                break;
        }
    }

    public void Gameplay()
    {
        FinishTime += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (rigidbody.constraints != RigidbodyConstraints.FreezeAll)
            {
                Utils.PlaySFX("Explosion");
                Vector3 RandomDir = new Vector3(Random.Range(-1, 1), 1, Random.Range(-1, 1)).normalized;
                rigidbody.AddForce(RandomDir * ExplosionForce);
                Instantiate(PlayerExplosion, transform.position, Quaternion.identity);
            }
        }
        if(Input.GetMouseButtonDown(0))
        {
            Utils.PlaySFX("Zoom");
            rigidbody.AddForce(PerspectiveCam.transform.forward * PushForce);
        }
        if (Input.GetKeyDown(KeyCode.E) || transform.position.y < -100)
        {
            if (rigidbody.constraints != RigidbodyConstraints.FreezeAll)
            {
                transform.position = StartPoint;
                rigidbody.velocity = Vector3.zero;
                Utils.PlaySFX("SelfDestruct");
            }
        }
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    if (rigidbody.constraints == RigidbodyConstraints.FreezeAll)
        //    {
        //        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        //    }
        //    else
        //    {
        //        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //    }
        //}
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            UIM.Pause();
            SwitchPlayerState(PlayerStates.NonGameplay);
        }
    }
}

public enum PlayerStates
{
    Gameplay,
    NonGameplay
}