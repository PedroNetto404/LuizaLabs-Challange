using System;
using FavoriteProducts.Domain.Core.Results;
using MediatR;

namespace FavoriteProducts.UseCases.Abstractions;

public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, Result<TResult>>
    where TQuery : IQuery<TResult>; 