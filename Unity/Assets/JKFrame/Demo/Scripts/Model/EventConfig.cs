
using JKFrame;
using System.Collections.Generic;

public class EventConfig
{
    Dictionary<string, int> kv = new Dictionary<string, int>();

    public int GetEventValue(string id)
    {
        if (kv.ContainsKey(id))
            return kv[id];
        kv.Add(id, TableSystem.Table.EventByID[id].Value);
        return kv[id];
    }

    public void SetEventValue(string id, int value)
    {
        if (kv.ContainsKey(id))
        {
            kv[id] = value;
            return;
        }
        kv.Add(id, value);
    }
}
