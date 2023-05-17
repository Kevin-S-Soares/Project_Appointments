import React from "react";

export function Page(props){
    if(!props.fetched){
        return loading(props);
    }
    if(props.data === {}){
        return noData(props);
    }
    return populateList(props);
}


function loading(props){
    return(
        <h3>Loading.</h3>
    );
}

function noData(props){
    return(
        <h3>No data available.</h3>
    );
}

function populateList(props){
    return (
        <dl>
        <dt>Id</dt>
        <dd>{props.data.id}</dd>
        <dt>Name</dt>
        <dd>{props.data.name}</dd>
        <dt>Email</dt>
        <dd>{props.data.email}</dd>
        <dt>Phone</dt>
        <dd>{props.data.phone}</dd>
    </dl>
    );
}