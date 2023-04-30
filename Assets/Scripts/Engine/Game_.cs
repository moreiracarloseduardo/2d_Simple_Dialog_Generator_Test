using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_ : MonoBehaviour {
    public static Game_ instance;
    public Rule_ rule_;
    public Ui_ ui;
    public Inventory_ inventory; 


    private void Awake() {
        instance = this;
    }

    void Start() {
        SetRefreshRate();
    }




    void SetRefreshRate() {
        //When dragging an item, if the refresh rate is not set, the frame rate becomes low.
        Resolution[] resolutions = Screen.resolutions;
        int maxRefreshRate = 60;
        foreach (var res in resolutions) {
            if (res.refreshRate > maxRefreshRate) maxRefreshRate = res.refreshRate;
        }
        Application.targetFrameRate = maxRefreshRate;
    }

}
