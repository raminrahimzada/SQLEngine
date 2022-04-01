using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#pragma warning disable CA2208 // Instantiate argument exceptions correctly

namespace SQLEngine.SqlServer;

public class SqlExpressionCompiler: IExpressionCompiler
{
    public string Compile<T>(Expression<Func<T, bool>> expression)
    {
        switch (expression.NodeType)
        {
            case ExpressionType.Add:
                break;
            case ExpressionType.AddChecked:
                break;
            case ExpressionType.And:
                break;
            case ExpressionType.AndAlso:
                break;
            case ExpressionType.ArrayLength:
                break;
            case ExpressionType.ArrayIndex:
                break;
            case ExpressionType.Call:
                break;
            case ExpressionType.Coalesce:
                break;
            case ExpressionType.Conditional:
                break;
            case ExpressionType.Constant:
                break;
            case ExpressionType.Convert:
                break;
            case ExpressionType.ConvertChecked:
                break;
            case ExpressionType.Divide:
                break;
            case ExpressionType.Equal:
                break;
            case ExpressionType.ExclusiveOr:
                break;
            case ExpressionType.GreaterThan:
                break;
            case ExpressionType.GreaterThanOrEqual:
                break;
            case ExpressionType.Invoke:
                break;
            case ExpressionType.Lambda:
                if (expression.Body is MethodCallExpression body)
                {
                    return Compile(body);
                }
                if (expression.Body is UnaryExpression unaryExpression)
                {
                    return Compile(unaryExpression);
                }
                if (expression.Body is BinaryExpression binaryExpression)
                {
                    return Compile(binaryExpression);
                }

                break;
            case ExpressionType.LeftShift:
                break;
            case ExpressionType.LessThan:
                break;
            case ExpressionType.LessThanOrEqual:
                break;
            case ExpressionType.ListInit:
                break;
            case ExpressionType.MemberAccess:
                break;
            case ExpressionType.MemberInit:
                break;
            case ExpressionType.Modulo:
                break;
            case ExpressionType.Multiply:
                break;
            case ExpressionType.MultiplyChecked:
                break;
            case ExpressionType.Negate:
                break;
            case ExpressionType.UnaryPlus:
                break;
            case ExpressionType.NegateChecked:
                break;
            case ExpressionType.New:
                break;
            case ExpressionType.NewArrayInit:
                break;
            case ExpressionType.NewArrayBounds:
                break;
            case ExpressionType.Not:
                break;
            case ExpressionType.NotEqual:
                break;
            case ExpressionType.Or:
                break;
            case ExpressionType.OrElse:
                break;
            case ExpressionType.Parameter:
                break;
            case ExpressionType.Power:
                break;
            case ExpressionType.Quote:
                break;
            case ExpressionType.RightShift:
                break;
            case ExpressionType.Subtract:
                break;
            case ExpressionType.SubtractChecked:
                break;
            case ExpressionType.TypeAs:
                break;
            case ExpressionType.TypeIs:
                break;
            case ExpressionType.Assign:
                break;
            case ExpressionType.Block:
                break;
            case ExpressionType.DebugInfo:
                break;
            case ExpressionType.Decrement:
                break;
            case ExpressionType.Dynamic:
                break;
            case ExpressionType.Default:
                break;
            case ExpressionType.Extension:
                break;
            case ExpressionType.Goto:
                break;
            case ExpressionType.Increment:
                break;
            case ExpressionType.Index:
                break;
            case ExpressionType.Label:
                break;
            case ExpressionType.RuntimeVariables:
                break;
            case ExpressionType.Loop:
                break;
            case ExpressionType.Switch:
                break;
            case ExpressionType.Throw:
                break;
            case ExpressionType.Try:
                break;
            case ExpressionType.Unbox:
                break;
            case ExpressionType.AddAssign:
                break;
            case ExpressionType.AndAssign:
                break;
            case ExpressionType.DivideAssign:
                break;
            case ExpressionType.ExclusiveOrAssign:
                break;
            case ExpressionType.LeftShiftAssign:
                break;
            case ExpressionType.ModuloAssign:
                break;
            case ExpressionType.MultiplyAssign:
                break;
            case ExpressionType.OrAssign:
                break;
            case ExpressionType.PowerAssign:
                break;
            case ExpressionType.RightShiftAssign:
                break;
            case ExpressionType.SubtractAssign:
                break;
            case ExpressionType.AddAssignChecked:
                break;
            case ExpressionType.MultiplyAssignChecked:
                break;
            case ExpressionType.SubtractAssignChecked:
                break;
            case ExpressionType.PreIncrementAssign:
                break;
            case ExpressionType.PreDecrementAssign:
                break;
            case ExpressionType.PostIncrementAssign:
                break;
            case ExpressionType.PostDecrementAssign:
                break;
            case ExpressionType.TypeEqual:
                break;
            case ExpressionType.OnesComplement:
                break;
            case ExpressionType.IsTrue:
                break;
            case ExpressionType.IsFalse:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        throw new ArgumentOutOfRangeException();
    }
    private static string Compile(UnaryExpression expression)
    {
        switch (expression.NodeType)
        {
            case ExpressionType.Add:
                break;
            case ExpressionType.AddChecked:
                break;
            case ExpressionType.And:
                break;
            case ExpressionType.AndAlso:
                break;
            case ExpressionType.ArrayLength:
                break;
            case ExpressionType.ArrayIndex:
                break;
            case ExpressionType.Call:
                break;
            case ExpressionType.Coalesce:
                break;
            case ExpressionType.Conditional:
                break;
            case ExpressionType.Constant:
                break;
            case ExpressionType.Convert:
                break;
            case ExpressionType.ConvertChecked:
                break;
            case ExpressionType.Divide:
                break;
            case ExpressionType.Equal:
                break;
            case ExpressionType.ExclusiveOr:
                break;
            case ExpressionType.GreaterThan:
                break;
            case ExpressionType.GreaterThanOrEqual:
                break;
            case ExpressionType.Invoke:
                break;
            case ExpressionType.Lambda:
                break;
            case ExpressionType.LeftShift:
                break;
            case ExpressionType.LessThan:
                break;
            case ExpressionType.LessThanOrEqual:
                break;
            case ExpressionType.ListInit:
                break;
            case ExpressionType.MemberAccess:
                break;
            case ExpressionType.MemberInit:
                break;
            case ExpressionType.Modulo:
                break;
            case ExpressionType.Multiply:
                break;
            case ExpressionType.MultiplyChecked:
                break;
            case ExpressionType.Negate:
                break;
            case ExpressionType.UnaryPlus:
                break;
            case ExpressionType.NegateChecked:
                break;
            case ExpressionType.New:
                break;
            case ExpressionType.NewArrayInit:
                break;
            case ExpressionType.NewArrayBounds:
                break;
            case ExpressionType.Not:
                return "NOT ( " + Compile(expression.Operand) + ")";
            case ExpressionType.NotEqual:
                break;
            case ExpressionType.Or:
                break;
            case ExpressionType.OrElse:
                break;
            case ExpressionType.Parameter:
                break;
            case ExpressionType.Power:
                break;
            case ExpressionType.Quote:
                break;
            case ExpressionType.RightShift:
                break;
            case ExpressionType.Subtract:
                break;
            case ExpressionType.SubtractChecked:
                break;
            case ExpressionType.TypeAs:
                break;
            case ExpressionType.TypeIs:
                break;
            case ExpressionType.Assign:
                break;
            case ExpressionType.Block:
                break;
            case ExpressionType.DebugInfo:
                break;
            case ExpressionType.Decrement:
                break;
            case ExpressionType.Dynamic:
                break;
            case ExpressionType.Default:
                break;
            case ExpressionType.Extension:
                break;
            case ExpressionType.Goto:
                break;
            case ExpressionType.Increment:
                break;
            case ExpressionType.Index:
                break;
            case ExpressionType.Label:
                break;
            case ExpressionType.RuntimeVariables:
                break;
            case ExpressionType.Loop:
                break;
            case ExpressionType.Switch:
                break;
            case ExpressionType.Throw:
                break;
            case ExpressionType.Try:
                break;
            case ExpressionType.Unbox:
                break;
            case ExpressionType.AddAssign:
                break;
            case ExpressionType.AndAssign:
                break;
            case ExpressionType.DivideAssign:
                break;
            case ExpressionType.ExclusiveOrAssign:
                break;
            case ExpressionType.LeftShiftAssign:
                break;
            case ExpressionType.ModuloAssign:
                break;
            case ExpressionType.MultiplyAssign:
                break;
            case ExpressionType.OrAssign:
                break;
            case ExpressionType.PowerAssign:
                break;
            case ExpressionType.RightShiftAssign:
                break;
            case ExpressionType.SubtractAssign:
                break;
            case ExpressionType.AddAssignChecked:
                break;
            case ExpressionType.MultiplyAssignChecked:
                break;
            case ExpressionType.SubtractAssignChecked:
                break;
            case ExpressionType.PreIncrementAssign:
                break;
            case ExpressionType.PreDecrementAssign:
                break;
            case ExpressionType.PostIncrementAssign:
                break;
            case ExpressionType.PostDecrementAssign:
                break;
            case ExpressionType.TypeEqual:
                break;
            case ExpressionType.OnesComplement:
                break;
            case ExpressionType.IsTrue:
                break;
            case ExpressionType.IsFalse:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        throw new ArgumentOutOfRangeException();
    }

    private static string Compile(MethodCallExpression expression)
    {
        var containsMethods = new[]
        {
            "Boolean Contains[Int32](System.Collections.Generic.IEnumerable`1[System.Int32], Int32)",
            "Boolean Contains[String](System.Collections.Generic.IEnumerable`1[System.String], System.String)",
            "Boolean Contains[Double](System.Collections.Generic.IEnumerable`1[System.Double], Double)",
            "Boolean Contains[Single](System.Collections.Generic.IEnumerable`1[System.Single], Single)",
            "Boolean Contains[Byte](System.Collections.Generic.IEnumerable`1[System.Byte], Byte)",
            "Boolean Contains[Int16](System.Collections.Generic.IEnumerable`1[System.Int16], Int16)",
            "Boolean Contains[Int64](System.Collections.Generic.IEnumerable`1[System.Int64], Int64)",
            "Boolean Contains[Decimal](System.Collections.Generic.IEnumerable`1[System.Decimal], System.Decimal)",
        };
       
        const string sqrtMethod = "Double Sqrt(Double)";
            
        var methodStr = expression.Method.ToString();
        if (containsMethods.Contains(methodStr))
        {
            return Compile(expression.Arguments[1]) + " IN (" + Compile(expression.Arguments[0]) + ")";
        }
        if (methodStr == sqrtMethod)
        {
            return " SQRT(" + Compile(expression.Arguments[0]) + ")";
        }

        throw null;
    }

    private static string Compile(BinaryExpression expression)
    {
        switch (expression.NodeType)
        {
            case ExpressionType.Add:
                return Compile(expression.Left) + "+" + Compile(expression.Right);
            case ExpressionType.AddChecked:
                break;
            case ExpressionType.And:
                return Compile(expression.Left) + " AND " + Compile(expression.Right);
            case ExpressionType.AndAlso:
                break;
            case ExpressionType.ArrayLength:
                break;
            case ExpressionType.ArrayIndex:
                break;
            case ExpressionType.Call:
                break;
            case ExpressionType.Coalesce:
                break;
            case ExpressionType.Conditional:
                break;
            case ExpressionType.Constant:
                break;
            case ExpressionType.Convert:
                break;
            case ExpressionType.ConvertChecked:
                break;
            case ExpressionType.Divide:
                return Compile(expression.Left) + " / " + Compile(expression.Right);
            case ExpressionType.Equal:
                return CompileBinary(expression.Left, expression.Right,"=");
            case ExpressionType.ExclusiveOr:
                break;
            case ExpressionType.GreaterThan:
                return CompileBinary(expression.Left, expression.Right,">");
            case ExpressionType.GreaterThanOrEqual:
                return CompileBinary(expression.Left, expression.Right, ">=");
            case ExpressionType.Invoke:
                break;
            case ExpressionType.Lambda:
                break;
            case ExpressionType.LeftShift:
                break;
            case ExpressionType.LessThan:
                return CompileBinary(expression.Left, expression.Right, "<");
            case ExpressionType.LessThanOrEqual:
                return CompileBinary(expression.Left, expression.Right, "<=");
            case ExpressionType.ListInit:
                break;
            case ExpressionType.MemberAccess:
                break;
            case ExpressionType.MemberInit:
                break;
            case ExpressionType.Modulo:
                break;
            case ExpressionType.Multiply:
                break;
            case ExpressionType.MultiplyChecked:
                break;
            case ExpressionType.Negate:
                break;
            case ExpressionType.UnaryPlus:
                break;
            case ExpressionType.NegateChecked:
                break;
            case ExpressionType.New:
                break;
            case ExpressionType.NewArrayInit:
                break;
            case ExpressionType.NewArrayBounds:
                break;
            case ExpressionType.Not:
                break;
            case ExpressionType.NotEqual:
                return CompileBinary(expression.Left, expression.Right, "<>");
            case ExpressionType.Or:
                return Compile(expression.Left) + " OR " + Compile(expression.Right);
            case ExpressionType.OrElse:
                break;
            case ExpressionType.Parameter:
                break;
            case ExpressionType.Power:
                break;
            case ExpressionType.Quote:
                break;
            case ExpressionType.RightShift:
                break;
            case ExpressionType.Subtract:
                break;
            case ExpressionType.SubtractChecked:
                break;
            case ExpressionType.TypeAs:
                break;
            case ExpressionType.TypeIs:
                break;
            case ExpressionType.Assign:
                break;
            case ExpressionType.Block:
                break;
            case ExpressionType.DebugInfo:
                break;
            case ExpressionType.Decrement:
                break;
            case ExpressionType.Dynamic:
                break;
            case ExpressionType.Default:
                break;
            case ExpressionType.Extension:
                break;
            case ExpressionType.Goto:
                break;
            case ExpressionType.Increment:
                break;
            case ExpressionType.Index:
                break;
            case ExpressionType.Label:
                break;
            case ExpressionType.RuntimeVariables:
                break;
            case ExpressionType.Loop:
                break;
            case ExpressionType.Switch:
                break;
            case ExpressionType.Throw:
                break;
            case ExpressionType.Try:
                break;
            case ExpressionType.Unbox:
                break;
            case ExpressionType.AddAssign:
                break;
            case ExpressionType.AndAssign:
                break;
            case ExpressionType.DivideAssign:
                break;
            case ExpressionType.ExclusiveOrAssign:
                break;
            case ExpressionType.LeftShiftAssign:
                break;
            case ExpressionType.ModuloAssign:
                break;
            case ExpressionType.MultiplyAssign:
                break;
            case ExpressionType.OrAssign:
                break;
            case ExpressionType.PowerAssign:
                break;
            case ExpressionType.RightShiftAssign:
                break;
            case ExpressionType.SubtractAssign:
                break;
            case ExpressionType.AddAssignChecked:
                break;
            case ExpressionType.MultiplyAssignChecked:
                break;
            case ExpressionType.SubtractAssignChecked:
                break;
            case ExpressionType.PreIncrementAssign:
                break;
            case ExpressionType.PreDecrementAssign:
                break;
            case ExpressionType.PostIncrementAssign:
                break;
            case ExpressionType.PostDecrementAssign:
                break;
            case ExpressionType.TypeEqual:
                break;
            case ExpressionType.OnesComplement:
                break;
            case ExpressionType.IsTrue:
                break;
            case ExpressionType.IsFalse:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        throw new NotImplementedException();
    }

    private static string CompileBinary(Expression left, Expression right,string @operator)
    {
        @operator = $" {@operator} ";
        if (left.NodeType == ExpressionType.Convert && right.NodeType==ExpressionType.Constant)
        {
            var memberExpression = ((MemberExpression) ((UnaryExpression) left)?.Operand);
            var leftPropertyType = ((PropertyInfo) memberExpression?.Member)
                ?.PropertyType;
            var constantExpression = ((ConstantExpression) right);
            var rightType = constantExpression.Type;
            //
            if (leftPropertyType == typeof(long))
            {
                return Compile(memberExpression) + @operator + Compile(right);
            }
            if (leftPropertyType == typeof(int))
            {
                if (rightType == typeof(long))
                {
                    var rightValue = (long)constantExpression.Value;
                    if (rightValue is < int.MaxValue and > int.MinValue)
                    {
                        return Compile(memberExpression) + @operator + Compile(right);
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                return Compile(memberExpression) + @operator + Compile(right);
            }

            if (leftPropertyType == typeof(short))
            {
                if (rightType == typeof(int))
                {
                    var rightValue = (int)constantExpression.Value;
                    if (rightValue is < short.MaxValue and > short.MinValue)
                    {
                        return Compile(memberExpression) + @operator + Compile(right);
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                if (rightType == typeof(long))
                {
                    var rightValue = (long)constantExpression.Value;
                    if (rightValue is < short.MaxValue and > short.MinValue)
                    {
                        return Compile(memberExpression) + @operator + Compile(right);
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                ;
            }


            if (leftPropertyType == typeof(byte))
            {
                if (rightType == typeof(short))
                {
                    var rightValue = (short)constantExpression.Value;
                    if (rightValue is < byte.MaxValue and > byte.MinValue)
                    {
                        return Compile(memberExpression) + @operator + Compile(right);
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                if (rightType == typeof(int))
                {
                    var rightValue = (int)constantExpression.Value;
                    if (rightValue is < byte.MaxValue and > byte.MinValue)
                    {
                        return Compile(memberExpression) + @operator + Compile(right);
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                if (rightType == typeof(long))
                {
                    var rightValue = (long)constantExpression.Value;
                    if (rightValue is < byte.MaxValue and > byte.MinValue)
                    {
                        return Compile(memberExpression) + @operator + Compile(right);
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                ;
            }
            ;
        }

        if (left.NodeType == ExpressionType.Convert && right.NodeType == ExpressionType.Convert)
        {
            var leftMemberExpression = ((UnaryExpression)left)?.Operand;
            var rightMemberExpression = ((UnaryExpression)right)?.Operand;
            return Compile(leftMemberExpression) + @operator + Compile(rightMemberExpression);
            ;
        }

        ;
        return Compile(left) + @operator + Compile(right);
    }

    private static string Compile(Expression expression)
    {
        switch (expression.NodeType)
        {
            case ExpressionType.Add:
                break;
            case ExpressionType.AddChecked:
                break;
            case ExpressionType.And:
                break;
            case ExpressionType.AndAlso:
                break;
            case ExpressionType.ArrayLength:
                break;
            case ExpressionType.ArrayIndex:
                break;
            case ExpressionType.Call:
                if(expression is MethodCallExpression callExpression)
                { return Compile(callExpression); }
                break;
            case ExpressionType.Coalesce:
                break;
            case ExpressionType.Conditional:
                break;
            case ExpressionType.Constant:
                var constantExpression = (expression as ConstantExpression);
                return ToSqlString(constantExpression?.Value);
            case ExpressionType.Convert:
                if (expression is UnaryExpression unaryExpression)
                    return CompileCast(unaryExpression.Operand, unaryExpression.Type);
                break;
            case ExpressionType.ConvertChecked:
                break;
            case ExpressionType.Divide:
                if (expression is BinaryExpression binaryExpressionDivide)
                    return "(" + Compile(binaryExpressionDivide.Left) + ") / (" + Compile(binaryExpressionDivide.Right) + ")";
                break;
            case ExpressionType.Equal:
                break;
            case ExpressionType.ExclusiveOr:
                break;
            case ExpressionType.GreaterThan:
                break;
            case ExpressionType.GreaterThanOrEqual:
                break;
            case ExpressionType.Invoke:
                break;
            case ExpressionType.Lambda:
                break;
            case ExpressionType.LeftShift:
                break;
            case ExpressionType.LessThan:
                break;
            case ExpressionType.LessThanOrEqual:
                break;
            case ExpressionType.ListInit:
                break;
            case ExpressionType.MemberAccess:
                if (expression is MemberExpression memberExpression)
                {
                    try
                    {
                        var currentValue = Expression.Lambda(memberExpression).Compile().DynamicInvoke();
                        return Compile(currentValue);
                    }
                    catch
                    {
                    }

                    return Query.Settings.EscapeStrategy.Escape(memberExpression.Member.Name);
                }
                break;
            case ExpressionType.MemberInit:
                break;
            case ExpressionType.Modulo:
                break;
            case ExpressionType.Multiply:
                if (expression is BinaryExpression binaryExpression)
                    return "(" + Compile(binaryExpression.Left) + ") * (" + Compile(binaryExpression.Right) + ")";
                break;
            case ExpressionType.MultiplyChecked:
                break;
            case ExpressionType.Negate:
                break;
            case ExpressionType.UnaryPlus:
                break;
            case ExpressionType.NegateChecked:
                break;
            case ExpressionType.New:
                break;
            case ExpressionType.NewArrayInit:
                break;
            case ExpressionType.NewArrayBounds:
                break;
            case ExpressionType.Not:
                break;
            case ExpressionType.NotEqual:
                break;
            case ExpressionType.Or:
                break;
            case ExpressionType.OrElse:
                break;
            case ExpressionType.Parameter:
                break;
            case ExpressionType.Power:
                break;
            case ExpressionType.Quote:
                break;
            case ExpressionType.RightShift:
                break;
            case ExpressionType.Subtract:
                break;
            case ExpressionType.SubtractChecked:
                break;
            case ExpressionType.TypeAs:
                break;
            case ExpressionType.TypeIs:
                break;
            case ExpressionType.Assign:
                break;
            case ExpressionType.Block:
                break;
            case ExpressionType.DebugInfo:
                break;
            case ExpressionType.Decrement:
                break;
            case ExpressionType.Dynamic:
                break;
            case ExpressionType.Default:
                break;
            case ExpressionType.Extension:
                break;
            case ExpressionType.Goto:
                break;
            case ExpressionType.Increment:
                break;
            case ExpressionType.Index:
                break;
            case ExpressionType.Label:
                break;
            case ExpressionType.RuntimeVariables:
                break;
            case ExpressionType.Loop:
                break;
            case ExpressionType.Switch:
                break;
            case ExpressionType.Throw:
                break;
            case ExpressionType.Try:
                break;
            case ExpressionType.Unbox:
                break;
            case ExpressionType.AddAssign:
                break;
            case ExpressionType.AndAssign:
                break;
            case ExpressionType.DivideAssign:
                break;
            case ExpressionType.ExclusiveOrAssign:
                break;
            case ExpressionType.LeftShiftAssign:
                break;
            case ExpressionType.ModuloAssign:
                break;
            case ExpressionType.MultiplyAssign:
                break;
            case ExpressionType.OrAssign:
                break;
            case ExpressionType.PowerAssign:
                break;
            case ExpressionType.RightShiftAssign:
                break;
            case ExpressionType.SubtractAssign:
                break;
            case ExpressionType.AddAssignChecked:
                break;
            case ExpressionType.MultiplyAssignChecked:
                break;
            case ExpressionType.SubtractAssignChecked:
                break;
            case ExpressionType.PreIncrementAssign:
                break;
            case ExpressionType.PreDecrementAssign:
                break;
            case ExpressionType.PostIncrementAssign:
                break;
            case ExpressionType.PostDecrementAssign:
                break;
            case ExpressionType.TypeEqual:
                break;
            case ExpressionType.OnesComplement:
                break;
            case ExpressionType.IsTrue:
                break;
            case ExpressionType.IsFalse:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
            
        throw new NotImplementedException();
    }

    private static string CompileCast(Expression expression, Type type)
    {
        if (expression.NodeType == ExpressionType.MemberAccess)
        {
            var memberExpression = (MemberExpression) expression;
            if(memberExpression.Member is PropertyInfo propertyInfo)
            {
                if (propertyInfo.PropertyType == type)
                {
                    return Compile(memberExpression);
                }
                else
                {
                    ;
                }
            }
            else if (memberExpression.Member is FieldInfo fieldInfo)
            {
                if (fieldInfo.FieldType == type)
                {
                    return Compile(memberExpression);
                }
                else
                {
                    ;
                }
            }
            else
            {
                ;
            }
        }

        return "CAST(" + Compile(expression) + " AS " + Compile(type) + ")";
    }

    private static string ToSqlString(object value)
    {
        if (value == null) return "NULL";
        AbstractSqlLiteral literal = null;
        if (value is int x1) literal = AbstractSqlLiteral.From(x1);
        if (value is long x2) literal = AbstractSqlLiteral.From(x2);
        if (value is short x3) literal = AbstractSqlLiteral.From(x3);
        if (value is double x4) literal = AbstractSqlLiteral.From(x4);
        if (value is decimal x5) literal = AbstractSqlLiteral.From(x5);
        if (value is float x6) literal = AbstractSqlLiteral.From(x6);
        if (value is char x7) literal = AbstractSqlLiteral.From(x7);
        if (value is string x8) literal = AbstractSqlLiteral.From(x8);
        if (value is byte x9) literal = AbstractSqlLiteral.From(x9);
        if (value is Guid x10) literal = AbstractSqlLiteral.From(x10);

        if (literal == null)
        {
            throw null;
        }

        return literal.ToSqlString();
    }

    private static string Compile(Type value)
    {
        if (value.ToString() == "System.Double")
        {
            return "double";
        }
        if (value.ToString() == "System.Int64")
        {
            return "bigint";
        }
        if (value.ToString() == "System.Int32")
        {
            return "int";
        }
        if (value.ToString() == "System.Int16")
        {
            return "smallint";
        }
        if (value.ToString() == "System.Single")
        {
            return "float";
        }
        if (value.ToString() == "System.Byte")
        {
            return "tinyint";
        }
        if (value.ToString() == "System.Decimal")
        {
            return "decimal(19,4)";
        }

        throw null;
    }
    private static string Compile(object value)
    {
        if (value is int[] intArr)
        {
            return string.Join(",", intArr);
        }
        if (value is long[] longArr)
        {
            return string.Join(",", longArr);
        }
        if (value is short[] shortArr)
        {
            return string.Join(",", shortArr);
        }
        if (value is byte[] byteArr)
        {
            return string.Join(",", byteArr);
        }
        if (value is double[] doubleArr)
        {
            return string.Join(",", doubleArr);
        }
        if (value is decimal[] decimalArr)
        {
            return string.Join(",", decimalArr);
        }
        if (value is float[] floatArr)
        {
            return string.Join(",", floatArr);
        }
        if (value is string[] stringArr)
        {
            return string.Join(",", stringArr.Select(x=>$"N'{x.Replace("'","''")}'"));
        }
        if (value is Guid g)
        {
            return $"'{g}'";
        }
        if (value is byte b)
        {
            return $"{b}";
        }
        ;

        throw null;
    }
}