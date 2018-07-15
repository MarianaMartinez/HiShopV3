using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiShop.Herramientas
{
    /// <summary>
    /// Esta clase se usa para pasar objetos por session
    /// AxelMolaro
    /// </summary>
    public static class SessionExtensions
    {
        /// <summary>
        /// Setear un objeto en session
        /// AxelMolaro
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetObjectAsJson(this Microsoft.AspNetCore.Http.ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static void SetObjectAsJson2(this Microsoft.AspNetCore.Http.ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }


    
        /// <summary>
        /// Optiene un objero de la session
        /// AxelMolaro
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
