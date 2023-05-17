import React from "react";
import { Page } from "./Page";
import { CookieHandler } from "../../../cookies/CookieHandler"

export class StateHandler extends React.Component{
    constructor(props){
        super(props);
        this.odontologist = {
            name: '',
            email: '',
            phone: ''
        }

        this.submit = this.submit.bind(this);
        this.changeValue = this.changeValue.bind(this);
    }

    render(){
        return(
            <Page 
            submit={this.submit}
            changeValue={this.changeValue}
            />
        )
    }

    submit(event){
        event.preventDefault();

        const header = CookieHandler.GetAuthorizedHeader();
        const body = JSON.stringify(this.odontologist);
        const options = {
            method: 'POST',
            headers: header,
            body: body
        }
        
        fetch('/api/Odontologist', options)
        .then(response => response.json())
        .then(json => console.log(json));
    }

    changeValue(event){
        const key = event.target.id;
        const value = event.target.value;
        this.odontologist[key] = value;
    }


}