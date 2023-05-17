import React from "react";

export function Page(props) {
    if(!props.fetched){
        return loading(props);
    }
    if(props.data === {}){
        return noData(props);
    }
    return populateForm(props);
}

function loading(props){
    return(
        <h5>Loading.</h5>
    );
}

function noData(props){
    return(
        <div>
            <h3>No data available.</h3>
            <a href={window.location.origin + "/Odontologist/Index"}>Return</a>
        </div>
    );
}

function populateForm(props){
    return (
        <div>
            <form>
                <label>Name:</label>
                <input type="text" id="name" onChange={props.changeValue} value={props.data.name} />
                <br />
                <label>Email:</label>
                <input type="email" id="email" onChange={props.changeValue} value={props.data.email} />
                <br />
                <label>Phone:</label>
                <input type="tel" id="phone" onChange={props.changeValue} value={props.data.phone} />
                <br />
                <input type="submit" onClick={props.submit} value="Submit"/>
                <a href={window.location.origin + "/Odontologist/Index"}>Return</a>
            </form>
        </div>
    );
}
