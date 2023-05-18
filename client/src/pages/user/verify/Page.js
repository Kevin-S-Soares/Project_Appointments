import React from "react";

export function Page(props){
    return(
        <form>
            <label>Token:</label>
            <input type="text" id="token" onChange={props.changeValue} value={props.token} />
            <br />
            <input type="submit" value="Submit" onClick={props.submit} />
            <a href={window.location.origin + "/User/Register"}>Register</a>
        </form>
    );
}