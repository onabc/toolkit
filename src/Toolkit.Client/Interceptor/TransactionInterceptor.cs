using Castle.DynamicProxy;
using System;
using System.Linq;
using System.Reflection;
using System.Transactions;

namespace Toolkit.Client
{
    /// <summary>
    /// 事务 拦截器
    /// </summary>
    public class TransactionInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            MethodInfo methodInfo = invocation.MethodInvocationTarget;
            if (methodInfo == null)
            {
                methodInfo = invocation.Method;
            }

            var transaction = methodInfo.GetCustomAttributes<TransactionCallHandlerAttribute>(true).FirstOrDefault();
            if (transaction != null)
            {
                TransactionOptions transactionOptions = new TransactionOptions();
                //设置事务隔离级别
                transactionOptions.IsolationLevel = transaction.IsolationLevel;
                //设置事务超时时间为60秒
                transactionOptions.Timeout = new TimeSpan(0, 0, transaction.Timeout);
                using (TransactionScope scope = new TransactionScope(transaction.ScopeOption, transactionOptions))
                {
                    try
                    {
                        //实现事务性工作
                        invocation.Proceed();
                        scope.Complete();
                    }
                    catch
                    {
                        // 记录异常
                        throw;
                    }
                }
            }
            else
            {
                // 没有事务时直接执行方法
                invocation.Proceed();
            }
        }
    }
}