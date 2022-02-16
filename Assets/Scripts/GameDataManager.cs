using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public string playerNameIn;
    public static GameDataManager dataManager;
    // Start is called before the first frame update
    private void Awake()
    {
        if (dataManager != null)
        {
            Destroy(gameObject);
            return;
        }
        dataManager = this;
        DontDestroyOnLoad(gameObject);
    }
    public void PlayerNameInput(string p)
    {
         playerNameIn = p;
        Debug.Log(playerNameIn);
    }
}
