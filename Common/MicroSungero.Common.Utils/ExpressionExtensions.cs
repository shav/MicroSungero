using System;
using System.Linq.Expressions;

namespace MicroSungero.Common.Utils
{
  /// <summary>
  /// Extension methods for lambda expressions.
  /// </summary>
  public static class ExpressionExtensions
  {
    /// <summary>
    /// Change expression parameter type.
    /// </summary>
    /// <typeparam name="TInput">Original type of expression parameter.</typeparam>
    /// <typeparam name="TOutput">Type of expression result.</typeparam>
    /// <typeparam name="TConvertedInput">New type of expression parameter.</typeparam>
    /// <param name="expression">Lambda expression.</param>
    /// <returns>Expression with converted parameter type.</returns>
    public static Expression<Func<TConvertedInput, TOutput>> ChangeParameterType<TInput, TOutput, TConvertedInput>(this Expression<Func<TInput, TOutput>> expression)
    {
      Expression converted = Expression.Convert(expression.Body, typeof(TConvertedInput));
      return Expression.Lambda<Func<TConvertedInput, TOutput>> (converted, expression.Parameters);
    }
  }
}
