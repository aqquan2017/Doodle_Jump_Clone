using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module.Achievement.Data;
using Module.Achievement;

public class TestAchievementModule : AchievementModule
{
    public AchievementLibrary Library;

    [SerializeField] private string dataString;

    public override void Initialize(AchievementLibrary data)
    {
        base.Initialize(data);
    }

    private void Start()
    {
        Initialize(Library);
    }

    [ContextMenu("TEST")]
    public void Test()
    {
        this.LogActivity(
               new Activity()
               {
                   ID = ActivityID.GET_CONTAINER,
                   Value = 5,
                   actType = Activity.VALUETYPE.ADD

               }
        );

        this.LogActivity(
              new Activity()
              {
                  ID = ActivityID.UPDATE_MONEY,
                  Value = 6000,
                  actType = Activity.VALUETYPE.REPLACE

              }
       );
    }

    [ContextMenu("SAVE")]
    public void TestSave()
    {
        Save();
    }

    [ContextMenu("LOAD DATA")]
    public void LoadData()
    {
        Load(dataString);
    }

    [ContextMenu("TEST BITMASK")]
    public void TestBitMask()
    {
        int bitCheck = 1 << 0;
        int field = 0;

        field.SetBit(bitCheck, true);

        Debug.Log(field);

        field.SetBit(bitCheck, false);
        
        Debug.Log(field);

    }

    private void SetBitMask(ref int mask, int bitSet, bool enable)
    {
        if (enable)
            mask |= bitSet;
        else
            mask &= ~bitSet;
    }

    public override void Save()
    {
        dataString = SaveData;
        Debug.LogError(dataString);
    }

    
    private void OnGetContainer_Callback()
    {
        this.LogActivity(
            new Activity()
            {
                ID = ActivityID.GET_CONTAINER,
                Value = 1,
                actType = Activity.VALUETYPE.ADD

            }
            );
    }


}

