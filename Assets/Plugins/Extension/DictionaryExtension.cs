using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.Extension
{
    public static class DictionaryExtension
    {
        public static void AddOrCreate<Tkey,Tvalue>(this Dictionary<Tkey,Tvalue> self,Tkey key,Tvalue value)
        {
            if (self.ContainsKey(key))
                self[key] = value;
            else
                self.Add(key, value);
        }
    }
}
