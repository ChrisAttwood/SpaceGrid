using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Builder : MonoBehaviour {

    public static Builder instance;
   
    public GameObject BluePrintTile;
    GameObject[] pbs;
    int bpSize = 100;
    BluePrint CurrentBluePrint;

    Vector2Int? StartPos;
    bool IsDrawing;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GameObject holder = new GameObject("bluePrintDesigner");
        pbs = new GameObject[bpSize];
        for (int i = 0; i < bpSize; i++)
        {
            pbs[i] = Instantiate(BluePrintTile);
            pbs[i].transform.parent = holder.transform;
            pbs[i].gameObject.SetActive(false);
        }
      

    }

    public void SetBluePrint(BluePrint bp)
    {

        ClearBluePrints();
        IsDrawing = false;
        CurrentBluePrint = bp;
    }



   

    void Update () {

        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (CurrentBluePrint != null)
            {
                IsDrawing = true;

                StartPos = ClickPlane.instance.GetGridPosition();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (CurrentBluePrint != null)
            {
                Build();
                IsDrawing = false;
                ClearBluePrints();

            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            CurrentBluePrint = null;
            IsDrawing = false;
        }


        if (IsDrawing)
        {
            UpdateBluePrint();
        }

    }

    void UpdateBluePrint()
    {

        ClearBluePrints();
        List<Vector2Int> Shape = GetShape();

        for (int j = 0; j < Shape.Count; j++)
        {
            pbs[j].gameObject.SetActive(true);
            pbs[j].transform.position = new Vector3(Shape[j].x,0f , Shape[j].y);
           
        }

    }

    void ClearBluePrints()
    {
        for (int i = 0; i < bpSize; i++)
        {
            pbs[i].gameObject.SetActive(false);
        }
    }



    void Build()
    {

        var shape = GetShape();

        foreach(var spot in shape)
        {
            var block = Instantiate(CurrentBluePrint.Structure);
            block.transform.position = new Vector3(spot.x,0f,spot.y);
        }
    }


    List<Vector2Int> GetShape()
    {
        List<Vector2Int> Shape = new List<Vector2Int>();

        switch (CurrentBluePrint.BuildType)
        {
            case BuildType.Single:
                Shape = new List<Vector2Int> { ClickPlane.instance.GetGridPosition().Value };
                break;
            case BuildType.Line:
                Shape = ShapeMaths.Line(StartPos.Value, ClickPlane.instance.GetGridPosition().Value);
                break;
            case BuildType.Box:
                Shape = ShapeMaths.Box(StartPos.Value, ClickPlane.instance.GetGridPosition().Value);
                break;
        }

        return Shape;
    }

}
