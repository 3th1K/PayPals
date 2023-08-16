﻿using LanguageExt.Common;
using MediatR;
using UserService.Api.Models;

namespace UserService.Api.Queries
{
    public class DeleteUserQuery:IRequest<Result<User>>
    {
        public readonly int Id;
        public DeleteUserQuery(int id)
        {
            Id = id;
        }
    }
}