import React from "react";

export function Page(props){
    return(
        <form>
            <label>Email:</label>
            <input type="email" id="email" onChange={props.changeValue} />
            <br />
            <label>Password:</label>
            <input type="password" id="password" onChange={props.changeValue} />
            <br />
            <input type="submit" onClick={props.submit} value="Submit" />
            <a href={window.location.origin + "/User/Login"}>Login</a>
        </form>
    );
}