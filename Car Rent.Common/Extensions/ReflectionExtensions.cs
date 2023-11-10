using System.Linq.Expressions;
using System.Reflection;

namespace Car_Rent.Common.Extensions;

public static class ReflectionExtensions
{
    //Returns all non-public instance variables in a type (class/struct) or an empty array
    public static FieldInfo[] GetVariables(this Type type)
        => type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

    /*Get the first collection matching the type T that is instantiated through a constructor
    or at initialization. Throw an exception if no collection of type T exists.*/
    public static FieldInfo? FindCollection<T>(this FieldInfo[] fields) where T : class
        => fields.FirstOrDefault(f => f.FieldType == typeof(List<T>) && f.IsInitOnly)
        ?? throw new InvalidOperationException("Unsupported type");

    // Get the collection data or throw an exception if the collection is null.
    public static object? GetData(this FieldInfo? field, object container)
        => field?.GetValue(container) ?? throw new InvalidDataException();

    public static IQueryable<T> ToQueryable<T>(this object? data) where T : class
        => data is not null && data is List<T> list
            ? list.AsQueryable()
            : throw new InvalidDataException();

    // Filter the result and return it from the collection
    public static List<T> Filter<T>(this IQueryable<T> collection, Expression<Func<T, bool>>? lambda) where T : class
        => lambda is null ? collection.ToList() : collection.Where(lambda).ToList();

    // Add an item
    public static void AddItem<T>(this FieldInfo? field, object container, T item) where T : class
    {
        if (field != null)
        {
            var collection = field.GetValue(container) as List<T>;
            collection?.Add(item);
        }
        else
        {
            throw new InvalidOperationException("Unsupported type");
        }
    }
}
