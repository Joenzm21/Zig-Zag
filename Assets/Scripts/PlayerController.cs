using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static float sratio = 1;

    public float speed;
    public float acceleration;
    public float maxspeed;
    public GameObject gotextobj;
    public GameObject satextobj;
    public GameObject transobj;
    public GameObject stransobj;
    public bool started = false;

    private bool setuping = false;
    private bool showing = true;
    private bool failed = false;
    private float cspeed;
    private Vector3 lastpos;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        cspeed = speed;
        StartCoroutine(Wait());
    }
    // Update is called once per frame
    void Update()
    {
        if (!failed && started && !showing)
        {
            transform.Translate((Input.GetKey("space") ? Vector3.forward : Vector3.left) * cspeed * Time.deltaTime);
            if (cspeed < maxspeed)
            {
                cspeed *= Mathf.Pow(1 + acceleration, Time.deltaTime);
                sratio = cspeed / speed;
            }
        }
        if (!started && !showing && Input.GetKeyDown("space"))
        {
            started = true;
            satextobj.SetActive(false);
        }
        if (failed && !setuping && !showing && Input.GetKeyDown("space"))
            StartCoroutine(ResetGame());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        stransobj.SetActive(false);
        showing = false;
        satextobj.SetActive(true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        lastpos = collision.gameObject.transform.position;
    }
    private void LateUpdate()
    {
        if ((failed && !setuping) || transform.position.y < 0.575f)
        {
            if (!failed)
            {
                showing = true;
                failed = true;
                gotextobj.SetActive(true);
                StartCoroutine(WaitS());
            }
            transform.Translate(((transform.position - lastpos).x > 0 ? Vector3.forward : Vector3.left) * Mathf.Min(1f + cspeed, 5f) * Time.deltaTime);
        }
    }
    IEnumerator WaitS()
    {
        yield return new WaitForSeconds(1f);
        showing = false;
    }
    IEnumerator ResetGame()
    {
        setuping = true;
        transobj.SetActive(true);
        yield return new WaitForSeconds(0.59f);
        transform.position = new Vector3(0, 0.575f, 0);
        GetComponent<PathGenerator>().ResetGame();
        gotextobj.GetComponentInChildren<Text>().color = new Color(0, 139, 236, 0);
        gotextobj.SetActive(false);
        GetComponent<PathGenerator>().text.text = "0";
        yield return new WaitForSeconds(0.38f);
        transobj.SetActive(false);
        cspeed = speed;
        yield return new WaitForSeconds(0.3f);
        failed = false;
        setuping = false;

    }
}