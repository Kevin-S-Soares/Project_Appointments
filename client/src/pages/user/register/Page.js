import React from "react";
import "./custom.css";

export function Page(props) {
    return (
        <div className="col-md-offset-4 col-md-4">
            <br />
            <div className="panel panel-default">
                <ul className="nav nav-tabs nav-justified" role="tablist">
                    <li role="presentation"><a href="/User/Login">Log in</a></li>
                    <li role="presentation" className="active right"><a href="/User/Register">Register</a></li>
                </ul>
                <form className="panel-body">
                    <div className="form-group">
                        <label htmlFor="email">Email:</label>
                        <input autoComplete="off" className="form-control" type="email" id="email" onChange={props.changeEmail} />
                    </div>
                    {incorrectEmail(props)}
                    <div className="form-group">
                        <label htmlFor="password">Password:</label>
                        <input autoComplete="off" className="form-control" type="password" id="password" onChange={props.changePassword} />
                    </div>
                    {passwordIsWeak(props)}
                    <div className="form-group">
                        <label htmlFor="confirm_password">Confirm password:</label>
                        <input autoComplete="off" className="form-control" type="password" id="confirm_password" onChange={props.changeConfirmPassword} />
                    </div>
                    {passwordsDoNotMatch(props)}
                    {errorMessage(props)}
                    <input className="btn btn-default" disabled={props.isSubmitButtonDisabled} type="submit" onClick={props.submit} value="Submit" />
                </form>
            </div>
        </div>
    );
}

function incorrectEmail(props) {
    if (!props.isEmailValid) {
        return (
            <p className="text-danger">Email is incorrect!</p>
        );
    }
    return (
        <></>
    );
}

function passwordIsWeak(props) {
    if (!props.isPasswordValid) {
        return (
            <p className="text-danger">Password must contain at least one letter, one number and one special character!</p>
        );
    }
    return (
        <></>
    );
}

function passwordsDoNotMatch(props) {
    if (!props.isConfirmPasswordValid) {
        return (
            <p className="text-danger">Passwords do not match!</p>
        );
    }
    return (
        <></>
    );
}

function errorMessage(props) {
    if (props.message !== null) {
        return (
            <p className="text-danger">{props.message}!</p>
        );
    }
    return (
        <></>
    )
}