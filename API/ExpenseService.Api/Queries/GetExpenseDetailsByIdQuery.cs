﻿using Common.DTOs.ExpenseDTOs;
using Common.Utilities;
using MediatR;

namespace ExpenseService.Api.Queries
{
    public class GetExpenseDetailsByIdQuery : IRequest<ApiResult<ExpenseResponse>>
    {
        public readonly int Id;
        public GetExpenseDetailsByIdQuery(int id) 
        {
            Id = id;
        }
    }
}
