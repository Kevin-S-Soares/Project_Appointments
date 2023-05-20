import React from "react";

export function Page(props){
    return(
        <div>
            <form>
                <label>Start:</label>
                <input type="time" id="start" onChange={props.changeValue}/>
                <br />
                <label>End:</label>
                <input type="time" id="end" onChange={props.changeValue} />
                <br />
                <label>Patient name:</label>
                <input type="text" id="patientName" onChange={props.changeValue} />
                <br />
                <label>Description:</label>
                <input type="text" id="patientName" onChange={props.changeValue} />
                <br />
                <input type="submit" onClick={props.submit}/>
                <a href={window.location.origin + "/Appointment/Index"}>Return</a>
            </form>
        </div>
    )
}

export function selectOdontologist(props){
    const options = props.odontologist.map(element => 
        <option value={element.id}>{element.name}</option>);
    return (
        <select>
            {options}
        </select>
    );
}

export function selectSchedules(props){
    const options = props.schedules.map(element => 
        <option value={element.id}>{element.id}</option>);
    return (
        <select>
            {options}
        </select>
    );
}