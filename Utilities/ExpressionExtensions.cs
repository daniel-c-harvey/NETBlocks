using System.Linq.Expressions;

namespace NetBlocks.Utilities;

public static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var leftVisitor = new ReplaceParameterVisitor(left.Parameters[0], parameter);
        var leftExpr = leftVisitor.Visit(left.Body);
        
        var rightVisitor = new ReplaceParameterVisitor(right.Parameters[0], parameter);
        var rightExpr = rightVisitor.Visit(right.Body);
        
        var combinedExpr = Expression.AndAlso(leftExpr, rightExpr);
        return Expression.Lambda<Func<T, bool>>(combinedExpr, parameter);
    }
    
    public static Expression<Func<T, bool>> Or<T>(
        this Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var leftVisitor = new ReplaceParameterVisitor(left.Parameters[0], parameter);
        var leftExpr = leftVisitor.Visit(left.Body);
        
        var rightVisitor = new ReplaceParameterVisitor(right.Parameters[0], parameter);
        var rightExpr = rightVisitor.Visit(right.Body);
        
        var combinedExpr = Expression.OrElse(leftExpr, rightExpr);
        return Expression.Lambda<Func<T, bool>>(combinedExpr, parameter);
    }
    
    private class ReplaceParameterVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _oldParameter;
        private readonly ParameterExpression _newParameter;
        
        public ReplaceParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
        {
            _oldParameter = oldParameter;
            _newParameter = newParameter;
        }
        
        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == _oldParameter ? _newParameter : base.VisitParameter(node);
        }
    }
    
    public static Expression<Func<T1, T2, bool>> And<T1, T2>(
        this Expression<Func<T1, T2, bool>> left,
        Expression<Func<T1, T2, bool>> right)
    {
        var parameter1 = Expression.Parameter(typeof(T1), "x");
        var parameter2 = Expression.Parameter(typeof(T2), "y");
        
        var leftVisitor = new ReplaceParametersVisitor(
            new[] { left.Parameters[0], left.Parameters[1] }, 
            new[] { parameter1, parameter2 });
        var leftExpr = leftVisitor.Visit(left.Body);
        
        var rightVisitor = new ReplaceParametersVisitor(
            new[] { right.Parameters[0], right.Parameters[1] }, 
            new[] { parameter1, parameter2 });
        var rightExpr = rightVisitor.Visit(right.Body);
        
        var combinedExpr = Expression.AndAlso(leftExpr, rightExpr);
        return Expression.Lambda<Func<T1, T2, bool>>(combinedExpr, parameter1, parameter2);
    }

    public static Expression<Func<T1, T2, bool>> Or<T1, T2>(
        this Expression<Func<T1, T2, bool>> left,
        Expression<Func<T1, T2, bool>> right)
    {
        var parameter1 = Expression.Parameter(typeof(T1), "x");
        var parameter2 = Expression.Parameter(typeof(T2), "y");
        
        var leftVisitor = new ReplaceParametersVisitor(
            new[] { left.Parameters[0], left.Parameters[1] }, 
            new[] { parameter1, parameter2 });
        var leftExpr = leftVisitor.Visit(left.Body);
        
        var rightVisitor = new ReplaceParametersVisitor(
            new[] { right.Parameters[0], right.Parameters[1] }, 
            new[] { parameter1, parameter2 });
        var rightExpr = rightVisitor.Visit(right.Body);
        
        var combinedExpr = Expression.OrElse(leftExpr, rightExpr);
        return Expression.Lambda<Func<T1, T2, bool>>(combinedExpr, parameter1, parameter2);
    }
    
    public static Expression<Func<T1, T2, T3, bool>> And<T1, T2, T3>(
        this Expression<Func<T1, T2, T3, bool>> left,
        Expression<Func<T1, T2, T3, bool>> right)
    {
        var parameter1 = Expression.Parameter(typeof(T1), "x");
        var parameter2 = Expression.Parameter(typeof(T2), "y");
        var parameter3 = Expression.Parameter(typeof(T2), "z");
        
        var leftVisitor = new ReplaceParametersVisitor(
            new[] { left.Parameters[0], left.Parameters[1], left.Parameters[2] }, 
            new[] { parameter1, parameter2, parameter3 });
        var leftExpr = leftVisitor.Visit(left.Body);
        
        var rightVisitor = new ReplaceParametersVisitor(
            new[] { right.Parameters[0], right.Parameters[1], right.Parameters[2] }, 
            new[] { parameter1, parameter2, parameter3 });
        var rightExpr = rightVisitor.Visit(right.Body);
        
        var combinedExpr = Expression.AndAlso(leftExpr, rightExpr);
        return Expression.Lambda<Func<T1, T2, T3, bool>>(combinedExpr, parameter1, parameter2,  parameter3);
    }

    public static Expression<Func<T1, T2, T3, bool>> Or<T1, T2, T3>(
        this Expression<Func<T1, T2, T3, bool>> left,
        Expression<Func<T1, T2, T3, bool>> right)
    {
        var parameter1 = Expression.Parameter(typeof(T1), "x");
        var parameter2 = Expression.Parameter(typeof(T2), "y");
        var parameter3 = Expression.Parameter(typeof(T2), "z");
        
        var leftVisitor = new ReplaceParametersVisitor(
            new[] { left.Parameters[0], left.Parameters[1], left.Parameters[2] }, 
            new[] { parameter1, parameter2, parameter3 });
        var leftExpr = leftVisitor.Visit(left.Body);
        
        var rightVisitor = new ReplaceParametersVisitor(
            new[] { right.Parameters[0], right.Parameters[1], right.Parameters[2] }, 
            new[] { parameter1, parameter2, parameter3 });
        var rightExpr = rightVisitor.Visit(right.Body);
        
        var combinedExpr = Expression.OrElse(leftExpr, rightExpr);
        return Expression.Lambda<Func<T1, T2, T3, bool>>(combinedExpr, parameter1, parameter2,  parameter3);
    }

    public class ReplaceParametersVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression[] _oldParameters;
        private readonly ParameterExpression[] _newParameters;
        
        public ReplaceParametersVisitor(ParameterExpression[] oldParameters, ParameterExpression[] newParameters)
        {
            _oldParameters = oldParameters;
            _newParameters = newParameters;
        }
        
        protected override Expression VisitParameter(ParameterExpression node)
        {
            for (int i = 0; i < _oldParameters.Length; i++)
            {
                if (node == _oldParameters[i])
                    return _newParameters[i];
            }
            
            return base.VisitParameter(node);
        }
    }
};