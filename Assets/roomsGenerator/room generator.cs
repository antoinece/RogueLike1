using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap _mapBackground;
    [SerializeField] private Tilemap _mapWalls;
    [SerializeField] private TileBase _baseBackground;
    [SerializeField] private TileBase _baseWalls;
    [SerializeField] public int _nbRooms;
    [SerializeField] private int _iterationDrunk = 1;
    [SerializeField] private static Vector2Int _size= new Vector2Int(25,13);
    public Vector2Int _centerRoom;
    private HashSet<Vector2Int> tilesBackground = new HashSet<Vector2Int>();
    private HashSet<Vector2Int> tilesWalls = new HashSet<Vector2Int>();
    [SerializeField] private Vector2 _perlinScale = new Vector2(1, 1);
    [Range(0, 1)] [SerializeField] private float idk;
    public List<Vector2Int> Rooms = new List<Vector2Int>();
    public List<Vector3Int> RoomsValue = new List<Vector3Int>();
    static Vector2Int _exteriorSize = new Vector2Int(_size.x + 2, _size.y + 2);
    private List<Vector3Int> _doors = new List<Vector3Int>();

    private int EValue = 0;
    // Vector2Int up = new Vector2Int(0, _exteriorSize.y);
    // Vector2Int right = new Vector2Int(_exteriorSize.x, 0);
    // Vector2Int down = new Vector2Int(0, -_exteriorSize.y);
    // Vector2Int left = new Vector2Int(-_exteriorSize.x, 0);

    public Vector2Int currentRoom= new Vector2Int(0, 0);
    
    private void PathRandom()
    {
        _centerRoom = new Vector2Int(0, 0);
        
        AddRoom(_centerRoom);
        Rooms.Add(_centerRoom);

        int i = 0;
        do
        {
            i++;
            Vector2Int randomSide = genTool.VonNeumannNeighbours[Random.Range(0, genTool.VonNeumannNeighbours.Length)];
            if (!Rooms.Contains(randomSide+_centerRoom))
            {
                AddRoom(_centerRoom+randomSide);
                Rooms.Add(_centerRoom+randomSide);
            }
            _centerRoom = _centerRoom+randomSide;
        } while (Rooms.Count<_nbRooms&&i<_nbRooms*10);
    }
    
    private void PathDrunk()
    {
        _centerRoom = new Vector2Int(0, 0);   
        
        AddRoom(_centerRoom);
        Rooms.Add(_centerRoom);
        
        do
        {
            Vector2Int randomSide = genTool.VonNeumannNeighbours[Random.Range(0, genTool.VonNeumannNeighbours.Length)];
            for (int n = 0; n < _iterationDrunk; n++) 
            { 
                if (!Rooms.Contains(randomSide+_centerRoom))
                {
                    AddRoom(_centerRoom+randomSide);
                    Rooms.Add(_centerRoom+randomSide);
                }
                _centerRoom = _centerRoom+randomSide;
            }
        } while (Rooms.Count < _nbRooms);
        
    }
    
    private void PathRule()
    {
        _centerRoom = new Vector2Int(0, 0);   
        
        AddRoom(_centerRoom);
        Rooms.Add(_centerRoom);
        int i = 0;
        do
        {
            i++;
            Vector2Int randomSide = genTool.VonNeumannNeighbours[Random.Range(0, genTool.VonNeumannNeighbours.Length)];
            if (!Rooms.Contains(randomSide+_centerRoom))
            {
                int r = Mathf.FloorToInt(Random.Range(0, 4));
                int _nbNeighbours = 0;
                foreach (Vector2Int neighbour in genTool.VonNeumannNeighbours)
                {
                    if (Rooms.Contains(_centerRoom+neighbour))
                    {
                        _nbNeighbours++;
                    }
                }

                if (_nbNeighbours==1||_nbNeighbours==0)
                {
                    AddRoom(_centerRoom+randomSide);
                    Rooms.Add(_centerRoom+randomSide);
                }
                else if (_nbNeighbours==2&&r>1)
                {
                    AddRoom(_centerRoom+randomSide);
                    Rooms.Add(_centerRoom+randomSide);
                }
                else if (_nbNeighbours==3&&r<1)
                {
                    AddRoom(_centerRoom+randomSide);
                    Rooms.Add(_centerRoom+randomSide);
                }
            }
            _centerRoom = _centerRoom+randomSide;
            
        } while (Rooms.Count < _nbRooms&&i<_nbRooms*10);
        
    }

    private void AddRoom(Vector2Int RoomOffset)
    {
        
        RoomsValue.Add(new Vector3Int(RoomOffset.x,RoomOffset.y,EValue));
        Vector2Int offset = new Vector2Int(Random.Range(-1000, 1000), Random.Range(-1000, 1000));
        for (int x = 0; x < _size.x + 4; x++)
        {
            for (int y = 0; y < _size.y + 4; y++)
            {
                if (x == 0 || x == _size.x + 3 || y == 0 || y == _size.y + 3 || x == 1 || x == _size.x + 2 || y == 1 ||
                    y == _size.y + 2)
                {
                    tilesWalls.Add(new Vector2Int((x - (_size.x + 4) / 2)+RoomOffset.x, (y - (_size.y + 4) / 2)+RoomOffset.y));
                }
                else if (x == 2 || x == _size.x + 1 || y == 2 || y == _size.y + 1)
                {
                    tilesBackground.Add(new Vector2Int((x - (_size.x+4) /2 )+RoomOffset.x, (y - (_size.y + 4) / 2)+RoomOffset.y));
                }
                else
                {
                    float noiseCoordX = x / (_size.x * 2f) + offset.x;
                    float noiseCoordY = y / (_size.y * 2f) + offset.y;

                    float rnd = Mathf.PerlinNoise(noiseCoordX * _perlinScale.x, noiseCoordY * _perlinScale.y);

                    if (rnd > idk)
                    {
                        //_map.SetTile(new Vector3Int(_center.x+x - _size.x/2 ,_center.y+y - _size.y/2),_base);
                        tilesWalls.Add(new Vector2Int((x - (_size.x + 4) / 2)+RoomOffset.x, (y - (_size.y + 4) / 2)+RoomOffset.y));
                    }
                    else
                    {
                        tilesBackground.Add(new Vector2Int((x - (_size.x + 4) / 2)+RoomOffset.x, (y - (_size.y + 4) / 2)+RoomOffset.y));
                    }
                }
            }
        }
        EValue++;
    }

    public void GenerateRa()
    {
        Rooms.Clear();
        tilesBackground.Clear();
        tilesWalls.Clear();
        PathRandom();
        genTool.DrawMap(_mapBackground,_baseBackground,tilesBackground);
        genTool.DrawMap(_mapWalls,_baseWalls,tilesWalls);
        DoorHole();
    }
    public void GenerateD()
    {
        Rooms.Clear();
        tilesBackground.Clear();
        tilesWalls.Clear();
        PathDrunk();
        genTool.DrawMap(_mapBackground,_baseBackground,tilesBackground);
        genTool.DrawMap(_mapWalls,_baseWalls,tilesWalls);
    }
    public void GenerateRu()
    {
        Rooms.Clear();
        tilesBackground.Clear();
        tilesWalls.Clear();
        PathRule();
        genTool.DrawMap(_mapBackground,_baseBackground,tilesBackground);
        genTool.DrawMap(_mapWalls,_baseWalls,tilesWalls);
    }
    public void Start()
    {
        Rooms.Clear();
        tilesBackground.Clear();
        tilesWalls.Clear();
        PathRandom();
        DoorHole();
        genTool.DrawMap(_mapBackground,_baseBackground,tilesBackground);
        genTool.DrawMap(_mapWalls,_baseWalls,tilesWalls);
        DoorOpen();
    }

    public void DoorHole()
    {
        _doors.Clear();
        foreach (Vector2Int room in Rooms)
        {
            if (Rooms.Contains(genTool.VonNeumannNeighbours[0]+room))
            {
                _doors.Add(new Vector3Int(room.x, room.y + 7,0));
                _doors.Add(new Vector3Int(room.x-1, room.y + 7,0));
                _doors.Add(new Vector3Int(room.x, room.y + 8,0));
                _doors.Add(new Vector3Int(room.x-1, room.y + 8,0));
            }

            if (Rooms.Contains(genTool.VonNeumannNeighbours[1] + room))
            {
                _doors.Add(new Vector3Int(room.x + 13, room.y,0));
                _doors.Add(new Vector3Int(room.x + 13, room.y-1,0));
                _doors.Add(new Vector3Int(room.x + 14, room.y,0));
                _doors.Add(new Vector3Int(room.x + 14, room.y-1,0));
            }
        }
    }

    public void DoorOpen()
    {
        foreach (var door in _doors)
        {
            _mapWalls.SetTile(door,null);
            _mapBackground.SetTile(door,_baseBackground);
        }
    }
    
    public void DoorClose()
    {
        foreach (var door in _doors)
        {
            Vector3Int a = new Vector3Int(door.x,door.y,0);
            _mapBackground.SetTile(a,null);
            _mapWalls.SetTile(a,_baseWalls);
        }
        
    }
    
    
}
