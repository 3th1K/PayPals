﻿using LanguageExt.Common;
using MediatR;
using UserService.Api.Models;

namespace UserService.Api.Queries
{
    public class GetUserDetailsByIdQuery : IRequest<Result<User>>
    {
        public readonly int Id;
        public GetUserDetailsByIdQuery(int id)
        {
            Id = id;
        }
    }
}