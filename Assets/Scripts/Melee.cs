using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    private Camera mainCam;
    public Vector3 mouse;
    public GameObject hand;
    public Transform handTransform;

    //Spawner
    public static Melee instance;
    private Stack<GameObject> stack;
    private GameObject spawnPoint;

    void Awake()
    {
        instance = this;
        stack = new Stack<GameObject>();
    }
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        mouse = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mouse - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }
    public void Push(GameObject obj)
    {
        obj.SetActive(false);
        stack.Push(obj);
    }
    public GameObject Pop()
    {
        GameObject obj = stack.Pop();
        obj.SetActive(true);
        obj.transform.position = spawnPoint.transform.position;
        return obj;
    }
    public GameObject Peek()
    {
        return stack.Peek();
    }
    public void RockSmash()
    {
        if (stack.Count != 0)
        {
            Pop();
        }
        else
        {
            Instantiate(hand, handTransform.position, Quaternion.identity);
        }
    }
}
