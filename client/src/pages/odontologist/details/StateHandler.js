import React from "react";
import { Page } from "./Page";
import { CookieHandler } from "../../../cookies/CookieHandler"

export class StateHandler extends React.Component{
    constructor(props){
        super(props);
        this.id = document.location.pathname.match("/Odontologist/Details/(\\d+)") === null? 
        0 : parseInt(document.location.pathname.match("/Odontologist/Details/(\\d+)")[1]);
        
        this.state = {
            fetched: false
        }
    }

    render(){
        return(
            <Page
            fetched={this.state.fetched}
            data={this.state.odontologist}
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
}