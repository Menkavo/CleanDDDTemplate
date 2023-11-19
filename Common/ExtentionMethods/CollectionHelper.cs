namespace Common.ExtentionMethods
{
    public static class CollectionHelper
    {
        public static bool IsCollectionNull<T>(this IEnumerable<T> collection) => collection == null;

        public static bool IsCollectionNotNull<T>(this IEnumerable<T> collection) => collection != null;

        public static bool IsCollectionNullOrEmpty<T>(this IEnumerable<T> collection) => collection == null || !collection.Any();

        public static bool IsCollectionNotNullOrEmpty<T>(this IEnumerable<T> collection) => collection != null && collection.Any();
    }
}