import React from "react";
import { Page } from "./Page";
import { CookieHandler } from "../../../cookies/CookieHandler"

export class StateHandler extends React.Component{
    constructor(props){
        super(props);
        this.id = document.location.pathname.match("/Odontologist/Edit/(\\d+)") === null? 
        0 : parseInt(document.location.pathname.match("/Odontologist/Edit/(\\d+)")[1]);
        this.odontologist = {
            name: '',
            email: '',
            phone: ''
        }

        this.state = {
            fetched: false,
            odontologist: this.odontologist
        }

        this.submit = this.submit.bind(this);
        this.changeValue = this.changeValue.bind(this);
    }

    render(){
        return(
            <Page
            fetched={this.state.fetched}
            data={this.state.odontologist}
            submit={this.submit}
            changeValue={this.changeValue}
            />
        );
    }

    componentDidMount(){
        const header = CookieHandler.GetAuthorizedHeader();
        const options = {
            method: 'GET',
            headers: header,
        }
        
        fetch('/api/Odontologist/' + this.id, options)
        .then(response => response.json())
        .then(json => {
            this.odontologist = json;
            this.setState({
                fetched: true,
                odontologist: this.odontologist
            });
        });
    }

    submit(event){
        event.preventDefault();

        const header = CookieHandler.GetAuthorizedHeader();
        const body = JSON.stringify(this.odontologist);
        const options = {
            method: 'PUT',
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
        this.setState({odontologist: this.odontologist})
    }
}