using Mapster;

namespace CleanDDDTemplate.Application.Utility.Mapper
{
    public static class DataMapper
    {
        public static T2 Map<T2>(this object source) where T2 : class => source.Adapt<T2>();

        public static TDestination Map<TDestination>(this object source, out TDestination destination) where TDestination : class
        {
            destination = source.Adapt<TDestination>();
            return destination;
        }

        public static TDestination Map<TSource, TDestination>(this TSource source, TDestination destination) => source.Adapt(destination);
    }
}