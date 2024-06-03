﻿using System.Runtime.InteropServices.JavaScript;

/// <summary>
/// Record for the user.
/// </summary>
public class UserInfo
{
    /// <summary>
    /// 
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// E-Mail address of the user.
    /// </summary>
    public required string EmailAddress { get; set; }

    /// <summary>
    /// Date of joining the company.
    /// </summary>
    public DateOnly DateOfJoin { get; set; }

    /// <summary>
    /// Roles of the user.
    /// </summary>
    public string? Roles { get; set; }
}