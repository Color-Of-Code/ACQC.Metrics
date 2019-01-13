using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACQC.Metrics
{
    public enum TokenClass
    {
        Identifier,
        
        K_class,
        K_enum,
        K_struct,
        K_union,

        K_template,

        K_do,
        K_if,
        K_for,
        K_while,
        K_switch,
        K_case,

        K_try,
        K_catch,
        K_throw,

        K_extern,
        K_const,
        K_mutable,
        K_register,
        K_restrict,
        K_volatile,

        Call_cdecl,
        Call_stdcall,
        Call_fastcall,
        Call_thiscall,

        Ellipsis,
        Operation,
        LeftParenthesis,
        RightParenthesis,
        LeftBrace,
        RightBrace,
        Semicolon,
        Colon,

        T_void
    }
}
