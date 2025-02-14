﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script
{
    public class GlobalUtilities
    {
        public static string SERVER_ADDR = "192.168.0.102:8080";
        public static string LoadServerAddress()
        {
            string serverAddr = "";
            string filePath = "/Assets/Resources/VRSlateServerAddress.txt";
            if(Application.platform == RuntimePlatform.WindowsEditor)
            {
                filePath = "/Assets/Resources/VRSlateServerAddress.txt";
                TextAsset serverAddrTxt = (TextAsset)Resources.Load("VRSlateServerAddress", typeof(TextAsset));
                serverAddr = serverAddrTxt.text;
            }
            else if(Application.platform == RuntimePlatform.Android)
            {
                filePath = Application.persistentDataPath + "/VRSlateServerAddress.txt";
                if(!File.Exists(filePath))
                {
                    FileStream Fs = File.Create(filePath);
                    StreamWriter sw = new StreamWriter(Fs);
                    sw.WriteLine(GlobalUtilities.SERVER_ADDR);
                    sw.Close();
                }
                using (StreamReader sr = File.OpenText(filePath))
                {
                    serverAddr = sr.ReadLine();
                }
            }
            
            if(serverAddr == "")
            {
                serverAddr = SERVER_ADDR;
            }
            return serverAddr;
        }
        
        public static Vector2 ConvertMobileRelPosToUnityRelPos(Vector2 mobileRelPos)
        {
            return new Vector2(mobileRelPos.x - 0.5f, 0.5f - mobileRelPos.y);
        }
        static public Vector3 boundPointToContainer(Vector3 src, Rect container2D)
        {
            Vector3 boundedPoint = new Vector3(src.x, src.y, src.z);
            if (boundedPoint.x < container2D.xMin)
            {
                boundedPoint.x = container2D.x;
            }
            else if (boundedPoint.x > container2D.xMax)
            {
                boundedPoint.x = container2D.xMax;
            }
            if (boundedPoint.y < container2D.yMin)
            {
                boundedPoint.y = container2D.yMin;
            }
            else if (boundedPoint.y > container2D.yMax)
            {
                boundedPoint.y = container2D.yMax;
            }
            return boundedPoint;
        }
    }
}
