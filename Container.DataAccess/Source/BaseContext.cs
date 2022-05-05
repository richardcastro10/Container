using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Numax.DataAccess
{
    public class BaseContext
    {
        private static readonly Dictionary<Thread, List<DbContext>> contextList = new Dictionary<Thread, List<DbContext>>();

        protected BaseContext() { }

        public static TContext GetContext<TContext>() where TContext : DbContext
        {
            lock (contextList)
            {
                return Get<TContext>();
            }
        }

        private static TContext Get<TContext>() where TContext : DbContext
        {
            if (!contextList.ContainsKey(Thread.CurrentThread))
            {
                contextList.Add(Thread.CurrentThread, new List<DbContext>() { });
            }
            TContext contexto = (TContext)contextList[Thread.CurrentThread].FirstOrDefault(c => typeof(TContext).Equals(c.GetType()));
            if (contexto == null)
            {
                return Create<TContext>();
            }
            return contexto;
        }

        private static TContext Create<TContext>() where TContext : DbContext
        {
            TContext contexto;
            contexto = (TContext)Activator.CreateInstance(typeof(TContext));
            contexto.Configuration.EnsureTransactionsForFunctionsAndCommands = false;
            contextList[Thread.CurrentThread].Add(contexto);
            return contexto;
        }

        public static void DisposeContexts()
        {
            lock (contextList)
            {
                List<DbContext> contextos = contextList.ContainsKey(Thread.CurrentThread) ? contextList[Thread.CurrentThread] : new List<DbContext>();
                Parallel.ForEach(contextos, new ParallelOptions() { MaxDegreeOfParallelism = 10 }, c => c.Dispose());

                if (contextList.ContainsKey(Thread.CurrentThread))
                    contextList.Remove(Thread.CurrentThread);
            }
        }

        public static void SaveChanges<TContext>() where TContext : DbContext
        {
            try
            {
                lock (contextList)
                {
                    TContext context = Get<TContext>();
                    context.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                throw DbUpdateExceptionHandle(ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw ObjectDisposedExceptionHandle(ex);
            }
            catch (DbEntityValidationException ex)
            {
                throw DbEntityValidationExceptionHandle(ex);
            }
        }

        private static Exception ObjectDisposedExceptionHandle(ObjectDisposedException ex)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("{name: " + ex.ObjectName + ", message: " + ex.Message + "}");
                return new Exception(builder.ToString(), ex);
            }
            catch (Exception internalEx)
            {
                return new Exception(internalEx.Message, ex);
            }
        }

        private static Exception DbEntityValidationExceptionHandle(DbEntityValidationException ex)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    DbEntityEntry entry = item.Entry;

                    List<String> originalValues = new List<string>();
                    if (entry.State != EntityState.Added && entry.OriginalValues != null)
                    {
                        foreach (var prop in entry.OriginalValues.PropertyNames)
                        {
                            originalValues.Add(prop + ": " + entry.OriginalValues.GetValue<Object>(prop));
                        }
                    }
                    List<String> currentValues = new List<string>();
                    if (entry.State != EntityState.Deleted && entry.CurrentValues != null)
                    {
                        foreach (var prop in entry.CurrentValues.PropertyNames)
                        {
                            currentValues.Add(prop + ": " + entry.CurrentValues.GetValue<Object>(prop));
                        }
                    }
                    builder.AppendLine("{name: " + entry.Entity.GetType().Name + ", state: " + entry.State.ToString() + ", originalValues: {" + String.Join(",", originalValues) + "}, currentValues: {" + String.Join(",", currentValues) + "}}");

                    foreach (DbValidationError subItem in item.ValidationErrors)
                    {
                        builder.AppendLine("{entityTypeName: " + entry.Entity.GetType().Name + ", propertyName: " + subItem.PropertyName + ", message: " + subItem.ErrorMessage + "}");
                    }
                }
                return new Exception(builder.ToString(), ex);
            }
            catch (Exception internalEx)
            {
                return new Exception(internalEx.Message, ex);
            }
        }

        private static Exception DbUpdateExceptionHandle(DbUpdateException ex)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                foreach (var entry in ex.Entries)
                {

                    List<String> originalValues = new List<string>();
                    if (entry.State != EntityState.Added && entry.OriginalValues != null)
                    {
                        foreach (var prop in entry.OriginalValues.PropertyNames)
                        {
                            originalValues.Add(prop + ": " + entry.OriginalValues.GetValue<Object>(prop));
                        }
                    }
                    List<String> currentValues = new List<string>();
                    if (entry.State != EntityState.Deleted && entry.CurrentValues != null)
                    {
                        foreach (var prop in entry.CurrentValues.PropertyNames)
                        {
                            currentValues.Add(prop + ": " + entry.CurrentValues.GetValue<Object>(prop));
                        }
                    }
                    builder.AppendLine("{name: " + entry.Entity.GetType().Name + ", state: " + entry.State.ToString() + ", originalValues: {" + String.Join(",", originalValues) + "}, currentValues: {" + String.Join(",", currentValues) + "}}");
                }

                if (ex.InnerException is System.Data.Entity.Core.UpdateException &&
                    ex.InnerException.InnerException is System.Data.SqlClient.SqlException)
                {
                    var sqlException = (System.Data.SqlClient.SqlException)ex.InnerException.InnerException;
                    var result = new List<ValidationResult>();
                    for (int i = 0; i < sqlException.Errors.Count; i++)
                    {
                        var errorNum = sqlException.Errors[i].Number;
                        string errorText = sqlException.Errors[i].Message;
                        builder.AppendLine("{errorNumber: " + sqlException.Errors[i].Number + ", errorMessage: " + sqlException.Errors[i].Message + "}");
                    }
                }
                return new Exception(builder.ToString(), ex);
            }
            catch (Exception internalEx)
            {
                return new Exception(internalEx.Message, ex);
            }
        }
    }
}
