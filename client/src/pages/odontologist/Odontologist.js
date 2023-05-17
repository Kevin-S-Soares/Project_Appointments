import React from "react";

import { Add } from "./add/Add";
import { Index } from "./index/Index";
import { Edit } from "./edit/Edit";
import { Remove } from "./remove/Remove"
import { Details } from "./details/Details";


export function AddOdontologist(props){
    return(
        <Add />
    );
}

export function IndexOdontologist(props){
    return(
        <Index />
    );
}

export function EditOdontologist(props){
    return(
        <Edit />
    );
}

export function RemoveOdontologist(props){
    return(
        <Remove />
    );
}

export function DetailsOdontologist(props){
    return (
        <Details />
    );
}