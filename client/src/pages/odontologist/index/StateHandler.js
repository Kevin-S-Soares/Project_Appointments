import React from "react";
import { CookieHandler } from "../../../cookies/CookieHandler";
import { Page } from "./Page";
import { AuthenticationHandler } from "../../../tools/AuthenticationHandler"

export class StateHandler extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            fetched: false,
            data: []
        }
    }

    render() {
        if(!AuthenticationHandler.IsLogged()){
            window.location = window.location.origin;
            return;
        }
        return (
            <Page 
            data={this.state.data} 
            fetched={this.state.fetched}
            />
        );
    }

    componentDidMount() {
        if(!AuthenticationHandler.IsLogged()){
            window.location = window.location.origin;
            return;
        }
        const header = CookieHandler.GetAuthorizedHeader();
        const options = {
            method: 'GET',
            headers: header
        }
        fetch('/api/Odontologist', options)
            .then(response => response.json())
            .then(json => this.setState({
                fetched: true,
                data: json
            }));
    }
}