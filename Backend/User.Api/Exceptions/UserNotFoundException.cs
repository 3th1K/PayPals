﻿using Microsoft.AspNetCore.Connections.Features;

public class UserNotFoundException : Exception
{
    public readonly string ErrorCode;
    public readonly string ErrorMessage;
    public UserNotFoundException() : base()
    {
        ErrorCode = "ERR_USER_NOT_FOUND";
        ErrorMessage = "User Is Not Found";
    }
}