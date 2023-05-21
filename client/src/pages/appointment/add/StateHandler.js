import React from "react";
import { Page } from "./Page";
import { CookieHandler } from "../../../cookies/CookieHandler"

export class StateHandler extends React.Component{
    constructor(props){
        super(props);
        this.data = [];
        this.appointment = {
            id: 0,
            scheduleId: 0,
            start: new Date(),
            end: new Date(),
            patientName: '',
            description: '',
        }

        this.state = {
            fetched: false,
            data: [],
            odontologistId: -1
        }

        this.submit = this.submit.bind(this);
        this.changeValue = this.changeValue.bind(this);
        this.changeOdontologist = this.changeOdontologist.bind(this);
    }

    render(){
        return(
            <Page 
            fetched={this.state.fetched}
            data={this.state.data}
            odontologistId={this.state.odontologistId}
            submit={this.submit}
            changeValue={this.changeValue}
            changeOdontologist={this.changeOdontologist}
            />
        )
    }

    componentDidMount(){
        const header = CookieHandler.GetAuthorizedHeader();
        const options = {
            method: 'GET',
            headers: header,
        }
        
        fetch('/api/DetailedOdontologist', options)
        .then(response => response.json())
        .then(json => {
            const id = json.length === 0? -1 : json[0].odontologist.id;
            this.data = json;
            this.setState({
                fetched: true,
                data: json,
                odontologistId: id
            });});
    }

    submit(event){
        event.preventDefault();

        const header = CookieHandler.GetAuthorizedHeader();
        const body = JSON.stringify(this.odontologist);
        const options = {
            method: 'POST',
            headers: header,
            body: body
        }
        
        fetch('/api/Appointment', options)
        .then(response => response.json())
        .then(json => console.log(json));
    }

    changeOdontologist(event){
        const id = parseInt(event.target.value)
        this.setState({
            odontologistId: id
        })
    }

    changeValue(event){
        const key = event.target.id;
        const value = event.target.value;
        this.odontologist[key] = value;
    }
}