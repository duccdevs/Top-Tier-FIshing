using System;
using UnityEngine;

[RequireComponent(typeof (PhotonView))]
public class FishLoc : Photon.MonoBehaviour, IPunObservable
{
    internal struct State
    {
        internal double timestamp;
        internal Vector3 pos;
        internal Quaternion rot;
    }

    private State[] m_BufferedState = new State[20];
    private int m_TimestampCount;
    public double InterpolationDelay = 0.15;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            Vector3 pos = transform.localPosition;
            Quaternion rot = transform.localRotation;
            stream.Serialize(ref pos);
            stream.Serialize(ref rot);
            stream.SendNext(Environment.TickCount);
        }
        else
        {
            Vector3 pos = Vector3.zero;
            Quaternion rot = Quaternion.identity;
            stream.Serialize(ref pos);
            stream.Serialize(ref rot);

            for (int i = this.m_BufferedState.Length - 1; i >= 1; i--)
            {
                this.m_BufferedState[i] = this.m_BufferedState[i - 1];
            }

            State state;
            state.timestamp = info.timestamp;
            state.pos = pos;
            state.rot = rot;
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
                    transform.localPosition = Vector3.Lerp(lhs.pos, rhs.pos, t);
                    transform.localRotation = Quaternion.Slerp(lhs.rot, rhs.rot, t);
                    return;
                }
            }
        }
        else
        {
            State latest = this.m_BufferedState[0];

            transform.localPosition = Vector3.Lerp(transform.localPosition, latest.pos, Time.deltaTime*20);
            transform.localRotation = latest.rot;
        }
    }
}