import React from "react";

export function Page(props) {
    if (!props.fetched) {
        return loading(props);
    }
    if (props.data === {}) {
        return noData(props);
    }
    return populateList(props);
}

function loading(props) {
    return (
        <h5>Loading.</h5>
    );
}

function noData(props) {
    return (
        <div>
            <h3>No data available.</h3>
            <a href={window.location.origin + "/Odontologist/Index"}>Return</a>
        </div>

    );
}

function populateList(props) {
    return (
        <div>
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
            <form>
                <input type="submit" onClick={props.submit} value="Submit"/>
                <a href={window.location.origin + "/Odontologist/Index"}>Return</a>
            </form>
        </div>
    );
}