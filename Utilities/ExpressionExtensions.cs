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
};