using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiState: MonoBehaviour
{

    public static UiState instance = null;
    private State _state = UiState.State.Menu;

    private void Awake()
    {
         if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        instance.SetState(UiState.State.Menu);
    }

    public void SetState(State state)
    {
        this._state = state;
    }

    public State GetCurrentState()
    {
        return this._state;
    }

    public enum State
    {
        Menu,
        Arena,
        Options,
        Customize,
        Upgrades,
        ShopCustomize,
        ShopUpgrades
    }
    
}
