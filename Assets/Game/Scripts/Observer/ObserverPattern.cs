using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverPattern : MonoBehaviour
{
    private void Start()
    {
        main(); 
    }
    static void main()
    {
        var videoData = new VideoData();

        _ = new YoutobeNotify(videoData);
        _ = new FacebookNotidy(videoData);

        videoData.setTitle("oserver");  
    }
}
