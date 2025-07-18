using System.IO;
using DP.Models;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System;

namespace DP.Contexts
{
    /// <summary>
    /// 인벤토리 데이터를 로드 / 세이브
    /// </summary>
    public class InventoryContext
    {
        public InventoryContext()
        {
            PATH = Application.persistentDataPath + "/Inventory.json";
            Load();
        }

        public Inventory Inventory { get; private set; }

        const int DEFAULT_SLOT_SIZE = 32;
        readonly string PATH;


        public event Action<IEnumerable<InventorySlot>> OnInventoryChanged;


        public void Load()
        {
            if (File.Exists(PATH))
            {
                // File 클래스의 함수를 쓰면 다음처럼 간단하게 사용 가능하다.
                string json = File.ReadAllText(PATH); // json 포맷의 형식이 읽힐 것
                Inventory = JsonConvert.DeserializeObject<Inventory>(json);
            }
            else
            {
                Inventory = new Inventory(DEFAULT_SLOT_SIZE);
                Save();
            }
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(Inventory);
            File.WriteAllText(PATH, json);
            OnInventoryChanged?.Invoke(Inventory.Slots); // 모든 슬롯 데이터 구독자에게 통지
        }
    }
}