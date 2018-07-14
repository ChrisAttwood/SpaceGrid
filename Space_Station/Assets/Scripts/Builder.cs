using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//One per scene. Using static instance for easy access.
public class Builder : MonoBehaviour {

    public static Builder instance;
   
    public GameObject BluePrintTile;
    GameObject[] pbs;
    int bpSize = 1000;
    BluePrint CurrentBluePrint;

    Vector2Int? StartPos;
    bool IsDrawing;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CreateAndHideBluePrintBlocks();
    }

    void CreateAndHideBluePrintBlocks()
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

    public void SetActiveBluePrint(BluePrint bp)
    {
        ClearBluePrints();
        IsDrawing = false;
        CurrentBluePrint = bp;
    }



   

    void Update () {

        //If the mouse pointer is over a UI element, do nothing and return.
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
                if (CurrentBluePrint.IsFoundation)
                {
                    BuildFoundation();
                }
                else
                {
                    Build();
                }

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
            pbs[j].transform.position = new Vector3(Shape[j].x, CurrentBluePrint.Level, Shape[j].y);
           
        }

    }

    void ClearBluePrints()
    {
        for (int i = 0; i < bpSize; i++)
        {
            pbs[i].gameObject.SetActive(false);
        }
    }



    void BuildFoundation()
    {
        var shape = GetShape();
        foreach(var spot in shape)
        {
            if (!StationStructure.instance.Foundations.ContainsKey(spot))
            {
                var block = Instantiate(CurrentBluePrint.Structure);
                block.transform.position = new Vector3(spot.x, 0f, spot.y);
                StationStructure.instance.Foundations[spot] = true;
                block.transform.SetParent(StationStructure.instance.transform);
            }
            
        }
    }

    void Build()
    {
        var shape = GetShape();
        foreach (var spot in shape)
        {
            if (StationStructure.instance.Foundations.ContainsKey(spot))
            {
                var block = Instantiate(CurrentBluePrint.Structure);
                block.transform.position = new Vector3(spot.x, CurrentBluePrint.Level, spot.y);
                block.transform.SetParent(StationStructure.instance.transform);
            }

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
            case BuildType.Footprint:
                Shape = FromFootPrint();
                break;
        }

        return Shape;
    }

    List<Vector2Int> FromFootPrint()
    {
        var pos = ClickPlane.instance.GetGridPosition().Value;
        List<Vector2Int> shape = new List<Vector2Int>();
        for (int x = -2; x <= 2; x++)
        {
            for (int y = -2; y <= 2; y++)
            {
                if (CurrentBluePrint.FootPrintData.rows[x+2].row[y+2])
                {
                    shape.Add(new Vector2Int(pos.x + x, pos.y + y));
                }
            }
        }

        return shape;
    }

}
