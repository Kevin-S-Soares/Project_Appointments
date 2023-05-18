import React from "react";
import { Page } from "./Page";
import { CookieHandler } from "../../../cookies/CookieHandler";

export class StateHandler extends React.Component{
    constructor(props){
        super(props);
        this.login = {
            email: '',
            password: ''
        };

        this.changeValue = this.changeValue.bind(this);
        this.submit = this.submit.bind(this);

    }

    render(){
        return (
            <Page 
            submit={this.submit}
            changeValue={this.changeValue}
            />
        );
    }

    changeValue(event){
        const key = event.target.id;
        const value = event.target.value;
        this.login[key] = value;
    }

    submit(event) {
        event.preventDefault()

        const header = new Headers({
            'Content-Type': 'application/json;charset=UTF-8' 
        });
        const body = JSON.stringify(this.login);
        const options = {
            method: 'POST',
            headers: header,
            body: body
        }

        fetch('/api/User/Login', options)
        .then(response => {
            if(response.status !== 200){
                throw Error(response.statusText);
            }
            return response.text();
        })
        .then(token => CookieHandler.SetAuthorizationCookie(token))
        .catch(error => console.log(error));
    }
}