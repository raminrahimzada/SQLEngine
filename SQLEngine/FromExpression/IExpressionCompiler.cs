using System;
using System.Linq.Expressions;

namespace SQLEngine;

public interface IExpressionCompiler
{
    string Compile<T>(Expression<Func<T, bool>> expression);
    string Compile<TEntity,TProperty>(Expression<Func<TEntity, TProperty>> expression);
}