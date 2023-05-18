import React from "react";
import { CookieHandler } from "../../../cookies/CookieHandler";
import { Page } from "./Page";

export class StateHandler extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            fetched: false,
            data: []
        }
    }

    render() {
        return (
            <Page 
            data={this.state.data} 
            fetched={this.state.fetched}
            />
        );
    }

    componentDidMount() {
        const header = CookieHandler.GetAuthorizedHeader();
        const options = {
            method: 'GET',
            headers: header
        }
        fetch('/api/Appointment', options)
            .then(response => response.json())
            .then(json => this.setState({
                fetched: true,
                data: json
            }));
    }
}