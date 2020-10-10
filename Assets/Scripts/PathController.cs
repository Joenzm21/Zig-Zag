using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    private bool falling;
    public bool Falling { get => falling; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (falling)
            transform.position -= new Vector3(0, 1 * Time.deltaTime, 0);
        if (transform.position.y < -2)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        StartCoroutine(Fall(0.9f / PlayerController.sratio));
    }

    IEnumerator Fall(float duration)
    {
        yield return new WaitForSeconds(duration);
        falling = true;
    }
}
