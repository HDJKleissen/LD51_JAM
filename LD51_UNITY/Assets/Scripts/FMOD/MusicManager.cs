using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System.Runtime.InteropServices;
using System;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public int section = 0;

    public delegate void BeateEventDelegate(int beat);
    public delegate void MarkerEventDelegate(string markerName);
    public static event BeateEventDelegate BeatUpdated;
    public static event MarkerEventDelegate MarkerUpdated;

    private int lastBeat = 0;
    private string lastMarker = "";

    [StructLayout(LayoutKind.Sequential)]

    private class TimelineInfo
    {
        public int currentBeat = 0;
        public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
    }

    private TimelineInfo timelineInfo;
    private GCHandle timelineHandle;

    private FMOD.Studio.EVENT_CALLBACK beatCallback;

    [SerializeField]
    private EventReference Music;

    private FMOD.Studio.EventInstance musicInstance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        Debug.Log("DDOL!");
        DontDestroyOnLoad(transform.root.gameObject);
        timelineInfo = new TimelineInfo();
        beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallBack);

        timelineHandle = GCHandle.Alloc(timelineInfo, GCHandleType.Pinned);

        musicInstance = RuntimeManager.CreateInstance("event:/Music");
        musicInstance.start();
        musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));

        musicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);

    }

    public void SetMusicSection(int value)
    {
        musicInstance.setParameterByName("Section", value);
        section = value;
    }

    private void OnDestroy()
    {
        musicInstance.setUserData(IntPtr.Zero);

        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();

        timelineHandle.Free();
    }

    private void Update()
    {
        if (lastBeat != timelineInfo.currentBeat)
        {
            lastBeat = timelineInfo.currentBeat;
            if (BeatUpdated != null)
                BeatUpdated(timelineInfo.currentBeat);
        }
        if (lastMarker != timelineInfo.lastMarker)
        {
            lastMarker = timelineInfo.lastMarker;
            if (MarkerUpdated != null)
                MarkerUpdated(timelineInfo.lastMarker);
        }
    }

    private static FMOD.RESULT BeatEventCallBack(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(instancePtr);

        FMOD.RESULT result = instance.getUserData(out IntPtr timelineInfoPtr);
        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError("Timeline Callback Error " + result);
        }
        else if (timelineInfoPtr != IntPtr.Zero)
        {
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;

            switch (type)
            {
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                    {
                        FMOD.Studio.TIMELINE_BEAT_PROPERTIES parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                        timelineInfo.currentBeat = parameter.beat;
                    }
                    break;
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        FMOD.Studio.TIMELINE_MARKER_PROPERTIES parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        timelineInfo.lastMarker = parameter.name;
                    }
                    break;
            }
        }
        return FMOD.RESULT.OK;
    }
}
