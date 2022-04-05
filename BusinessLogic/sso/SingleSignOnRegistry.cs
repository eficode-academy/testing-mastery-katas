﻿namespace Backend.sso
{
    public interface SingleSignOnRegistry
    {
        SSOToken RegisterNewSession(String userName, String password);
        bool IsValid(SSOToken token);
        void Unregister(SSOToken token);
    }
}
