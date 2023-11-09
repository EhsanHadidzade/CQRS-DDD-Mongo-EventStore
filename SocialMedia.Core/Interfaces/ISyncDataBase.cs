using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces;

public interface ISyncDataBase
{
    Task SaveAsync<TQueryModel>(TQueryModel queryModel, Expression<Func<TQueryModel, bool>> upsertFilter) where TQueryModel : IQueryModel;

    Task DeleteAsync<TQueryModel>(Expression<Func<TQueryModel, bool>> deleteFilter) where TQueryModel : IQueryModel;
}