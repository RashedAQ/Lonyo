using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData
{
    public int _deathCount;
    public string _sceneName;
    public bool _isNewGame;
    public int totalMoney;
    public Vector3 _playerPos;
    public Quaternion _playerRota;
    public Quaternion _cameraRota;
    public SerializableDictionary<string,bool>_moneyCollected;
    public bool _Test;
    public SerializableDictionary<string, bool> _Takeable;
    public SerializableDictionary<string, bool> _bojectDie;
    public SerializableDictionary<string, bool> _isCollected;
    public List<string> collectedApples = new List<string>();

    public GameData()
    {
      this._Test = true; 
        _isCollected = new SerializableDictionary<string,bool>();
        this._deathCount = 0;
        this._sceneName = "";
        this._isNewGame = true;
        this.totalMoney = 0;
        _playerPos = Vector3.zero; 
        _playerRota = Quaternion.identity;
        _cameraRota = Quaternion.identity;
        _moneyCollected = new SerializableDictionary<string,bool>();
        _Takeable = new SerializableDictionary<string,bool>();
        _bojectDie = new SerializableDictionary<string,bool>();
    }
}