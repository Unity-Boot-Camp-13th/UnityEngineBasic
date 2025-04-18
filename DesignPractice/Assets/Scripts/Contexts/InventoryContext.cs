using System.IO;
using DP.Models;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections;

namespace DP.Contexts
{
    public class InventoryContext
    {
        public InventoryContext()
        {
            Path = Application.persistentDataPath + "/Inventory.json";
            Load();
        }

        public Inventory Inventory { get; private set; }

        const int DEFAULT_SLOT_SIZE = 32;
        readonly string Path;

        public void Load()
        {
            if (File.Exists(Path))
            {
                // Path 의 File 을 쓰거나 읽는 흐름을 담당할 스트림 생성 (FileMode 는 없으면 만들어서라도 생성)
                // Stream 의 데이터를 읽어야 하므로 Reader 도 생성
                using (FileStream stream = new FileStream(Path, FileMode.OpenOrCreate))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    Inventory = JsonConvert.DeserializeObject<Inventory>(json);
                }

                // File 클래스의 함수를 쓰면 다음처럼 간단하게 사용 가능하다.
                // string json = File.ReadAllText(Path); // json 포맷의 형식이 읽힐 것
                // _inventory = JsonConvert.DeserializeObject<Inventory>(json);
            }
            else
            {
                Inventory = new Inventory(DEFAULT_SLOT_SIZE);
            }
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(Inventory);

            using (FileStream stream = new FileStream(Path, FileMode.OpenOrCreate))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(json);
            }

            // File.WriteAllText(Path, json);
        }
    }
}