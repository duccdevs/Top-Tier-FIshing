using System;
using UnityEngine;

[RequireComponent(typeof (PhotonView))]
public class CosCube : Photon.MonoBehaviour, IPunObservable
{
    internal struct State
    {
        internal double timestamp;
    }
    private SpriteRenderer myFlip;
    private State[] m_BufferedState = new State[20];
    private int m_TimestampCount;
    public double InterpolationDelay = 0.15;

    void Awake()
    {
        myFlip = GetComponent<SpriteRenderer>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(myFlip.flipX);
            stream.SendNext(myFlip.enabled);
        }
        else
        {
            myFlip.flipX = (bool)stream.ReceiveNext();
            myFlip.enabled = (bool)stream.ReceiveNext();

            for (int i = this.m_BufferedState.Length - 1; i >= 1; i--)
            {
                this.m_BufferedState[i] = this.m_BufferedState[i - 1];
            }

            State state;
            state.timestamp = info.timestamp;
            this.m_BufferedState[0] = state;
            this.m_TimestampCount = Mathf.Min(this.m_TimestampCount + 1, this.m_BufferedState.Length);

            for (int i = 0; i < this.m_TimestampCount - 1; i++)
            {
                if (this.m_BufferedState[i].timestamp < this.m_BufferedState[i + 1].timestamp)
                {
                    Debug.Log("State inconsistent");
                }
            }
        }
    }

    public void Update()
    {
        if (this.photonView.isMine || !PhotonNetwork.inRoom)
        {
            return;
        }
        double currentTime = PhotonNetwork.time;
        double interpolationTime = currentTime - this.InterpolationDelay;

        if (this.m_BufferedState[0].timestamp > interpolationTime)
        {
            for (int i = 0; i < this.m_TimestampCount; i++)
            {
                if (this.m_BufferedState[i].timestamp <= interpolationTime || i == this.m_TimestampCount - 1)
                {
                    State rhs = this.m_BufferedState[Mathf.Max(i - 1, 0)];
                    State lhs = this.m_BufferedState[i];

                    double diffBetweenUpdates = rhs.timestamp - lhs.timestamp;
                    float t = 0.0F;
                    if (diffBetweenUpdates > 0.0001)
                    {
                        t = (float)((interpolationTime - lhs.timestamp)/diffBetweenUpdates);
                    }

                    // if t=0 => lhs is used directly
                    return;
                }
            }
        }
        else
        {
            State latest = this.m_BufferedState[0];
        }
    }
}