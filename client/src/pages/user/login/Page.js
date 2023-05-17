import React from "react";

export function Page(props) {
    return (
        <form>
            <label>Email:</label>
            <input type="email" id="email"/>
            <br />
            <label>Password:</label>
            <input type="password" id="password" />
            <br />
            <input type="submit" onClick={submitFunction} value="Submit"/>
        </form>
    );
}


async function submitFunction(event) {
    event.preventDefault()
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;
    const header = new Headers({
        'Content-Type': 'application/json;charset=UTF-8' 
    });
    const body = JSON.stringify({
        email: email,
        password: password
    });
    const options = {
        method: 'POST',
        headers: header,
        body: body
    }
    await fetch('/api/User/Login', options)
    .then(response => response.text())
    .then(token => setCookie(token));
}

function setCookie(token){
    const today = new Date();
    const nextMonth = new Date(today.getFullYear(),
     today.getMonth() + 1, today.getDate());
    document.cookie = "jwt=bearer " + token + "; expires=" + nextMonth.toUTCString()  + ";"
}