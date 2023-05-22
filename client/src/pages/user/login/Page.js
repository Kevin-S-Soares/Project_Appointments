import React from "react";

export function Page(props) {
    return (
        <div className="col-md-offset-4 col-md-4">
            <br />
            <div className="panel panel-default">
                <div className="nav-center">
                    <ul className="nav nav-tabs nav-justified" role="tablist">
                        <li role="presentation" className="active"><a href="/User/Register">Log in</a></li>
                        <li role="presentation"><a href="/User/Register">Register</a></li>
                    </ul>
                </div>
                <form className="panel-body">
                    <div className="form-group">
                        <label htmlFor="email">Email:</label>
                        <input autoComplete="off" className="form-control" type="email" id="email" onChange={props.changeValue} />
                    </div>
                    <div className="form-group">
                        <label htmlFor="password">Password:</label>
                        <input autoComplete="off" className="form-control" type="password" id="password" onChange={props.changeValue} />
                    </div>
                    {errorMessage(props)}
                    <input className="btn btn-default" type="submit" onClick={props.submit} value="Submit" />
                    <a style={{marginLeft: 15}}href={window.location.origin + "/User/ForgetPassword"}>Forget password?</a>
                </form>
            </div>
        </div>
    );
}

function errorMessage(props){
    if(props.message !== null){
        return(
            <p className="text-danger">{props.message}!</p>
        );
    }
    return (
        <></>
    );
}