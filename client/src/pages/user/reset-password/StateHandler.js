import React from "react";
import { Page } from "./Page";

export class StateHandler extends React.Component{
    constructor(props){
        super(props);

        this.resetPassword = {
            token: new URLSearchParams(window.location.search).get('token'),
            password: '',
            confirm_password: ''
        }

        console.log(this.resetPassword)

        this.state = {
            disabled: ''
        }

        this.changeValue = this.changeValue.bind(this);
        this.submit = this.submit.bind(this);
    }

    render(){
        return(
            <Page
            disabled={this.state.disabled}
            changeValue={this.changeValue}
            submit={this.submit}
            />
        );
    }

    changeValue(event){
        const key = event.target.id;
        const value = event.target.value;
        this.resetPassword[key] = value
        const condition = this.resetPassword.confirm_password === this.resetPassword.password? 
        '': 'disabled';
        this.setState({
            disabled: condition,
        });
    }

    submit(event){
        event.preventDefault();

        const header = new Headers({
            'Content-Type' : 'application/json;charset=UTF-8'
        })
        const body = JSON.stringify({
            token: this.resetPassword.token,
            password: this.resetPassword.password
        });
        const options = {
            method: 'POST',
            headers: header,
            body: body
        }

        fetch('/api/User/ResetPassword', options)
        .then(response => {
            if(response.status !== 200){
                throw Error(response.statusText)
            }
            return response.text()
        })
        .then(text => console.log(text))
        .catch(error => console.log(error))
    }
}