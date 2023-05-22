import React from "react";

export function Page(props) {
    const url = window.location.origin;
    return (
        <nav className="navbar navbar-default">
            <div className="container-fluid">
                <div className="navbar-header">
                    <button type="button" className="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
                        <span className="sr-only">Toggle navigation</span>
                        <span className="icon-bar"></span>
                        <span className="icon-bar"></span>
                        <span className="icon-bar"></span>
                    </button>
                </div>
                <div className="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                    <ul className="nav navbar-nav">
                        <li className="active"><a href={url + "/Appointment/Index"}>Appointment <span className="sr-only">(current)</span></a></li>
                        <li><a href={url + "/Odontologist/Index"}>Odontologist</a></li>
                    </ul>
                    {manageUser(props)}
                </div>
            </div>
        </nav>
    );
}

function manageUser(props){
    if(props.logged){
        return loggedUser(props);
    }
    return enterUser(props);
}

function enterUser(props) {
    const url = window.location.origin;
    return (
        <ul className="nav navbar-nav navbar-right">
            <li className="user">
                <a href="/" className="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Enter<span className="caret"></span></a>
                <ul className="dropdown-menu">
                    <li><a href={url + "/User/Register"}>Register</a></li>
                    <li><a href={url + "/User/Login"}>Log in</a></li>
                </ul>
            </li>
        </ul>
    );
}

function loggedUser(props) {
    return (
        <ul className="nav navbar-nav navbar-right">
            <li className="user">
                <a href="/" className="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Manage user<span className="caret"></span></a>
                <ul className="dropdown-menu">
                    <li><a href="/">My profile</a></li>
                    <li><a href="/">Change password</a></li>
                    <li role="separator" className="divider"></li>
                    <li><a onClick={props.logOff} href="/">Log off</a></li>
                </ul>
            </li>
        </ul>
    );
}