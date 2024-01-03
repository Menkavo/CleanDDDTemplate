using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common.ExtentionMethods
{
    public static class ObjectHelper
    {
        /// <summary>
        /// Serilizes the object into a JSON string.
        /// </summary>
        /// <param name="source">The object being serilized.</param>
        /// <returns>A JSON string representation of the object.</returns>
        public static string SerializeObject(this object source) => JsonConvert.SerializeObject(source);

        /// <summary>
        /// Serilizes the object into a JSON string with indented formatting.
        /// </summary>
        /// <param name="source">The object being serilized.</param>
        /// <returns>An indented JSON string representation of the object.</returns>
        public static string SerializeObjectIndented(this object source) => JsonConvert.SerializeObject(source, Formatting.Indented);

        /// <summary>
        /// Deserilizes the specified JSON string into an object.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="source">The JSON string.</param>
        /// <returns>The deserilized object from the JSON string.</returns>
        public static T DeserializeObject<T>(this string source) where T : class => JsonConvert.DeserializeObject<T>(source, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

        /// <summary>
        /// Makes a clone of the object.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="source">The object to be cloned</param>
        /// <returns>A cloned replica of the object.</returns>
        public static T CloneObject<T>(this T source) where T : class => JsonConvert.DeserializeObject<T>(
            JsonConvert.SerializeObject(source, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));

        /// <summary>
        /// Compares 2 objects.
        /// </summary>
        /// <typeparam name="T">Type of the objects</typeparam>
        /// <param name="obj1">First object</param>
        /// <param name="obj2">Second object</param>
        /// <returns>True if values of all properties of both objects are equal.</returns>
        public static bool IsIdenticalWith<T>(this T obj1, T obj2) where T : class =>
            JsonConvert.SerializeObject(obj1, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }) ==
            JsonConvert.SerializeObject(obj2, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

        /// <summary>
        /// Compares multiple objects.
        /// </summary>
        /// <typeparam name="T">Type of the objects</typeparam>
        /// <param name="obj1">First object</param>
        /// <param name="objects">Other objects</param>
        /// <returns>True if the values of all properties of the first object is equal to another object. false if not.</returns>
        public static bool IsIdenticalWithAny<T>(this T obj1, params T?[] objects) => objects.Any(obj =>
        {
            var isIdentical = JsonConvert.SerializeObject(obj1, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }) ==
            JsonConvert.SerializeObject(obj, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return isIdentical;
        });

        /// <summary>
        /// Compares multiple objects.
        /// </summary>
        /// <typeparam name="T">Type of the objects</typeparam>
        /// <param name="obj1">First object</param>
        /// <param name="objects">Other objects</param>
        /// <returns>True if the values of all properties of the first object is equal to all other objects. false if not.</returns>
        public static bool IsIdenticalWithAll<T>(this T obj1, params T?[] objects) => objects.All(obj =>
        {
            var isIdentical = JsonConvert.SerializeObject(obj1, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }) ==
            JsonConvert.SerializeObject(obj, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return isIdentical;
        });

        /// <summary>
        /// Converts an object of type T1 to an object of type T2
        /// </summary>
        /// <typeparam name="T1">Source type</typeparam>
        /// <typeparam name="T2">Destination type</typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T2 ConvertObject<T1, T2>(this T1 source) where T1 : class where T2 : class => JsonConvert.DeserializeObject<T2>(JsonConvert.SerializeObject(source, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));

        /// <summary>
        /// Converts a list of type T1 to a list of type T2
        /// </summary>
        /// <typeparam name="T1">Source type</typeparam>
        /// <typeparam name="T2">Destination type</typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<T2> ConvertList<T1, T2>(this List<T1> source) where T1 : class where T2 : class => JsonConvert.DeserializeObject<List<T2>>(JsonConvert.SerializeObject(source, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include, ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));

        /// <summary>
        /// Clones the object with the change specified.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="obj">The object that is being cloned.</param>
        /// <param name="changes">The change that is being made to the property of the object.</param>
        /// <returns>A new cloned object with the new property value.</returns>
        public static T AsNew<T>(this T obj, Action<T> changes) where T : class
        {
            T obj2 = obj.CloneObject();
            changes(obj2);
            return obj2;
        }

        /// <summary>
        /// Clones the object with the changes specified.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="obj">The object that is being cloned.</param>
        /// <param name="changes">The changes that is being made to the properties of the object.</param>
        /// <returns>A new cloned object with the new property values.</returns>
        public static T AsNew<T>(this T obj, Func<T, T> changes) where T : class
        {
            T obj2 = obj.CloneObject();
            return changes(obj2);
        }

        /// <summary>
        /// Parses a json to an object with designated type.
        /// Returns false rather than throwing an exception if input is invalid.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="convertedObject"></param>
        /// <returns></returns>
        public static bool TryConvert<T>(this string source, out T convertedObject)
        {
            var x = new List<int> { };
            x.AsNew(xx => xx.Where(y => y == 3));
            convertedObject = default;
            try
            {
                convertedObject = JToken.Parse(source).ToObject<T>();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}