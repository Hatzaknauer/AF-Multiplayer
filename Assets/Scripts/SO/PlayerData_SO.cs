using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewPlayerData", menuName = "PlayerData")]
public class PlayerData_SO : ScriptableObject, ISerializationCallbackReceiver
{
    int scoreInit = 0;
    public int score;

    public void OnAfterDeserialize()
    {
        score = scoreInit;
    }

    public void OnBeforeSerialize()
    {

    }
}
