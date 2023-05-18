import React from "react";
import { LoginUser } from "./login/LoginUser";
import { RegisterUser } from "./register/RegisterUser";
import { VerifyUser } from "./verify/VerifyUser";
import { ForgetPasswordUser } from "./forget-password/ForgetPasswordUser";
import { ResetPasswordUser } from "./reset-password/ResetPasswordUser";

export function Login(props){
    return (
        <LoginUser />
    );
}

export function Register(props){
    return (
        <RegisterUser />
    );
}

export function Verify(props){
    return(
        <VerifyUser />
    );
}

export function ForgetPassword(props){
    return(
        <ForgetPasswordUser />
    );
}

export function ResetPassword(props){
    return(
        <ResetPasswordUser />
    );
}