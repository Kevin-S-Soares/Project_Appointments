import React from "react";
import { Page } from "./Page";
import { AuthenticationHandler } from "../../../tools/AuthenticationHandler";

export class StateHandler extends React.Component {
    constructor(props) {
        super(props);

        this.register = {
            email: "",
            password: ""
        }
        this.confirmPassword = ""

        this.state = {
            isEmailValid: true,
            isPasswordValid: true,
            isConfirmPasswordValid: true,
            isSubmitButtonDisabled: true,
            message: null
        }

        this.submit = this.submit.bind(this);
        this.changeEmail = this.changeEmail.bind(this);
        this.changePassword = this.changePassword.bind(this);
        this.changeConfirmPassword = this.changeConfirmPassword.bind(this);
    }

    render() {
        if (AuthenticationHandler.IsLogged()) {
            window.location = window.location.origin
        }
        return (
            <Page
                submit={this.submit}
                changeEmail={this.changeEmail} 
                changePassword={this.changePassword}
                changeConfirmPassword={this.changeConfirmPassword}
                isEmailValid={this.state.isEmailValid} 
                isPasswordValid={this.state.isPasswordValid}
                isConfirmPasswordValid={this.state.isConfirmPasswordValid}
                isSubmitButtonDisabled={this.state.isSubmitButtonDisabled}
                message={this.state.message}
            />
        );
    }

    changeEmail(event) {
        const value = event.target.value;
        if (value.match("^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$") === null) {
            this.setState({ isEmailValid: false })
        }
        else {
            this.setState({ isEmailValid: true })
        }
        this.register.email = value;
        this.changeSubmitButton();
    }

    changePassword(event) {
        const value = event.target.value;
        if (value.match("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$") === null) {
            this.setState({ isPasswordValid: false })
        }
        else {
            this.setState({ isPasswordValid: true })
        }
        this.register.password = value;
        this.changeSubmitButton();
    }

    changeConfirmPassword(event){
        const value = event.target.value;
        const condition = this.register.password === value;
        this.confirmPassword = value;
        this.setState({isConfirmPasswordValid: condition});
        this.changeSubmitButton();
    }

    changeSubmitButton(){
        const condition = this.register.email
        .match("^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$") !== null
        && this.register.password
        .match("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$") !== null
        && this.register.password === this.confirmPassword;
        this.setState({isSubmitButtonDisabled: condition? "" : "disabled"});
    }

    submit(event) {
        event.preventDefault();

        const body = JSON.stringify(this.register);
        const header = new Headers({
            'Content-Type': 'application/json;charset=UTF-8'
        })
        const options = {
            method: 'POST',
            headers: header,
            body: body
        }

        fetch('/api/User/Register', options)
            .then(response => {
                if (response.status !== 200) {
                    return response.text().then(error => {throw new Error(error)});
                }
                return response.json()
            })
            .then()
            .catch(error => this.setState({message: error.message}))
    }
}