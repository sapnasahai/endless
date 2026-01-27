using UnityEngine;

public class PlayerRewindRecorder : MonoBehaviour
{
    private CircularBuffer<Vector3> positionBuffer;

    private float recordInterval = 1f;
    private float timer;

    [SerializeField] private int bufferSize = 5;


    public int BufferCount => positionBuffer.Count;

    void Awake()
    {
        positionBuffer = new CircularBuffer<Vector3>(bufferSize);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= recordInterval)
        {
            RecordSnapshot();
            timer = 0f;
        }
    }

    private void RecordSnapshot()
    {
        positionBuffer.Add(transform.position);
    }

    public bool HasHistory => positionBuffer.Count > 0;

    public Vector3 GetRewindPosition()
    {
        if (positionBuffer.Count < bufferSize)
            return positionBuffer.GetOldest();

        return positionBuffer.GetFromNewest(bufferSize - 1);
    }

    public void ClearHistory()
    {
        positionBuffer.Clear();
    }

    public  Vector3 GetPositionFromNewest(int offset)
    {
        return positionBuffer.GetFromNewest(offset);
    }
    


}
