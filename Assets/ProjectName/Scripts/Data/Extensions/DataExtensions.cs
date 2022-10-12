using UnityEngine;

namespace ProjectName.Scripts.Data.Extensions
{
    public static class DataExtensions
    {
        public static Vector3Data AsVector3Data(this Vector3 self) =>
            new Vector3Data(self.x, self.y, self.z);

        public static Vector3 AsUnityVector(this Vector3Data self) =>
            new Vector3(self.X, self.Y, self.Z);

        public static T ToDeserialize<T>(this string json) =>
            JsonUtility.FromJson<T>(json);

        public static string ToJson(this object self) =>
            JsonUtility.ToJson(self);

        public static Vector3 AddY(this Vector3 self, float y)
        {
            self.y += y;
            return self;
        }
    }
}