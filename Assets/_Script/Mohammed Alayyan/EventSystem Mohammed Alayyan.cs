using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public event  Action OnRoomEntered;
  

    public delegate float EnemyEventHandle( );
    public EnemyEventHandle OnEnemySpawn;


    public UnityEvent OnPlyerWin;







    private void Start()
    {
        OnRoomEntered = RegisterEnemy;

        OnRoomEntered += RegisterEnemy;
        OnRoomEntered += A;
        OnRoomEntered += B;

        OnRoomEntered?.Invoke();
    }

    private void OnDestroy()
    {

      //  Button b = default;
      
        OnRoomEntered -= RegisterEnemy;
        
      OnPlyerWin?.Invoke();
      
        OnEnemySpawn?.Invoke();
    }


    private void RegisterEnemy()
    { 
    
    }


    public void A()
    {
        
    }

    private void B()
    { 
    
    }

    public void ShowWinScreen()
    { 
    
    }


    IEnumerator DelayCall(Action callback)
    { 
        yield return new WaitForSeconds(5);



     callback?.Invoke();
    }
}
