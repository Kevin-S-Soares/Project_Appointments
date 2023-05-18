import React from "react";
import { Login } from "./login/Login";
import { Register } from "./register/Register";
import { Verify } from "./verify/Verify";
import { ForgetPassword } from "./forget-password/ForgetPassword";
import { ResetPassword } from "./reset-password/ResetPassword";

export function LoginUser(props){
    return (
        <Login />
    );
}

export function RegisterUser(props){
    return (
        <Register />
    );
}

export function VerifyUser(props){
    return(
        <Verify />
    );
}

export function ForgetPasswordUser(props){
    return(
        <ForgetPassword />
    );
}

export function ResetPasswordUser(props){
    return(
        <ResetPassword />
    );
}