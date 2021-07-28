using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleMover : MonoBehaviour
{
    
    private GameObject _activePiece;
    private Camera _cam;
    private Vector3 _childPos;
    private Vector3 _startPos;
    
    //public Transform DestinationPos;
    private float _distance;

    Control _control;
    
    
   
    public float offset; // Camera offset
    public float Speed;  // Piece following touch

   // Vector3 touchPos = new Vector3();
    

    // public static int currTouch = 0;

    private bool _isPiecePicked = true;
    private bool _getStartPos = true;
   
   
    
   // public Transform DestPos;
   // public Transform StartPos;
    
    // Start is called before the first frame update
    void Start()
    {
        _control = FindObjectOfType<Control>();
        
        
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
      
       // MouseControls();
   

#if UNITY_ANDROID

        MovePiece();

        
    }


    private void MouseControls()
    {
        
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, offset));



            RaycastHit hit;

            
            if (Physics.Raycast(_cam.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, out hit))
            {
              
                if ( hit.collider.tag == "InPlace") return;
                if (hit.collider.gameObject.tag == "Puzzle" && hit.collider.gameObject != null && _isPiecePicked)
                {

                    _activePiece = hit.collider.gameObject;


                    GetStartPos(_activePiece);

                    _getStartPos = false;
                    _isPiecePicked = false;
                    


                }
                


            }
            if (_activePiece == null) return;
            _distance = Vector3.Distance(_activePiece.transform.position, _activePiece.transform.parent.position);

            _activePiece.transform.position = Vector3.Lerp(_activePiece.transform.position, mousePos, Speed * Time.deltaTime);
        }
        
            

        if (Input.GetMouseButtonUp(0) && _activePiece != null)
        {

            _getStartPos = true;
            _isPiecePicked = true;

            if (_distance < 1)
            {

                _activePiece.transform.position = _activePiece.transform.parent.position;
                _activePiece.gameObject.tag = "InPlace";
               // _tmpPuzzle = null;


                _control.RemoveFromList();
            }
            else
            {
                // _tmpPuzzle.transform.position = Vector3.Lerp(_tmpPuzzle.transform.position, _startPos, Speed * Time.deltaTime);
                _activePiece.transform.position = _startPos;
                _activePiece = null;
            }
        }

        
            

        
        
    }

    private void GetStartPos(GameObject puzzlePiece)
    {
        if (_getStartPos)
        {
            _childPos = _activePiece.transform.GetChild(0).position;
            _startPos = _childPos;
        }

    }
    // ---------------------------------------Touch Controls----------------------------------------------

    private void MovePiece()
    {




        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {

            RaycastHit hit;

            // Get the active piece
            if (Physics.Raycast(_cam.ScreenToWorldPoint(touch.position), Vector3.forward, out hit))
            {

                if (hit.collider.tag == "InPlace") return;
                if (hit.collider.gameObject.tag == "Puzzle" && hit.collider.gameObject != null && _isPiecePicked)
                {

                    _activePiece = hit.collider.gameObject;

                    // Destroy(_tmpPuzzle.gameObject);
                    GetStartPos(_activePiece);

                    _getStartPos = false;
                    _isPiecePicked = false;



                }



            }
        }
        //Puzzle piece movement
        if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
        {
            Vector3 touchPos = _cam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, offset));

            if (_activePiece == null) return;
            _distance = Vector3.Distance(_activePiece.transform.position, _activePiece.transform.parent.position);

            _activePiece.transform.position = Vector3.Lerp(_activePiece.transform.position, touchPos, Speed * Time.deltaTime);

        }

        if (touch.phase == TouchPhase.Ended)
        {
            _getStartPos = true;
            _isPiecePicked = true;

            // Piece in place
            if (_distance < 0.5f)
            {

                _activePiece.transform.position = _activePiece.transform.parent.position;
                //_tmpPuzzle.gameObject.tag = "InPlace";
                _activePiece = null;


                _control.RemoveFromList();
            }
            // Piece back to start
            else
            {
                // _tmpPuzzle.transform.position = Vector3.Lerp(_tmpPuzzle.transform.position, _startPos, Speed * Time.deltaTime);
                _activePiece.transform.position = _startPos;
                _activePiece = null;
            }
        }
        
        
        


        



#endif



    }
    



}
