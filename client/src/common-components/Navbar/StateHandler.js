import React from "react";
import { Page } from "./Page";
import { CookieHandler } from "../../cookies/CookieHandler";
import { AuthenticationHandler } from "../../tools/AuthenticationHandler";

export class StateHandler extends React.Component{
    constructor(props){
        super(props);

        this.state = {
            logged: false
        }

    }

    render(){
        return(
            <Page
            logged={this.state.logged} 
            logOff={this.logOff}
            />
        );
    }

    componentDidMount(){
        const isLogged = AuthenticationHandler.IsLogged();
        this.setState({logged: isLogged});
    }

    logOff(){
        CookieHandler.SetAuthorizationCookie("");
        window.location = window.location.origin + "/About";
    }
}