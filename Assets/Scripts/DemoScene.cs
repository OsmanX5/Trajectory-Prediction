using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DemoScene : MonoBehaviour
{
    public TMP_Text NeededForce_txt;
    public TMP_Text NeededVelocity_txt;
    public TrajectoryPrediction predector;
    Vector3 Temp;
    // Update is called once per frame
    void Update()
    {
        Temp = predector.NeededForce;
        NeededForce_txt.text = $"Needed Force \n X:{Temp.x} , Y:{Temp.y} , Z:{Temp.z}";

        Temp = predector.NeededVelocity;
        NeededVelocity_txt.text = $"Needed Velocity \n X:{Temp.x} , Y:{Temp.y} , Z:{Temp.z}";
    }
}
