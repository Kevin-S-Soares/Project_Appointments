import React from "react";
import { AddAppointment } from "./add/AddAppointment"
import { IndexAppointment }  from "./index/IndexAppointment"
import { EditAppointment } from "./edit/EditAppointment"
import { RemoveAppointment } from "./remove/RemoveAppointment"

export function Add(props){
    return(
        <AddAppointment />
    );
}

export function Index(props){
    return(
        <IndexAppointment />
    );
}

export function Edit(props){
    return(
        <EditAppointment />
    );
}

export function Remove(props){
    return(
        <RemoveAppointment />
    );
}