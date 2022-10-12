using System;
using System.Collections.Generic;
using Object = System.Object;

namespace ProjectName.Scripts.Utilities
{
    public static class ExceptionValidator
    {
        public static void ThrowIfNull(params Object[] objects)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] == null)
                    throw new ArgumentNullException(GetIndexMessage(i));
            }
        }

        public static void ThrowIfTrue(params bool[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if(values[i])
                    throw new ArgumentException(GetIndexMessage(i));
            }
        }

        public static void ThrowIfContainsKey<K, V>(Dictionary<K, V> dictionary, K key)
        {
            if (dictionary.ContainsKey(key))
                throw new ArgumentNullException("Current collection contains this key");
        }
        
        public static void ThrowIfNotContainsKey<K, V>(Dictionary<K, V> dictionary, K key)
        {
            if (!dictionary.ContainsKey(key))
                throw new ArgumentNullException("Current collection not contains this key");
        }

        public static void ThrowIfContains<T>(List<T> list, T value)
        {
            if (list.Contains(value))
                throw new ArgumentNullException("Current list contains this value");
        }
        
        public static void ThrowIfNotContains<T>(List<T> list, T value)
        {
            if (!list.Contains(value))
                throw new ArgumentNullException("Current list not contains this value");
        }

        public static void ThrowIfEnumNone(int value)
        {
            if (value == 0)
                throw new ArgumentNullException("Current enum value is none");
        }

        private static string GetIndexMessage(int index) => "index = " + index;

    }
}
