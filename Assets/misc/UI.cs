using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI lives;
    public TextMeshProUGUI rooms;

    [SerializeField] private playerControler _playerControler;
    [SerializeField] private ennemyGen _ennemyGen;
    [SerializeField] private RoomGenerator _roomGenerator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lives.text = "Lives : " + _playerControler.life + "/" + _playerControler.Maxlife;
        rooms.text = "Rooms : " + _ennemyGen.VisitedRooms.Count + "/" + _roomGenerator._nbRooms;
    }
}
