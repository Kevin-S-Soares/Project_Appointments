import React from "react";
import { Page } from "./Page";

export class StateHandler extends React.Component{
    constructor(props){
        super(props);

        this.token = new URLSearchParams(window.location.search).get('token');
        
        this.state = {
            token: this.token
        }

        this.submit = this.submit.bind(this);
        this.changeValue = this.changeValue.bind(this);
    }

    render(){
        return(
            <Page
            token={this.state.token}
            submit={this.submit}
            changeValue={this.changeValue}
            />
        );
    }

    changeValue(event){
        this.token = event.target.value;
    }

    submit(event){
        event.preventDefault();

        const header = new Headers({
            'Content-Type' : 'application/json;charset=UTF-8'
        })
        const options = {
            method: 'POST',
            headers: header,
        }

        fetch('/api/User/VerifyToken?token=' + this.token, options)
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