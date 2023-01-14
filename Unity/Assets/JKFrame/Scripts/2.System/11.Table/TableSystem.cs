using System.IO;
using System;
using UnityEngine;

public static class TableSystem
{
    public static main.Table Table;

    public static void LoadAllTable()
    {
        using (var stream = new FileStream($"{Application.dataPath}/JKFrame/Table/table_gen.bin", FileMode.Open))
        {
            stream.Position = 0;

            var reader = new tabtoy.TableReader(stream);

            Table = new main.Table();

            try
            {
                Table.Deserialize(reader);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }

            // 建立所有数据的索引
            Table.IndexData();
        }
    }
}
