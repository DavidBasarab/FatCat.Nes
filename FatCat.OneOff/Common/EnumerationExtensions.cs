using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;

namespace FatCat.OneOff.Common
{
	[PublicAPI]
	public static class EnumerationExtensions
	{
		public static bool Empty<T>(this IEnumerable<T> source) => !source.Any();

		public static bool Empty<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) => !source.Any(predicate);

		public static string GetDescription(this Enum value)
		{
			var type = value.GetType();
			var name = Enum.GetName(type, value);

			if (name == null) return string.Empty;

			var field = type.GetField(name);

			if (field != null)
			{
				var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

				if (attr != null) return attr.Description;
			}

			return value.ToString();
		}

		public static string GetDescriptionText<T>([NotNull] this T value)
		{
			if (value == null) throw new ArgumentNullException(nameof(value));

			var type = typeof(T);

			var name = Enum.GetName(type, value);

			if (name == null) return string.Empty;

			var field = type.GetField(name);

			if (field != null)
			{
				var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

				if (attr != null) return attr.Description;
			}

			return value.ToString();
		}

		public static bool IsFlagNotSet<T>(this T value, T flag) where T : struct
		{
			var testValueNumber = Convert.ToInt64(value);
			var flagNumberValue = Convert.ToInt64(flag);

			return (testValueNumber & flagNumberValue) == 0;
		}

		public static bool IsFlagSet<T>(this T value, T flag) where T : struct
		{
			var testValueNumber = Convert.ToInt64(value);
			var flagNumberValue = Convert.ToInt64(flag);

			return (testValueNumber & flagNumberValue) != 0;
		}

		public static T ToEnum<T>(this string value) => (T)Enum.Parse(typeof(T), value, true);

		public static T ToEnum<T>(this string value, T errorValue)
		{
			try { return (T)Enum.Parse(typeof(T), value, true); }
			catch { return errorValue; }
		}

		public static IList<T> ToList<T>() => (from object value in Enum.GetValues(typeof(T))
												select (T)value).ToList();
	}
}