using Newtonsoft.Json;

namespace IronGrip.Extensions
{
    public static class SessionExtension
    {

        public static T GetObject<T>(this ISession session, string key)
        {
            string datos = session.GetString(key);
            if(datos == null)
            {
                return default;
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(datos);
            }
        }

        public static void SetObject(this ISession session, string key, object value)
        {
            string json = JsonConvert.SerializeObject(value);
            session.SetString(key, json);
        }

    }
}
