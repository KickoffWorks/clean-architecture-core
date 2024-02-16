using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sample.Core.Infrastructure.Configurations;

public static class EntityTypeBuilderExtensions
{
    public static void ConfigureEntityProperties<T>(this EntityTypeBuilder<T> builder) where T : class
    {
        foreach (var property in typeof(T).GetProperties())
        {
            var columnName = property.Name;
            var columnType = GetColumnType(property.PropertyType);

            if (!string.IsNullOrEmpty(columnType))
            {
                builder.Property(columnName).HasColumnType(columnType);
            }
        }
    }

    private static string? GetColumnType(Type propertyType)
    {
        if (propertyType == typeof(Guid))
        {
            return "uuid";
        }
        else if (propertyType == typeof(string))
        {
            return "text";
        }
        else if (propertyType == typeof(int))
        {
            return "integer";
        }
        else if (propertyType == typeof(bool))
        {
            return "boolean";
        }
        else if (propertyType == typeof(DateTime))
        {
            return "timestamp without time zone";
        }

        return null;
    }
}