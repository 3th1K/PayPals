﻿using Common.DTOs.UserDTOs;
using MediatR;

namespace UserService.Api.Queries
{
    public class GetUserDetailsByIdQuery : IRequest<UserDetailsResponse>
    {
        public readonly int Id;
        public GetUserDetailsByIdQuery(int id)
        {
            Id = id;
        }
    }
}
