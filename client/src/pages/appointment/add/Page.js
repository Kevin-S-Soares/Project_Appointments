import React from "react";

export function Page(props){
    return(
        <div>
            <form>
                <label>Odontologist:</label>
                {selectOdontologist(props)}
                <br />
                <label>Schedule:</label>
                {selectSchedules(props)}
                <br />
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
                <input type="text" id="description" onChange={props.changeValue} />
                <br />
                <input type="submit" onClick={props.submit}/>
                <a href={window.location.origin + "/Appointment/Index"}>Return</a>
            </form>
        </div>
    )
}

function selectOdontologist(props){
    if(!props.fetched){
        return(
            <select></select>
        );
    }
    const options = props.data.map((element, iteration) => 
        <option value={element.odontologist.id} 
        key={iteration}
        >{element.odontologist.name}</option>
    )
    return (
        <select onChange={props.changeOdontologist}>
            {options}
        </select>
    );
}

function selectSchedules(props){
    if(props.odontologistId === -1){
        return(
            <select></select>
        );
    }
    let query = props.data.filter(x => x.odontologist.id === props.odontologistId)[0];
    query = query.schedules.map(x => x.schedule)
    const options = query.map((x, y) => <option value={x.id} key={y}>{x.name}</option>)
    return(
        <select>{options}</select>
    );
}