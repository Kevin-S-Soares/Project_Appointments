import React from "react";

export function Page(props){
    return(
        <form>
            <label>New password:</label>
            <input type="password" id="password" onChange={props.changeValue} />
            <br />
            <label>Confirm new password:</label>
            <input type="password" id="confirm_password" onChange={props.changeValue} />
            <br />
            {matchPassword(props)}
            <input type="submit" value="Submit" onClick={props.submit} disabled={props.disabled}/>
        </form>
    );
}

function matchPassword(props){
    if(props.disabled === ''){
        return (
        <></>
        );
    }
    return (
        <div>
            <label>Passwords do not match</label>
            <br />
        </div>
    );
}