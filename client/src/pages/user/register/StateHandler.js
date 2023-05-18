import React from "react";
import { Page } from "./Page";

export class StateHandler extends React.Component{
    constructor(props){
        super(props);

        this.register = {
            email: '',
            password: ''
        }

        this.submit = this.submit.bind(this);
        this.changeValue = this.submit.bind(this);
    }

    render(){
        return(
            <Page
            submit={this.submit}
            changeValue={this.changeValue} 
            />
        );
    }

    changeValue(event){
        const key = event.target.id;
        const value = event.target.value;
        this.register[key] = value;
    }

    submit(event){
        event.preventDefault();

        const body = JSON.stringify(this.register);
        const header = new Headers({
            'Content-Type' : 'application/json;charset=UTF-8'
        })
        const options = {
            method: 'POST',
            headers: header,
            body: body
        }
        
        fetch('/api/User/Register', options)
        .then(response => {
            if(response.status !== 200){
                throw Error(response.statusText)
            }
            return response.json()
        })
        .then()
        .catch(error => console.log(error))
    }
}