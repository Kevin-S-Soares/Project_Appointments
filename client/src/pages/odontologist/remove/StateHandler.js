import React from "react";
import { Page } from "./Page";
import { CookieHandler } from "../../../cookies/CookieHandler"

export class StateHandler extends React.Component{
    constructor(props){
        super(props);
        this.id = document.location.pathname.match("/Odontologist/Remove/(\\d+)") === null? 
        0 : parseInt(document.location.pathname.match("/Odontologist/Remove/(\\d+)")[1]);
        this.state = {
            fetched: false,
            odontologist: {}
        }

        this.submit = this.submit.bind(this);
    }

    render(){
        return(
            <Page 
            fetched={this.state.fetched}
            data={this.state.odontologist}
            submit={this.submit}
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
            this.setState({
                fetched: true,
                odontologist: json
            });
        });
    }

    submit(event){
        event.preventDefault();

        const header = CookieHandler.GetAuthorizedHeader();
        const options = {
            method: 'DELETE',
            headers: header,
        }
        
        fetch('/api/Odontologist?id=' + this.id, options)
        .then(response => response.text())
        .then(json => console.log(json));
    }
}