using System.Collections.Generic;
using GoogleSheetsToUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "SheetDataReader", menuName = "SO/SheetDataReader")]
public class SheetDataReader : SheetData
{
    [SerializeField] public List<ItemData> dataList = new List<ItemData>();

    // 셀 리스트를 전달 받아서, 전달받은 키에 따라 값 적용
    public void UpdateSheetData(List<GSTU_Cell> list, int id)
    {
        // 각 행을 읽을 때마다, 클래스 생성 후 리스트에 추가
        
        int Id = 0;
        string Name = null;
        string Description = null;
        int Value = 0;
        int Count = 0;
        int Price = 0;

        // 리스트의 개수만큼 작업을 진행합니다.
        for (int i = 0; i < list.Count; i++)
        {
            // 현재 순서의 칼럼 아이디를 조사해서 그 값을 적용
            switch(list[i].columnId)
            {
                case "id" :
                    Id = int.Parse(list[i].value);
                    break;

                case "name":
                    Name = list[i].value;
                    break;

                case "description":
                    Description = list[i].value;
                    break;

                case "value":
                    Value = int.Parse(list[i].value);
                    break;

                case "count":
                    Count = int.Parse(list[i].value);
                    break;

                case "price":
                    Price = int.Parse(list[i].value);
                    break;
            }
        }

        dataList.Add(new ItemData(Id, Name, Description, Value, Count, Price));

    }
}
