import React from "react";

import { AddOdontologist } from "./add/AddOdontologist";
import { IndexOdontologist } from "./index/IndexOdontologist";
import { EditOdontologist } from "./edit/EditOdontologist";
import { RemoveOdontologist } from "./remove/RemoveOdontologist"
import { DetailsOdontologist } from "./details/DetailsOdontologist";


export function Add(props){
    return(
        <AddOdontologist />
    );
}

export function Index(props){
    return(
        <IndexOdontologist />
    );
}

export function Edit(props){
    return(
        <EditOdontologist />
    );
}

export function Remove(props){
    return(
        <RemoveOdontologist />
    );
}

export function Details(props){
    return (
        <DetailsOdontologist />
    );
}