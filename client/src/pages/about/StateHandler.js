import React from "react";
import { Page } from "./Page";

export class StateHandler extends React.Component{
    constructor(props){
        super(props);
        this.state = {};
    }

    render(){
        return(
            <Page />
        );
    }
}