import React from "react";
import { Page } from "./Page";

export class StateHandler extends React.Component{
    constructor(props){
        super(props);

        this.email = '';

        this.submit = this.submit.bind(this);
        this.changeValue = this.changeValue.bind(this);
    }

    render(){
        return(
            <Page
            changeValue={this.changeValue}
            submit={this.submit} 
            />
        );
    }

    changeValue(event){
        this.email = event.target.value;
    }

    submit(event){
        event.preventDefault()

        const header = new Headers({
            'Content-Type': 'application/json;charset=UTF-8' 
        });

        const options = {
            method: 'POST',
            headers: header,
        }

        fetch('/api/User/ForgetPassword?email=' + this.email, options)
        .then(response => {
            if(response.status !== 200){
                throw Error(response.statusText);
            }
            return response.text();
        })
        .then(message => console.log(message))
        .catch(error => console.log(error));
    }
}