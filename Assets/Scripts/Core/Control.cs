using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    private List<GameObject> _puzzleList = new List<GameObject>();

    public GameObject background;
    public GameObject FireworksPrefab;

    private bool doFireworks = true;
    
    // Start is called before the first frame update
    void Start()
    {
        background.SetActive(false);
        GameObject[] puzzleArray = GameObject.FindGameObjectsWithTag("Puzzle");


        foreach (GameObject item in puzzleArray)
        {
            _puzzleList.Add(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID


        LevelPass();
        // Debug.Log(PuzzleList.Count);
    }

    public void LevelPass()
    {
        if (_puzzleList.Count == 0)
        {

            StartCoroutine(PanelShow());

        }
    }

    public void Fireworks()
    {

        if(doFireworks == true)

        {
            GameObject fireworks = Instantiate(FireworksPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            doFireworks = false;
            Destroy(fireworks, 3f);
        }
             
 
    }
    private IEnumerator PanelShow()
    {
        Fireworks();
        yield return new WaitForSeconds(2f);
        background.SetActive(true);
        yield return null;
    }
    public void RemoveFromList()
    {
        _puzzleList.RemoveAt(0);

    }

#endif
}
