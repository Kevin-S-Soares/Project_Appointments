import React from "react";

export function Page(props){
    return(
        <div>
            <form>
                <label>Name:</label>
                <input type="text" id="name" onChange={props.changeValue}/>
                <br />
                <label>Email:</label>
                <input type="email" id="email" onChange={props.changeValue} />
                <br />
                <label>Phone:</label>
                <input type="tel" id="phone" onChange={props.changeValue} />
                <br />
                <input type="submit" onClick={props.submit}/>
            </form>
        </div>
    )
}