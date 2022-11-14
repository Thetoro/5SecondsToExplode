using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMovement : MonoBehaviour
{
    public delegate void StartTimeAtLaunch();
    public static event StartTimeAtLaunch onLaunch;

    private Rigidbody2D _rb;
    [SerializeField]
    private GameObject _bomb;
    private Vector3 _worldPos;
    private RaycastHit2D _hitData;
    private float _yeetForce;
    [SerializeField]
    private float _maxYeetForce = 10f;
    [SerializeField]
    private float _strenghtShoot = 100f;
    private bool _isLaunch;
    private Vector3 _bombPosition;
    private LineRenderer _forceLine;
    
    private void Awake() 
    {
        _rb = GetComponent<Rigidbody2D>();
        _forceLine = GetComponent<LineRenderer>();
    }

    private void Start() 
    {
        _isLaunch = false;
        _bombPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {  
        if(!_isLaunch)
            BombMove();
        
    }

    private void BombMove()
    {
        //Click bomb to make it move
        _worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _hitData = Physics2D.Raycast(new Vector2(_worldPos.x, _worldPos.y), Vector2.zero, 0);
        if (_hitData && Input.GetMouseButtonDown(0))
        {
            _bomb = _hitData.transform.gameObject;
            //Debug.Log(_bomb);
        }

        //If bomb is selected calculates where to move
        if (_bomb != null)
        {
            DrawLineForce();
            float posX = _bomb.transform.position.x;
            float posY = _bomb.transform.position.y;
            if (Input.GetMouseButtonUp(0))
            {
                _yeetForce = Mathf.Sqrt(Mathf.Pow(posX - _worldPos.x, 2) + Mathf.Pow(posY - _worldPos.y, 2));
                if(_yeetForce > 5)
                    _yeetForce = _maxYeetForce;
                _rb.AddForce(new Vector2(-_worldPos.x, -_worldPos.y) * _yeetForce * _strenghtShoot);
                //Debug.Log(_yeetForce);
                _isLaunch = true;
                if(onLaunch != null)
                    onLaunch();
            }

        }
    }

    private void DrawLineForce()
    {
        Vector3 oppLine, oppCal = new Vector3();
        oppCal = _worldPos - _bombPosition;
        oppLine = (oppCal * -1) + _bombPosition;
        List<Vector3> pos = new List<Vector3>();
        pos.Add(_bombPosition);
        pos.Add(new Vector3(Mathf.Clamp(oppLine.x, _bombPosition.x - (Mathf.PI*0.5f), _bombPosition.x + (Mathf.PI*0.5f)), Mathf.Clamp(oppLine.y, _bombPosition.y - (Mathf.PI*0.5f), _bombPosition.y + (Mathf.PI*0.5f)), 10));
        _forceLine.startWidth = 0.1f;
        _forceLine.endWidth = 0.1f;
        _forceLine.SetPositions(pos.ToArray());
        _forceLine.useWorldSpace = true;
    }


    
}
