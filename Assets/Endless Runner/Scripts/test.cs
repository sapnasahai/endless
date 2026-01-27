using UnityEngine;

public class test : MonoBehaviour
{void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * 5f * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * 5f * Time.deltaTime;
        }
    }
}
