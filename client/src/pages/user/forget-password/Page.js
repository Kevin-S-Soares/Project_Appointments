import React from "react";

export function Page(props){
    return (
        <form>
            <label>Email:</label>
            <input type="email" id="email" onChange={props.changeValue} />
            <br />
            <input type="submit" value="Submit" onClick={props.submit} />
            <a href={window.location.origin + "/User/Register"}>Return</a>
        </form>
    );
}