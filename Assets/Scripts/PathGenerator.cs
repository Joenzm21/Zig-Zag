using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathGenerator : MonoBehaviour
{
    public int bonus;
    public GameObject rhombus;
    public Text text;
    int counter = -1;
    List<GameObject> paths = new List<GameObject>();
    public static float hbsize;
    public static float hpsize;

    // Start is called before the first frame update
    void Start()
    {
        hbsize = GetComponent<Renderer>().bounds.size.x / 2;
        hpsize = rhombus.GetComponent<Renderer>().bounds.size.x / 2;
        Init();
    }
    private void OnCollisionExit(Collision collision)
    {
        counter++;
        text.text = counter.ToString();
        paths.RemoveAt(0);
        CreatePath();
    }
    void Init()
    {
        GameObject obj = Instantiate(rhombus, Vector3.zero, Quaternion.Euler(0, 45, 0));
        obj.transform.localScale = new Vector3(5f, 0.75f, 5f);
        float hmsize = obj.GetComponent<Renderer>().bounds.size.x / 2;
        obj = Instantiate(rhombus, new Vector3(hpsize, 0, hmsize), Quaternion.Euler(0, 45, 0));
        paths.Add(obj);
        for (int i = 2; i < 50; i++)
            CreatePath();
    }
    public void ResetGame()
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Respawn"))
            Destroy(gameObject);
        paths.Clear();
        counter = -1;
        Init();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bonus")
            counter += bonus;
    }
    void CreatePath()
    {
        Vector3 lastpos = paths[paths.Count - 1].transform.position;
        GameObject obj;
        if (UnityEngine.Random.Range(0, 101) % 2 == 0)
            obj = Instantiate(rhombus, lastpos + new Vector3(-hpsize, 0, hpsize), Quaternion.Euler(0, 45, 0));

        else
            obj = Instantiate(rhombus, lastpos + new Vector3(hpsize, 0, hpsize), Quaternion.Euler(0, 45, 0));
        paths.Add(obj);
    }
}
