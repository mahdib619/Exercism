using System;

public static class Permissions
{
    public static Permission Default(AccountType accountType) => accountType switch
    {
        AccountType.Guest => Permission.Read,
        AccountType.User => Permission.Read | Permission.Write,
        AccountType.Moderator => Permission.All,
        _ => Permission.None
    };

    public static Permission Grant(Permission current, Permission grant) => current | grant;

    public static Permission Revoke(Permission current, Permission revoke) => (current ^ revoke) & current;

    public static bool Check(Permission current, Permission check) => current.HasFlag(check);
}

public enum AccountType
{
    Guest,
    User,
    Moderator
}

[Flags]
public enum Permission : byte
{
    Read = 0b00000001,
    Write = 0b00000010,
    Delete = 0b00000100,
    All = 0b00000111,
    None = 0b00000000
}